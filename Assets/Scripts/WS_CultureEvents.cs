﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CultureTraits;

//tribal
public class CultureBirthEvent : WS_BaseEvent
{
    public CultureBirthEvent() { eventName = "Culture Birth"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return tile.population > 50000.0f && tile.culture.tribal;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < tile.farmers * 0.00001f;
    }

    protected override void Success()
    {
        tile.culture = new WS_Culture(tile.culture, tile);
    }
}


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

            float influenceBalance = neighboringCulture.culture.influenceBonus - tile.culture.influenceBonus;

            float halfDistanceTile = tile.DistanceTo(tile.culture.capital) * 0.5f; 
            float halfDistanceNeighbor = neighboringCulture.DistanceTo(neighboringCulture.culture.capital) * 0.5f;


            float cultureBalance = (popBalance + growthBalance + influenceBalance + (halfDistanceTile - halfDistanceNeighbor)) / 4.0f;

            if(Random.Range(0.0f, 1.0f) < cultureBalance * 0.001f)
            {
                adoptedCulture = neighboringCulture;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        tile.culture = adoptedCulture.culture;
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

        if (!tile.culture.tribal && !tile.culture.merged)
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

            foreach(WS_CultureTrait trait in tile.culture.traits)
                foreach(WS_CultureTrait neighborTrait in neighboringCulture.culture.traits)
                {
                    if (trait == neighborTrait)
                        affinity += 1.0f;
                }

            affinity += tile.culture.syncretism + neighboringCulture.culture.syncretism;

            if (Random.Range(0.0f, 1.0f) < affinity * 0.02f)
            {
                mergedCulture = neighboringCulture;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        tile.culture = new WS_Culture(tile.culture, mergedCulture.culture, tile);
    }
}

public class CulturalEvolutionEvent : WS_BaseEvent
{
    public CulturalEvolutionEvent() { eventName = "Cultural Evolution"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return !tile.culture.tribal;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < 0.0001f;
    }

    protected override void Success()
    {
        tile.culture.changeRandomTrait(tile);
    }
}


public class CulturalCollapseEvent : WS_BaseEvent
{
    public CulturalCollapseEvent() { eventName = "Cultural Collapse"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return !tile.culture.tribal && tile.nation != null;
    }

    protected override bool SuccessCheck()
    {
        if (tile.nation.nationTiles.Count > 30)
            tile.culture.decadence += (tile.nation.nationTiles.Count - 30) * 0.01f;

        return Random.Range(0.0f, 1.0f) < tile.culture.decadence * 0.01f;
    }

    protected override void Success()
    {
        tile.culture.syncretism += 20.0f;
        tile.culture.influenceBonus -= 7.0f;
    }
}
