using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterTriggerEvent : WS_BaseEvent
{
    WS_Disaster triggeredDisaster = null;

    public DisasterTriggerEvent() { eventName = "Disaster Trigger"; module = EventModule.DISASTER; }

    protected override bool FireCheck()
    {
        return tile.disaster == null;
    }

    protected override bool SuccessCheck()
    {
        triggeredDisaster = null;

        float higherChance = 0.0f;

        foreach(WS_Disaster disaster in WS_World.disasters)
            if(disaster.Chance(tile) > higherChance)
            {
                higherChance = disaster.Chance(tile);
                triggeredDisaster = disaster;
            }

        return Random.Range(0.0f, 1.0f) < higherChance;
    }

    protected override void Success()
    {
        List<WS_Tile> affectedTiles = new List<WS_Tile>();

        affectedTiles.Add(tile);

        int areaOfEffect = triggeredDisaster.AreaOfEffect();

        if(areaOfEffect > 1)
        {
            foreach (WS_Tile neighbor in tile.Neighbors())
                if (tile.population > 0.0f && tile.disaster == null)
                    affectedTiles.Add(tile);

            for(int i = affectedTiles.Count; i > areaOfEffect; i--)
            {
                int index = Mathf.FloorToInt(Random.Range(1.0f, affectedTiles.Count - 0.01f));

                affectedTiles.RemoveAt(index);
            }
        }

        foreach(WS_Tile affectedTile in affectedTiles)
        {
            triggeredDisaster.Apply(affectedTile);
            affectedTile.disaster = triggeredDisaster;
        }
    }
}


public class DisasterEndEvent : WS_BaseEvent
{
    public DisasterEndEvent() { eventName = "Disaster End"; module = EventModule.DISASTER; }

    protected override bool FireCheck()
    {
        return tile.disaster != null;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < (tile.disasterDuration / (tile.disaster.AvgDuration() * 4.0f));
    }

    protected override void Success()
    {
        tile.disaster.Reverse(tile);
        tile.disaster = null;
        tile.disasterDuration = 0;
    }

    protected override void Fail()
    {
        tile.disasterDuration++;
    }
}


public class DisasterSpreadEvent : WS_BaseEvent
{
    WS_Disaster spreadDisaster = null;

    public DisasterSpreadEvent() { eventName = "Disaster Spread"; module = EventModule.DISASTER; }

    protected override bool FireCheck()
    {
        return tile.disaster == null;
    }

    protected override bool SuccessCheck()
    {
        spreadDisaster = null;

        float higherChance = 0.0f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            if (neighbor.disaster != null)
                if (neighbor.disaster.SpreadChance(tile) > higherChance)
                {
                    higherChance = neighbor.disaster.SpreadChance(tile);
                    spreadDisaster = neighbor.disaster;
                }

        return Random.Range(0.0f, 1.0f) < higherChance;
    }

    protected override void Success()
    {
        spreadDisaster.Apply(tile);
        tile.disaster = spreadDisaster;
    }
}