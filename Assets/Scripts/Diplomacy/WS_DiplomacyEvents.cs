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
            int opinion = 0;

            if (gov.rulingCulture.tribal)
                opinion -= 5;

            if (gov.rulingReligion.tribal)
                opinion -= 5;

            if (gov.rulingCulture == tile.government.rulingCulture)         opinion += 20;
            else opinion -= 20;

            if (gov.rulingReligion == tile.government.rulingReligion)       opinion += 20;
            else opinion -= 20;

            if (gov.powerDistribution == tile.government.powerDistribution) opinion += 10;
            else opinion -= 10;

            if (gov.powerHolder == tile.government.powerHolder)             opinion += 10;
            else opinion -= 10;

            if (gov.centralization == tile.government.centralization)       opinion += 10;
            else opinion -= 10;

            if (gov.authoritarianism == tile.government.authoritarianism)   opinion += 10;
            else opinion -= 10;

            opinion += (int)(tile.government.legitimacy / 50.0f);

            foreach (WS_Treaty treaty in tile.government.treaties)
            {
                if (treaty.target == gov)
                {
                    switch (treaty.type)
                    {
                        case TreatyType.ALLIANCE:           opinion += 30; break;
                        case TreatyType.TRADE_AGREEMENT:    opinion += 20; break;
                        case TreatyType.PEACE:              opinion += 10; break;
                        case TreatyType.TRUCE:              opinion -= 10; break;
                        case TreatyType.TRADE_EMBARGO:      opinion -= 30; break;
                        case TreatyType.WAR:                opinion -= 100; break;
                    }
                }
            }

            tile.government.borderingOpinions[index] = Mathf.Lerp(tile.government.borderingOpinions[index], opinion, 0.01f);
            tile.government.borderingOpinions[index] += Random.Range(-5.0f, 5.0f);
            index++;
        }
    }
}



public class TreatyProposalEvent : WS_BaseEvent
{
    public TreatyProposalEvent() { eventName = "Treaty Proposal"; module = EventModule.DIPLOMACY; }

    WS_Government target = null;
    float opinion = 0;
    TreatyType treaty = TreatyType.PEACE;

    protected override bool FireCheck()
    {
        return tile.government.capital == tile && tile.government.borderingGovernments.Count > 1;
    }

    protected override bool SuccessCheck()
    {
        treaty = TreatyType.PEACE;

        opinion = 0.0f;

        int selector = Mathf.FloorToInt(Random.Range(0.0f, tile.government.borderingGovernments.Count - 0.01f));
        opinion = tile.government.borderingOpinions[selector];
        target = tile.government.borderingGovernments[selector];


        if (opinion > 50.0f)                        treaty = TreatyType.ALLIANCE;
        else if (opinion > 30.0f)                   treaty = TreatyType.TRADE_AGREEMENT;

        else if (opinion > -30.0f)                  treaty = TreatyType.PEACE;
        else if (opinion > -50.0f)                  treaty = TreatyType.TRADE_EMBARGO;

        else if (tile.government.soldierPool == 0)  treaty = TreatyType.WAR;

        if(treaty == TreatyType.WAR)
            return Random.Range(0.0f, 1.0f) < 0.03f;
        else
            return Random.Range(0.0f, 1.0f) < 0.1f;
    }

    protected override void Success()
    {
        foreach (WS_Treaty existingTreaty in tile.government.treaties)
        {
            if (existingTreaty.type == TreatyType.WAR)
                if(treaty == TreatyType.WAR || existingTreaty.target == target)
                    return;

            else if (existingTreaty.target == target)
            {
                if (existingTreaty.type != treaty && existingTreaty.type != TreatyType.TRUCE)
                {
                    tile.government.treaties.Remove(existingTreaty);
                    break;
                }
                else
                    return;
            }
        }

        WS_Treaty newTreatyA = new WS_Treaty();
        WS_Treaty newTreatyB = new WS_Treaty();
        newTreatyA.type = newTreatyB.type = treaty;

        newTreatyA.remainingDuration = newTreatyB.remainingDuration = Mathf.FloorToInt(Random.Range(30.0f, 100.0f));

        newTreatyA.target = target;
        tile.government.treaties.Add(newTreatyA);

        newTreatyB.target = tile.government;
        target.treaties.Add(newTreatyB);

        if (newTreatyA.type == TreatyType.WAR)
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
                    treaty.remainingDuration = 30;
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

