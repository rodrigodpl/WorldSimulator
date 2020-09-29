using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNeighborsEvent : WS_BaseEvent
{
    public FindNeighborsEvent() { eventName = "Find Neighbors"; module = EventModule.DIPLOMACY; }

    protected override void Success()
    {
        foreach (WS_Tile neighbor in tile.Neighbors())
        {
            if (neighbor.government != null)
            {
                if (neighbor.government != tile.government)
                {
                    int index = tile.government.borderingGovernments.IndexOf(neighbor.government);

                    if (index == -1)
                    {
                        tile.government.borderingGovernments.Add(neighbor.government);
                        tile.government.borderingOpinions.Add(10000);
                    }
                    else if(tile.government.borderingOpinions[index] < 1000)
                        tile.government.borderingOpinions[index] += 10000;
                }
            }
        }
    }
}

public class OpinionsEvent : WS_BaseEvent
{
    public OpinionsEvent() { eventName = "Opinions"; module = EventModule.DIPLOMACY; }

    protected override bool FireCheck()
    {
        return tile.government.capital == tile && tile.government.borderingGovernments.Count > 1;
    }

    protected override void Success()
    {
        for (int i = 0; i < tile.government.borderingGovernments.Count; i++)
        {
            if (tile.government.borderingOpinions[i] < 1000)
            {
                tile.government.borderingGovernments.RemoveAt(i);
                tile.government.borderingOpinions.RemoveAt(i);
                i--;
            }
            else
                tile.government.borderingOpinions[i] -= 10000;
        }

        int index = 0;
        foreach (WS_Government gov in tile.government.borderingGovernments)
        {
            int opinion = 35;

            if (gov.rulingCulture == tile.government.rulingCulture) opinion += 15;
            else
            {
                opinion -= 25;

                foreach (WS_Trait trait in tile.culture.traits)
                    foreach (WS_Trait neighborTrait in gov.rulingCulture.traits)
                    {
                        if (trait == neighborTrait)
                            opinion += 10;
                    }
            }

            if (gov.rulingReligion == tile.government.rulingReligion)       opinion += 15;
            else
            {
                opinion -= 25;

                foreach (WS_Trait trait in tile.religion.traits)
                    foreach (WS_Trait neighborTrait in gov.rulingCulture.traits)
                    {
                        if (trait == neighborTrait)
                            opinion += 10;
                    }
            }

            opinion -= 25;

            foreach (WS_Trait trait in tile.government.traits)
                foreach (WS_Trait neighborTrait in gov.traits)
                {
                    if (trait == neighborTrait)
                        opinion += 10;
                }

            foreach (WS_Treaty treaty in tile.government.treaties)
            {
                if (treaty.target == gov)
                {
                    switch (treaty.type)
                    {
                        case TreatyType.ALLIANCE:           opinion += 40; break;
                        case TreatyType.TRADE_AGREEMENT:    opinion += 30; break;
                        case TreatyType.NON_AGGRESSION:     opinion += 10; break;
                        case TreatyType.TRUCE:              opinion -= 10; break;
                        case TreatyType.TRADE_EMBARGO:      opinion -= 40; break;
                        case TreatyType.WAR:                opinion -= 100; break;
                    }
                }
            }


            tile.government.borderingOpinions[index] = Mathf.Lerp(tile.government.borderingOpinions[index], opinion, 0.1f);
            tile.government.borderingOpinions[index] += Random.Range(-10, 10);
            index++;
        }
    }
}



public class TreatyProposalEvent : WS_BaseEvent
{
    public TreatyProposalEvent() { eventName = "Treaty Proposal"; module = EventModule.DIPLOMACY; }

    WS_Government target = null;
    float opinion = 0;
    TreatyType treaty = TreatyType.NON_AGGRESSION;

    protected override bool FireCheck()
    {
        return tile.government.capital == tile && tile.government.borderingGovernments.Count > 1;
    }

    protected override bool SuccessCheck()
    {
        int selector = Mathf.FloorToInt(Random.Range(0.0f, tile.government.borderingGovernments.Count - 0.01f));
        target = tile.government.borderingGovernments[selector];

        foreach (WS_Treaty treaty in tile.government.treaties)
            if (treaty.target == target)
                return false;


        treaty = TreatyType.NONE;
        opinion = tile.government.borderingOpinions[selector];

        if (opinion > 40.0f)                        treaty = TreatyType.ALLIANCE;
        else if (opinion > 20.0f)                   treaty = TreatyType.TRADE_AGREEMENT;

        else if (opinion > 10.0f)                   treaty = TreatyType.NON_AGGRESSION;
        else if (opinion > -50.0f && opinion < -20) treaty = TreatyType.TRADE_EMBARGO;

        else if (tile.government.soldierPool == 0)  treaty = TreatyType.WAR;

        if(treaty == TreatyType.WAR)
            return Random.Range(0.0f, 1.0f) < 0.03f;
        else
            return Random.Range(0.0f, 1.0f) < 0.2f;
    }

