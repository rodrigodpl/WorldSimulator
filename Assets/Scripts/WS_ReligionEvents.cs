using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//tribal
public class ReligiousBirthEvent : WS_BaseEvent
{
    public ReligiousBirthEvent() { eventName = "Religious Birth"; module = EventModule.RELIGION; }

    protected override bool FireCheck()
    {
        return tile.population > 50000.0f && tile.religion.tribal;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < tile.farmers * 0.0000001f;
    }

    protected override void Success()
    {
        tile.religion = new WS_Religion(tile.religion, tile);

        foreach (WS_Tile neighbor in tile.Neighbors())
            if (neighbor.religion != null)
                if (neighbor.religion.capital != neighbor && neighbor.religion.tribal)
                    neighbor.religion = tile.religion;
    }
}


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

            float influenceBalance = neighboringReligion.religion.influenceBonus - tile.religion.influenceBonus;
            influenceBalance += neighboringReligion.religionBonus - tile.religionBonus;

            float cultureBalance = neighboringReligion.culture.influenceBonus - tile.culture.influenceBonus;

            float religionBalance = (popBalance + growthBalance + influenceBalance + cultureBalance) / 4.0f;

            if (Random.Range(0.0f, 1.0f) < religionBalance * 0.0005f)
            {
                adoptedReligion = neighboringReligion;
                return true;
            }
        }

        return false;
    }

    protected override void Success()
    {
        tile.religion = adoptedReligion.religion;
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

        if (!tile.religion.tribal && !tile.religion.merged)
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
        return !tile.religion.tribal;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < 0.0001f;
    }

    protected override void Success()
    {
        tile.religion.changeRandomTrait(tile);
    }
}


public class ReligiousReformEvent : WS_BaseEvent
{
    public ReligiousReformEvent() { eventName = "Religious Reform"; module = EventModule.RELIGION; }

    protected override bool FireCheck()
    {
        return !tile.religion.tribal;
    }

    protected override bool SuccessCheck()
    {
        tile.religion.decadence += 0.0001f;

        if (tile.religion.capital == tile)
        {
            tile.religion.decadence -= 0.03f;

            if (tile.religion.decadence < 0.0f)
                tile.religion.decadence = 0.0f;

            return Random.Range(0.0f, 1.0f) < tile.religion.corruption * 0.0001f;
        }
        else
            return false;
    }

    protected override void Success()
    {
        tile.religion.syncretism += 20.0f;
        tile.religion.influenceBonus -= 7.0f;
        tile.religion.decadence = 0.0f;
    }
}
