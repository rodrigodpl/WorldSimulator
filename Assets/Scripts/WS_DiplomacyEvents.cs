using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpinionsEvent : WS_BaseEvent
{
    public OpinionsEvent() { eventName = "Opinions"; module = EventModule.DIPLOMACY; }

    protected override void Success()
    {
        if (tile.government.updateOpinion)
        {
            tile.government.borderingGovernments.Clear();
            tile.government.borderingOpinions.Clear();
            tile.government.updateOpinion = false;
        }

        foreach(WS_Tile neighbor in tile.Neighbors())
        {
            if(neighbor.government != tile.government && !tile.government.borderingGovernments.Contains(neighbor.government))
            {
                int opinion = 0;

                if (neighbor.government.rulingCulture == tile.government.rulingCulture)         opinion += 10;
                else                                                                            opinion -= 10;

                if (neighbor.government.rulingReligion == tile.government.rulingReligion)       opinion += 10;
                else                                                                            opinion -= 10;

                if (neighbor.government.powerDistribution == tile.government.powerDistribution) opinion += 10;
                else                                                                            opinion -= 10;

                if (neighbor.government.powerHolder == tile.government.powerHolder)             opinion += 10;
                else                                                                            opinion -= 10;

                if (neighbor.government.centralization == tile.government.centralization)       opinion += 10;
                else                                                                            opinion -= 10;

                if (neighbor.government.authoritarianism == tile.government.authoritarianism)   opinion += 10;
                else                                                                            opinion -= 10;

                opinion += (int)(tile.government.legitimacy / 50.0f);

                foreach(WS_Treaty treaty in tile.government.treaties)
                {
                    if(treaty.members.Contains(neighbor.government))
                    {
                        switch(treaty.type)
                        {
                            case TreatyType.WAR:                opinion += 30; break;
                            case TreatyType.ALLIANCE:           opinion += 20; break;
                            case TreatyType.TRADE_AGREEMENT:    opinion += 20; break;
                            case TreatyType.TRADE_EMBARGO:      opinion += 10; break;
                        }
                    }
                    else if(treaty.targets.Contains(neighbor.government))
                    {
                        switch (treaty.type)
                        {
                            case TreatyType.WAR:                opinion -= 100; break;
                            case TreatyType.TRADE_EMBARGO:      opinion -= 30; break;
                        }
                    }
                }

                tile.government.borderingGovernments.Add(neighbor.government);
                tile.government.borderingOpinions.Add(opinion);
            }
        }
    }
}


public class TreatyProposalEvent : WS_BaseEvent
{
    public TreatyProposalEvent() { eventName = "Treaty Proposal"; module = EventModule.DIPLOMACY; }

    WS_Government target = null;
    int opinion = 0;
    TreatyType treaty = TreatyType.NONE;

    protected override bool FireCheck()
    {
        tile.government.updateOpinion = true;
        opinion = 0;
        WS_Government target = null;

        int selector = Mathf.FloorToInt(Random.Range(0.0f, tile.government.borderingGovernments.Count - 0.01f));
        target = tile.government.borderingGovernments[selector];
        opinion = tile.government.borderingOpinions[selector];

        return Random.Range(0.0f, 1.0f) < Mathf.Abs(opinion) / 100.0f;
    }

    protected override bool SuccessCheck()
    {
        treaty = TreatyType.NONE;
        float chance = 0.0f; 

        if(opinion > 0)
        {
            chance = Random.Range(0.0f, opinion);

            if      (chance > 30)   treaty = TreatyType.TRADE_AGREEMENT;
            else if (chance > 50)   treaty = TreatyType.ALLIANCE;
        }
        else
        {
            chance = Random.Range(opinion, 0.0f);

            if      (chance < -30)  treaty = TreatyType.TRADE_EMBARGO;
            else if (chance < -50)  treaty = TreatyType.WAR;
        }

        return treaty != TreatyType.NONE;
    }

}
