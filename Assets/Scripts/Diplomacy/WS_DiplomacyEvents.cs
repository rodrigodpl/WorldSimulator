using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNeighborsEvent : WS_BaseEvent
{
    public FindNeighborsEvent() { eventName = "Find Neighbors"; module = EventModule.DIPLOMACY; }

    protected override void Success()
    {
        List<WS_Government> foundGovs = new List<WS_Government>();

        foreach (WS_Tile neighbor in tile.Neighbors())
        {
            if (neighbor.government != null)
            {
                if (neighbor.government != tile.government && !tile.government.borderingGovernments.Contains(neighbor.government))
                {
                    foundGovs.Add(neighbor.government);
                    tile.government.borderingGovernments.Add(neighbor.government);
                    tile.government.borderingOpinions.Add(0);
                }
            }
        }

        for (int i = 0; i < tile.government.borderingGovernments.Count; i++)
        {
            if (!foundGovs.Contains(tile.government.borderingGovernments[i]))
            {
                tile.government.borderingGovernments.RemoveAt(i);
                i--;
            }
        }
    }
}

public class OpinionsEvent : WS_BaseEvent
{
    public OpinionsEvent() { eventName = "Opinions"; module = EventModule.DIPLOMACY; }

    protected override void Success()
    {
        int index = 0;
        foreach (WS_Government gov in tile.government.borderingGovernments)
        {
            int opinion = 0;

            if (gov.rulingCulture == tile.government.rulingCulture) opinion += 20;
            else opinion -= 20;

            if (gov.rulingReligion == tile.government.rulingReligion) opinion += 20;
            else opinion -= 20;

            if (gov.powerDistribution == tile.government.powerDistribution) opinion += 10;
            else opinion -= 10;

            if (gov.powerHolder == tile.government.powerHolder) opinion += 10;
            else opinion -= 10;

            if (gov.centralization == tile.government.centralization) opinion += 10;
            else opinion -= 10;

            if (gov.authoritarianism == tile.government.authoritarianism) opinion += 10;
            else opinion -= 10;

            opinion += (int)(tile.government.legitimacy / 50.0f);

            foreach (WS_Treaty treaty in tile.government.treaties)
            {
                if (treaty.target == gov)
                {
                    switch (treaty.type)
                    {
                        case TreatyType.ALLIANCE: opinion += 30; break;
                        case TreatyType.TRADE_AGREEMENT: opinion += 20; break;
                        case TreatyType.TRADE_EMBARGO: opinion -= 30; break;
                        case TreatyType.WAR: opinion -= 100; break;
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


        if (opinion > 50.0f)        treaty = TreatyType.ALLIANCE;
        else if (opinion > 30.0f)   treaty = TreatyType.TRADE_AGREEMENT;

        else if (opinion > -30.0f)  treaty = TreatyType.PEACE;
        else if (opinion > -50.0f)  treaty = TreatyType.TRADE_EMBARGO;
        else                        treaty = TreatyType.WAR;


        return Random.Range(0.0f, 1.0f) < 0.1f;
    }

    protected override void Success()
    {
        foreach(WS_Treaty existingTreaty in tile.government.treaties)
            if(existingTreaty.target == target)
            {
                if(existingTreaty.type != treaty)
                    tile.government.treaties.Remove(existingTreaty);
                else
                    return;
            }

        WS_Treaty newTreaty = new WS_Treaty();
        newTreaty.type = treaty;
        newTreaty.remainingDuration = Mathf.FloorToInt(Random.Range(30.0f, 100.0f));

        newTreaty.target = target;
        tile.government.treaties.Add(newTreaty);

        newTreaty.target = tile.government;
        target.treaties.Add(newTreaty);
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
                tile.government.treaties.Remove(treaty);
                return;
            }
        }
    }
}

