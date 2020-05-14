using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisasterType { NONE, DROUGHT, FLOOD, TSUNAMI, PLAGUE }

public class WS_BaseDisaster
{
    public virtual DisasterType Type() { return DisasterType.NONE; }

    public virtual int AreaOfEffect() { return 1; }
    public virtual int AvgDuration() { return 10; }

    public virtual void Apply(WS_Tile tile) { }
    public virtual void Reverse(WS_Tile tile) { }

    public virtual float SpreadChance(WS_Tile tile) { return 0.0f; }
    public virtual float Chance(WS_Tile tile) { return 0.0f; }
}


public class DroughtDisaster : WS_BaseDisaster
{
    public override DisasterType Type() { return DisasterType.DROUGHT; }

    override public void Apply(WS_Tile tile) { tile.habitability -= 40.0f; }
    override public void Reverse(WS_Tile tile) { tile.habitability += 40.0f; }

    override public float Chance(WS_Tile tile)
    {
        return ((1000.0f - tile.airPressure) + (tile.avgTemperature - 21.0f)) * 0.00001f;
    }
}


public class FloodDisaster : WS_BaseDisaster
{
    public override DisasterType Type() { return DisasterType.FLOOD; }

    override public void Apply(WS_Tile tile) { tile.habitability -= 40.0f; }
    override public void Reverse(WS_Tile tile) { tile.habitability += 40.0f; }

    override public float Chance(WS_Tile tile)
    {
        return (tile.humidity - 85.0f) * 0.0001f;
    }
}


public class TsunamiDisaster : WS_BaseDisaster
{
    public override DisasterType Type() { return DisasterType.TSUNAMI; }

    override public void Apply(WS_Tile tile) { tile.sanitation -= 25; }
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

public class PlagueDisaster : WS_BaseDisaster
{
    public override DisasterType Type() { return DisasterType.PLAGUE; }

    override public void Apply(WS_Tile tile) { tile.sanitation -= 30; }
    override public void Reverse(WS_Tile tile) { tile.sanitation += 30; }

    public override int AreaOfEffect() { return (int)Random.Range(1.0f, 3.0f); }
    public override int AvgDuration() { return 20; }

    override public float Chance(WS_Tile tile)
    {
        return 0.00001f * ((tile.population / 1000.0f) - tile.sanitation);
    }

    override public float SpreadChance(WS_Tile tile)
    {
        return 0.001f * ((tile.population / 1000.0f) - (tile.sanitation * 0.3f));
    }
}