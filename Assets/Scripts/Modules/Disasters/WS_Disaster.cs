using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisasterType { NONE, DROUGHT, FLOOD, TSUNAMI, EARTHQUAKE, PLAGUE, FIRE }

public class WS_Disaster
{
    public virtual DisasterType Type() { return DisasterType.NONE; }

    public virtual int AreaOfEffect() { return 1; }
    public virtual int AvgDuration() { return 10; }

    public virtual void Apply(WS_Tile tile) { }
    public virtual void Reverse(WS_Tile tile) { }

    public virtual float SpreadChance(WS_Tile tile) { return 0.0f; }
    public virtual float Chance(WS_Tile tile) { return 0.0f; }
}


public class WS_DroughtDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.DROUGHT; }

    override public void Apply(WS_Tile tile) { tile.habitability -= 40.0f; tile.prosperity *= 0.9f; tile.unrest += 7.5f; }
    override public void Reverse(WS_Tile tile) { tile.habitability += 40.0f; }

    override public float Chance(WS_Tile tile)
    {
        return ((1000.0f - tile.airPressure) + (tile.avgTemperature - 21.0f)) * 0.00001f;
    }
}


public class WS_FloodDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.FLOOD; }

    override public void Apply(WS_Tile tile) { tile.habitability -= 40.0f; tile.infrastructurePoints -= 10.0f; tile.prosperity *= 0.95f; tile.unrest += 5.0f; }
    override public void Reverse(WS_Tile tile) { tile.habitability += 40.0f; }

    override public float Chance(WS_Tile tile)
    {
        return (tile.humidity - 85.0f) * 0.0001f;
    }
}

public class WS_EarthQuakeDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.EARTHQUAKE; }

    override public void Apply(WS_Tile tile) { tile.population *= 0.85f; tile.infrastructurePoints = 0.0f; tile.prosperity *= 0.7f; tile.unrest += 30.0f; }
    override public void Reverse(WS_Tile tile) { }
    public override int AvgDuration() { return 1; }

    override public float Chance(WS_Tile tile)
    {
        return 0.0001f;
    }
}


public class WS_TsunamiDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.TSUNAMI; }

    override public void Apply(WS_Tile tile) { tile.sanitation -= 25; tile.infrastructurePoints -= 30.0f; tile.prosperity *= 0.9f; tile.unrest += 5.0f; }
    override public void Reverse(WS_Tile tile) { tile.sanitation += 25; }

    public override int AreaOfEffect() { return (int)Random.Range(3.0f, 6.0f); }

    override public float Chance(WS_Tile tile)
    {
        float chance = 0.0f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            if (neighbor.seaBody)
                chance += 0.0001f;

        return chance;
    }
}

public class WS_PlagueDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.PLAGUE; }

    override public void Apply(WS_Tile tile) { tile.sanitation -= 30; tile.unrest += 5.0f; }
    override public void Reverse(WS_Tile tile) { tile.sanitation += 30; }

    public override int AreaOfEffect() { return (int)Random.Range(1.0f, 3.0f); }
    public override int AvgDuration() { return 15; }

    override public float Chance(WS_Tile tile)
    {
        return 0.00001f * ((tile.population / 1000.0f) - tile.sanitation);
    }

    override public float SpreadChance(WS_Tile tile)
    {
        return 0.0003f * ((tile.population / 1000.0f) - (tile.sanitation * 0.2f));
    }
}

public class WS_FireDisaster : WS_Disaster
{
    public override DisasterType Type() { return DisasterType.FIRE; }

    override public void Apply(WS_Tile tile) { tile.population *= 0.99f; tile.unrest += 5.0f; tile.prosperity *= 0.9f; }
    override public void Reverse(WS_Tile tile) { }

    public override int AreaOfEffect() { return (int)Random.Range(1.0f, 3.0f); }
    public override int AvgDuration() { return 5; }

    override public float Chance(WS_Tile tile)
    {
        float chance = 0.0f;
        switch(tile.biome)
        {
            case Biome.ALPINE_FOREST: chance = 1.0f; break;
            case Biome.BOREAL_FOREST: chance = 1.0f; break;
            case Biome.TEMPERATE_FOREST: chance = 1.5f; break;
            case Biome.TEMPERATE_SHRUBLAND: chance =  2.0f; break;
        }

        return 0.00005f * chance;
    }

    override public float SpreadChance(WS_Tile tile)
    {
        float chance = 0.0f;
        switch (tile.biome)
        {
            case Biome.ALPINE_FOREST: chance = 1.0f; break;
            case Biome.BOREAL_FOREST: chance = 1.0f; break;
            case Biome.TEMPERATE_FOREST: chance = 2.0f; break;
            case Biome.TEMPERATE_SHRUBLAND: chance = 3.0f; break;
        }

        return 0.001f * chance;
    }
}