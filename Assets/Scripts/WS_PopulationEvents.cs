using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerationEvent : WS_BaseEvent
{
    public FoodGenerationEvent() { eventName = "Food Generation"; module = EventModule.POPULATION; }

    protected override void Success()
    {
        float survivalismBonus = Mathf.Max(tile.mainCulture.survivalism - tile.habitability, 0.0f);

        float producedFood = tile.Population() * (1.0f - tile.urbanPercentile);
        producedFood *= 1.0f + ((tile.habitability + survivalismBonus - 50.0f) / 200.0f);
        producedFood *= tile.mainCulture.FoodEfficiency;
        
        tile.storedFood = (producedFood * 0.75f) - tile.Population();
        tile.nation.storedFood += (producedFood * 0.25f);
    }
}

public class FoodConsumptionEvent : WS_BaseEvent
{
    public FoodConsumptionEvent() { eventName = "Food Consumption"; module = EventModule.POPULATION; }

    protected override void Success()
    {
        float growthFactor = 0.0f;

        tile.storedFood += tile.nation.storedFood * (tile.Population() / tile.nation.population);

        growthFactor += (((tile.Population() + tile.storedFood) / tile.Population()) - 1.0f);
        growthFactor *= (tile.mainCulture.growthBonus / tile.mainCulture.mortalityRate);
        growthFactor = (growthFactor * 0.000003f) + 1.0f;

        float newPop = ((tile.Population() * growthFactor) - tile.Population()) * WS_World.frameMult;

        if (growthFactor < 1.0f)
        {
            tile.addPopulation(tile.mainCulture, 0.0f, newPop);
        }
        else
        {
            tile.urbanPercentile +=  tile.mainCulture.urbanization * newPop * Mathf.Min(tile.urbanPercentile, 0.1f) * WS_World.frameMult / 100000000.0f;
            
            if (tile.isFrontier)
                tile.settlerPercentile += newPop * WS_World.frameMult / tile.Population();

            tile.addPopulation(tile.mainCulture, newPop * (1.0f - tile.urbanPercentile), newPop * tile.urbanPercentile);
        }

        if (tile.urbanPercentile < 0.0f) tile.urbanPercentile = 0.0f;
        else if (tile.urbanPercentile > 1.0f) tile.urbanPercentile = 1.0f;

    }
}

public class MigrationEvent : WS_BaseEvent
{
    public MigrationEvent() { eventName = "Migration"; module = EventModule.POPULATION; }

    protected override void Success()
    {
        if (tile.Population() > tile.nation.population * 0.01f)
        {

            if (tile.settlement == Settlement.VILLAGE)
            {
                WS_Tile highestUrban = tile;

                foreach (WS_Tile neigbhor in tile.Neighbors())
                    if (neigbhor.urbanPercentile * neigbhor.Population() > highestUrban.urbanPercentile * highestUrban.Population())
                        highestUrban = neigbhor;

                if (highestUrban != tile)
                {
                    float migrants = highestUrban.Population() * WS_World.frameMult * 0.0000005f;

                    if (tile.Population() * 0.002f < migrants) migrants = tile.Population() * 0.002f;

                    highestUrban.addPopulation(tile.mainCulture, 0.0f, migrants);
                    tile.addPopulation(tile.mainCulture, 0.0f, -migrants);
                }
            }
            else
            {
                WS_Tile lowestRural = tile;

                foreach (WS_Tile neigbhor in tile.Neighbors())
                    if ((1.0f - neigbhor.urbanPercentile) * neigbhor.Population() < (1.0f - lowestRural.urbanPercentile) * lowestRural.Population() && neigbhor.Population() > 0.0f)
                        lowestRural = neigbhor;

                if (lowestRural != tile)
                {
                    float migrants = lowestRural.Population() * WS_World.frameMult * 0.0000005f;

                    if (tile.Population() * 0.002f < migrants) migrants = tile.Population() * 0.002f;

                    lowestRural.addPopulation(tile.mainCulture, migrants, 0.0f);
                    tile.addPopulation(tile.mainCulture, -migrants, 0.0f);
                }
            }
        }
        
    }
}
        

public class ColonizationEvent : WS_BaseEvent
{
    private WS_Tile destination = null;

    public ColonizationEvent() { eventName = "Colonization"; module = EventModule.POPULATION; }

    protected override bool FireCheck()
    {
        return (tile.settlerPercentile > tile.mainCulture.settlerCap / tile.mainCulture.expansionBonus &&
                tile.settlerPercentile > tile.mainCulture.nationSettlerCap);
    }

    protected override bool SuccessCheck()
    {
        foreach (WS_Tile neighbor in tile.Neighbors())
            if (neighbor.Population() == 0.0f && !neighbor.seaBody)
            {
                if (destination == null ? true : neighbor.habitability > destination.habitability)
                    destination = neighbor;
            }

        if (destination == null)
            return false;

        return Random.Range(0.0f, 1.0f) > ((destination.habitability + tile.mainCulture.survivalism) / 300.0f);
    }

    protected override void Success()
    {
        float settlers = tile.settlerPercentile / 100.0f * tile.Population();

        destination.addPopulation(tile.mainCulture, settlers * (1.0f - tile.urbanPercentile), settlers * tile.urbanPercentile, false);

        destination.nation = tile.nation;
        destination.mainCulture = tile.mainCulture;
        tile.nation.nationTiles.Add(destination);

        tile.settlerPercentile = 0.0f;
        tile.addPopulation(tile.mainCulture, -settlers * (1.0f - tile.urbanPercentile), -settlers * tile.urbanPercentile, false);
        destination = null;
    }

    protected override void Fail()
    {
        float settlers = tile.settlerPercentile / 100.0f * tile.Population();
        tile.settlerPercentile = 0.0f;
        tile.addPopulation(tile.mainCulture, -settlers * (1.0f - tile.urbanPercentile), -settlers * tile.urbanPercentile, false);
        destination = null;
    }
}



public class RuralMigrationsEvent : WS_BaseEvent
{

    public RuralMigrationsEvent() { eventName = "Rural Migrations"; module = EventModule.POPULATION; }

    protected override bool FireCheck()
    {
        return  (tile.nation.storedFood / 0.25f < tile.nation.population && tile.urbanPercentile > 0.3f);
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 100.0f) < (Mathf.Sqrt(WS_World.frameMult));
    }

    protected override void Success()
    {
        float intensity = Random.Range(0.1f, 0.25f);
        tile.urbanPercentile -= intensity;
    }

}


