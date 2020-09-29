using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CulturalAdoptionEvent : WS_BaseEvent
{
    public CulturalAdoptionEvent() { eventName = "Culture Adoption"; module = EventModule.CULTURE; }

    List<WS_Tile> neighboringCultures = new List<WS_Tile>();
    WS_Tile adoptedCulture = null;

    protected override bool FireCheck()
    {
        neighboringCultures.Clear();
        adoptedCulture = null;

        foreach (WS_Tile neighbor in tile.CulturedNeighbors())
        {
            if (neighbor.culture != tile.culture)
                neighboringCultures.Add(neighbor);
        }

        return neighboringCultures.Count > 1;
    }

    protected override bool SuccessCheck()
    {
        foreach(WS_Tile neighboringCulture in neighboringCultures)
        {
            float popBalance = neighboringCulture.population / tile.population;

            float growthBalance = neighboringCulture.lastPopGrowth / tile.lastPopGrowth;

            float neighborInfluence = (neighboringCulture.culture.influenceBonus + neighboringCulture.cultureBonus) * neighboringCulture.culture.influenceMul;
            float influence = (tile.culture.influenceBonus + tile.cultureBonus) * tile.culture.influenceMul;

            float influenceBalance = neighborInfluence / influence;

            float governmentBonus = 0.0f;

            if (neighboringCulture.culture == neighboringCulture.government.rulingCulture)  governmentBonus += 1.0f;
            if (tile.culture == tile.government.rulingCulture)                              governmentBonus -= 1.0f;

            if (tile.government == neighboringCulture.government)                           governmentBonus += 0.25f;
            // Prosperity bonus
            float prosperityBonus = (neighboringCulture.prosperity / neighboringCulture.population) / (tile.prosperity / tile.population);

            float cultureBalance = (popBalance + growthBalance + influenceBalance + governmentBonus + prosperityBonus) / 5.0f;

            if(Random.Range(0.0f, 1.0f) < cultureBalance * 0.002f)
            {
                adoptedCulture = neighboringCulture;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        tile.culture.influenceMul -= 0.1f;
        tile.culture = adoptedCulture.culture;
        tile.culture.influenceMul += 0.15f;
    }
}

public class CulturalMergeEvent : WS_BaseEvent
{
    public CulturalMergeEvent() { eventName = "Cultural merge"; module = EventModule.CULTURE; }

    List<WS_Tile> neighboringCultures = new List<WS_Tile>();
    WS_Tile mergedCulture = null;

    protected override bool FireCheck()
    {
        neighboringCultures.Clear();
        mergedCulture = null;

        if (!tile.culture.merged)
        {
            foreach (WS_Tile neighbor in tile.CulturedNeighbors())
            {
                if (neighbor.culture != tile.culture && !neighbor.culture.merged)
                    neighboringCultures.Add(neighbor);
            }
        }

        return neighboringCultures.Count > 1;
    }


    protected override bool SuccessCheck()
    {
        foreach (WS_Tile neighboringCulture in neighboringCultures)
        {
            float affinity = 0.0f;

            foreach(WS_Trait trait in tile.culture.traits)
                foreach(WS_Trait neighborTrait in neighboringCulture.culture.traits)
                {
                    if (trait == neighborTrait)
                        affinity += 1.0f;
                }

            affinity += tile.culture.syncretism + neighboringCulture.culture.syncretism;

            if (Random.Range(0.0f, 1.0f) < affinity * 0.001f)
            {
                mergedCulture = neighboringCulture;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        mergedCulture.culture.merged = true;
        tile.culture = new WS_Culture(tile.culture, mergedCulture.culture, tile);
        tile.culture.influenceMul = 2.5f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            if(neighbor.culture != null)
                if (neighbor.culture.capital != neighbor)
                    neighbor.culture = tile.culture;

    }
}

public class CulturalEvolutionEvent : WS_BaseEvent
{
    public CulturalEvolutionEvent() { eventName = "Cultural Evolution"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return tile.culture.capital == tile;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < (0.001f / tile.culture.influenceMul);
    }

    protected override void Success()
    {
        tile.culture.changeRandomTrait(tile);
        tile.culture.influenceMul = 2.0f;
    }
}


public class CulturalCollapseEvent : WS_BaseEvent
{
    public CulturalCollapseEvent() { eventName = "Cultural Collapse"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return tile.culture.capital == tile;
    }

    protected override bool SuccessCheck()
    {
        tile.culture.influenceMul = Mathf.Lerp(tile.culture.influenceMul, 1.0f, 0.1f);
        tile.culture.syncretism = Mathf.Lerp(tile.culture.syncretism, 5.0f, 0.1f);

        if(tile.culture.collapsed == 0)
            return 1.0f / Mathf.Pow(tile.culture.influenceMul, 2) > Random.Range(0.0f, 2000f);
        else
        {
            tile.culture.collapsed--;
            return false;
        }
    }

    protected override void Success()
    {
        tile.culture.syncretism += 30.0f;
        tile.culture.influenceMul = 0.1f;
        tile.culture.collapsed = Random.Range(10, 20);
    }
}
