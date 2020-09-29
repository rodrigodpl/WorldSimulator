using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UprisingResult { NONE, DISCONTENT, IMPROVED_RIGHTS, REFORM, SCHISM}

public class UnrestEvent : WS_BaseEvent
{
    public UnrestEvent() { eventName = "Unrest"; module = EventModule.GOVERNMENT; }

    protected override void Success()
    {
        tile.unrest += 1.0f / (tile.prosperity / tile.population);
        tile.unrest *= tile.unrestDecay;


        if (tile.culture != tile.government.rulingCulture)
            tile.unrestCultural += 2.5f;

        if (tile.religion != tile.government.rulingReligion)
            tile.unrestReligious += 2.5f;

        if (tile.culture != tile.government.rulingCulture && tile.religion != tile.government.rulingReligion)
        {
            tile.unrestCultural += 2.5f;
            tile.unrestReligious += 2.5f;
        }

        if (tile.government.rebels == null)                                                 tile.government.rebels = tile;
        else if(tile.government.rebels.unrest < tile.unrest)                                tile.government.rebels = tile;

        if (tile.government.rebelsReligious == null)                                        tile.government.rebelsReligious = tile;
        else if (tile.government.rebelsReligious.unrestReligious < tile.unrestReligious)    tile.government.rebelsReligious = tile;

        if (tile.government.rebelsCultural == null)                                         tile.government.rebelsCultural = tile;
        else if (tile.government.rebelsCultural.unrestCultural < tile.unrestCultural)       tile.government.rebelsCultural = tile;

        tile.unrest *= tile.unrestDecay;
        tile.unrestCultural *= tile.unrestDecay;
        tile.unrestReligious *= tile.unrestDecay;

        tile.government.unrest += tile.unrest * 0.3f;
        tile.government.unrestCultural += tile.unrestDecay * 0.3f;
        tile.government.unrestReligious += tile.unrestDecay * 0.3f;
    }

}


public class UprisingEvent : WS_BaseEvent
{
    float totalUnrest = 0.0f;

    public UprisingEvent() { eventName = "Uprising"; module = EventModule.GOVERNMENT; }

    protected override bool FireCheck()
    {
        return tile == tile.government.capital;
    }

    protected override bool SuccessCheck()
    {
        totalUnrest = (tile.government.unrest + tile.government.unrestCultural + tile.unrestReligious);

        return totalUnrest / tile.government.repression > Random.Range(0.0f, 50000.0f);
    }

    protected override void Success()
    {
        UprisingResult result = UprisingResult.NONE;
        float selector = Random.Range(0.0f, 5000.0f) + totalUnrest * tile.government.repression;

        if (selector < 2000.0f) result = UprisingResult.DISCONTENT;
        if (selector < 3500.0f) result = UprisingResult.IMPROVED_RIGHTS;
        if (selector < 4500.0f) result = UprisingResult.REFORM;
        else                    result = UprisingResult.SCHISM;

        if (result == UprisingResult.DISCONTENT)
        { 
            tile.government.repression = Mathf.Min(2.0f, tile.government.repression + 0.3f);
            tile.culture.syncretism -= 2.0f;
            tile.religion.syncretism -= 2.0f;
        }
        else
        {
            switch (result)
            {
                case UprisingResult.IMPROVED_RIGHTS:    tile.government.repression = Mathf.Max(0.5f, tile.government.repression - 0.1f);  break;
                case UprisingResult.REFORM:             tile.government.repression = Mathf.Max(0.5f, tile.government.repression - 0.25f); break;
                case UprisingResult.SCHISM:             tile.government.repression = Mathf.Max(0.5f, tile.government.repression - 0.5f);  break;
            }

            if (tile.government.unrestReligious > tile.government.unrestCultural && tile.government.unrestReligious > tile.unrest)
            {
                if (tile.government != tile.government.rebelsReligious.government)
                    return;

                switch (result)
                {
                    case UprisingResult.IMPROVED_RIGHTS:    tile.government.rebelsReligious.religion.influenceMul += 0.5f; break;
                    case UprisingResult.REFORM:             tile.government.rulingReligion = tile.government.rebelsReligious.religion; break;
                    case UprisingResult.SCHISM:             tile.government.CreateSchism(tile.government.rebelsReligious, EventModule.RELIGION); break;
                }
            }
            else if (tile.government.unrestCultural > tile.government.unrest && tile.government.unrestCultural > tile.unrestReligious)
            {
                if (tile.government != tile.government.rebelsCultural.government)
                    return;

                switch (result)
                {
                    case UprisingResult.IMPROVED_RIGHTS:    tile.government.rebelsCultural.culture.influenceMul += 0.5f; break;
                    case UprisingResult.REFORM:             tile.government.rulingCulture = tile.government.rebelsCultural.culture; break;
                    case UprisingResult.SCHISM:             tile.government.CreateSchism(tile.government.rebelsCultural, EventModule.CULTURE); break;
                }
            }
            else
            {
                if (tile.government != tile.government.rebels.government)
                    return;

                switch (result)
                {
                    case UprisingResult.IMPROVED_RIGHTS:    tile.government.repression = 0.5f; break;
                    case UprisingResult.REFORM:             tile.government.rulingCulture.influenceMul -= 0.5f;
                                                            tile.government.rulingReligion.influenceMul -= 0.5f; break;
                    case UprisingResult.SCHISM:             tile.government.CreateSchism(tile.government.rebels, EventModule.POPULATION); break;
                }
            }

        }

        tile.government.unrest = tile.government.unrestCultural = tile.unrestReligious = 0.0f;
    }

    protected override void Fail()
    {
        tile.government.unrest = tile.government.unrestCultural = tile.unrestReligious = 0.0f;
    }
}