    protected override void Success()
    {
        WS_Treaty newTreatyA = new WS_Treaty();
        WS_Treaty newTreatyB = new WS_Treaty();
        newTreatyA.type = newTreatyB.type = treaty;

        newTreatyA.remainingDuration = newTreatyB.remainingDuration = Random.Range(30, 50);

        newTreatyA.target = target;
        tile.government.treaties.Add(newTreatyA);

        newTreatyB.target = tile.government;
        target.treaties.Add(newTreatyB);

        if (treaty == TreatyType.WAR)
        {
            tile.government.warNum++;
            target.warNum++;
        }
    }
}


public class TreatyEndedEvent : WS_BaseEvent
{
    public TreatyEndedEvent() { eventName = "Treaty Ended"; module = EventModule.DIPLOMACY; }

    protected override bool SuccessCheck()
    {
        return tile.government.capital == tile;
    }

    protected override void Success()
    {
        foreach(WS_Treaty treaty in tile.government.treaties)
        {
            treaty.remainingDuration--;
            if (treaty.remainingDuration <= 0)
            {
                if(treaty.type == TreatyType.WAR)
                {
                    treaty.type = TreatyType.TRUCE;
                    treaty.remainingDuration = Random.Range(20, 40);
                    tile.government.warNum--;
                    tile.government.warScore = 50.0f;
                }
                else
                    tile.government.treaties.Remove(treaty);

                return;
            }
        }
    }
}


public class JoinWarEvent : WS_BaseEvent
{
    public JoinWarEvent() { eventName = "Join War"; module = EventModule.DIPLOMACY; }

    WS_Treaty alliedWar = null;

    protected override bool FireCheck()
    {
        return tile.government.capital == tile;
    }

    protected override bool SuccessCheck()
    {
        alliedWar = null;
        List<WS_Government> alliances = new List<WS_Government>();

        foreach (WS_Treaty treaty in tile.government.treaties)
            if (treaty.type == TreatyType.ALLIANCE)
                alliances.Add(treaty.target);

        foreach(WS_Government alliance in alliances)
        {
            foreach(WS_Treaty alliedTreaty in alliance.treaties)
            {
                if (alliedTreaty.type == TreatyType.WAR && tile.government.borderingGovernments.Contains(alliedTreaty.target))
                {
                    bool valid = true;

                    foreach (WS_Treaty treaty in tile.government.treaties)
                    {
                        if (treaty.target == alliedTreaty.target)
                        {
                            switch (treaty.type)
                            {
                                case TreatyType.ALLIANCE: valid = false; break;
                                case TreatyType.TRUCE: valid = false; break;
                                case TreatyType.NON_AGGRESSION: valid = false; break;
                            }

                            if (valid)
                            {

                                foreach (WS_Treaty enemyTreaty in treaty.target.treaties)
                                    if (enemyTreaty.target == tile.government)
                                    {
                                        treaty.target.treaties.Remove(enemyTreaty);
                                        break;
                                    }

                                tile.government.treaties.Remove(treaty);
                                break;
                            }
                        }
                    }

                    alliedWar = alliedTreaty;
                    return Random.Range(0.0f, 1.0f) < ((tile.government.warNum > 0) ? 0.1f : 0.4f) && valid;
                }
            }
        }

        return false;
    }

    protected override void Success()
    {
        WS_Treaty newTreatyA = new WS_Treaty();
        WS_Treaty newTreatyB = new WS_Treaty();
        newTreatyA.type = newTreatyB.type = TreatyType.WAR;

        newTreatyA.remainingDuration = newTreatyB.remainingDuration = alliedWar.remainingDuration;

        newTreatyA.target = alliedWar.target;
        tile.government.treaties.Add(newTreatyA);

        newTreatyB.target = tile.government;
        alliedWar.target.treaties.Add(newTreatyB);

        tile.government.warNum++;
        alliedWar.target.warNum++;

    }
}

