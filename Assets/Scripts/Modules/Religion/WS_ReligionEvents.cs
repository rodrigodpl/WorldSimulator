using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ReligiousAdoptionEvent : WS_BaseEvent
{
    public ReligiousAdoptionEvent() { eventName = "Religious Adoption"; module = EventModule.RELIGION; }

    List<WS_Tile> neighboringReligions = new List<WS_Tile>();
    WS_Tile adoptedReligion = null;

    protected override bool FireCheck()
    {
        neighboringReligions.Clear();
        adoptedReligion = null;

        foreach (WS_Tile neighbor in tile.CulturedNeighbors())
        {
            if (neighbor.religion != tile.religion)
                neighboringReligions.Add(neighbor);
        }

        return neighboringReligions.Count > 1;
    }

    protected override bool SuccessCheck()
    {
        foreach (WS_Tile neighboringReligion in neighboringReligions)
        {
            float popBalance = neighboringReligion.population / tile.population;

            float growthBalance = neighboringReligion.lastPopGrowth / tile.lastPopGrowth;

            float neighborInfluence = (neighboringReligion.religion.influenceBonus + neighboringReligion.religionBonus) * neighboringReligion.religion.influenceMul;
            float influence = (tile.religion.influenceBonus + tile.religionBonus) * tile.religion.influenceMul;

            float influenceBalance = neighborInfluence / influence;

            float cultureBalance = neighboringReligion.culture.influenceBonus - tile.culture.influenceBonus;

            float governmentBonus = 0.0f;

            if (neighboringReligion.religion == neighboringReligion.government.rulingReligion)  governmentBonus += 1.0f;
            if (tile.religion == tile.government.rulingReligion)                                governmentBonus -= 1.0f;

            if(tile.government == neighboringReligion.government)                               governmentBonus += 0.25f;

            float religionBalance = (popBalance + growthBalance + influenceBalance + cultureBalance + governmentBonus) / 5.0f;

            if (Random.Range(0.0f, 1.0f) < religionBalance * 0.001f)
            {
                adoptedReligion = neighboringReligion;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        tile.religion.influenceMul -= 0.1f;
        tile.religion = adoptedReligion.religion;
        tile.religion.influenceMul += 0.15f;
    }
}

public class ReligiousMergeEvent : WS_BaseEvent
{
    public ReligiousMergeEvent() { eventName = "Religious merge"; module = EventModule.RELIGION; }

    List<WS_Tile> neighboringReligions = new List<WS_Tile>();
    WS_Tile mergedReligion = null;

    protected override bool FireCheck()
    {
        neighboringReligions.Clear();
        mergedReligion = null;

        if (!tile.religion.merged)
        {
            foreach (WS_Tile neighbor in tile.CulturedNeighbors())
            {
                if (neighbor.religion != tile.religion && !neighbor.religion.merged)
                    neighboringReligions.Add(neighbor);
            }
        }

        return neighboringReligions.Count > 1;
    }


    protected override bool SuccessCheck()
    {
        foreach (WS_Tile neighboringReligion in neighboringReligions)
        {
            float affinity = 0.0f;

            foreach (WS_Trait trait in tile.religion.traits)
                foreach (WS_Trait neighborTrait in neighboringReligion.religion.traits)
                {
                    if (trait == neighborTrait)
                        affinity += 1.0f;
                }

            affinity += tile.religion.syncretism + neighboringReligion.religion.syncretism;

            if (Random.Range(0.0f, 1.0f) < affinity * 0.001f)
            {
                mergedReligion = neighboringReligion;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        mergedReligion.religion.merged = true;
        tile.religion = new WS_Religion(tile.religion, mergedReligion.religion, tile);
        tile.religion.influenceMul = 2.5f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            if(neighbor.religion != null)
                if (neighbor.religion.capital != neighbor)
                    neighbor.religion = tile.religion;
    }
}

public class ReligiousEvolutionEvent : WS_BaseEvent
{
    public ReligiousEvolutionEvent() { eventName = "Religious Evolution"; module = EventModule.RELIGION; }

    protected override bool FireCheck()
    {
        return tile.religion.capital == tile;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < (0.001f / tile.religion.influenceMul);
    }

    protected override void Success()
    {
        tile.religion.changeRandomTrait(tile);
    }
}


public class ReligiousCollapseEvent : WS_BaseEvent
{
    public ReligiousCollapseEvent() { eventName = "Religious Collapse"; module = EventModule.RELIGION; }

    protected override bool FireCheck()
    {
        return tile.religion.capital == tile;
    }

    protected override bool SuccessCheck()
    {
        tile.religion.influenceMul = Mathf.Lerp(tile.religion.influenceMul, 1.0f, 0.1f);
        tile.religion.syncretism = Mathf.Lerp(tile.religion.syncretism, 5.0f, 0.1f);

        if (tile.culture.collapsed == 0)
            return 1.0f / Mathf.Pow(tile.religion.influenceMul, 2) > Random.Range(0.0f, 2000f);
        else
        {
            tile.religion.collapsed--;
            return false;
        }
    }

    protected override void Success()
    {
        tile.religion.syncretism += 30.0f;
        tile.religion.influenceMul = 0.1f;
        tile.religion.collapsed = Random.Range(10, 20);
    }
}
