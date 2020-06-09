using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowthEvent : WS_BaseEvent
{
    public PopulationGrowthEvent() { eventName = "Pop Growth"; module = EventModule.POPULATION; }

    protected override void Success()
    {
        // Habitability

        float habitability = tile.habitability;

        if (tile.habitability - 100.0f < 0.0f)
            habitability += Mathf.Min(tile.culture.survivalism, 100.0f - tile.habitability);

        float habitabilityBonus = 1.0f;

        // Spring

        float springValue = Random.Range(0.5f, 1.5f) * habitability;

        if (springValue < 60.0f)        habitabilityBonus -= 0.3f;
        else if (springValue < 80.0f)   habitabilityBonus -= 0.15f;
        else if (springValue < 120.0f)  habitabilityBonus += 0.15f;
        else                            habitabilityBonus += 0.3f;

        // Winter

        float winterValue = Random.Range(0.5f, 1.5f) * habitability;

        if (winterValue < 60.0f)        habitabilityBonus -= 0.3f;
        else if (winterValue < 80.0f)   habitabilityBonus -= 0.15f;
        else if (winterValue < 120.0f)  habitabilityBonus += 0.15f;
        else                            habitabilityBonus += 0.3f;

        // Healthcare

        float healthcareBonus = 1.0f;

        if (tile.sanitation / (tile.population / 1000.0f) < 1.0f)
            healthcareBonus = tile.sanitation / (tile.population / 1000.0f);

        healthcareBonus += tile.healthcare + tile.culture.healthcare * 0.01f;
        
        // Neighbors

        float neighborBonus = 0.0f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            neighborBonus += neighbor.storedFood;

        neighborBonus /= 6;


        // Growth

        tile.foodUnits = tile.farmers * tile.foodEfficiency * tile.culture.FoodEfficiency * habitabilityBonus * healthcareBonus;
        tile.foodUnits += neighborBonus;
        tile.foodUnits *= 1000.0f;

        tile.storedFood = (tile.foodUnits - tile.population) * (tile.storage * 0.01f) * 0.001f;

        float popGrowth = (tile.foodUnits - tile.population) * 0.01f;


        // avoid future divisions by zero
        if (popGrowth != 0.0f)
            tile.lastPopGrowth = popGrowth;
        else
            tile.lastPopGrowth = -0.01f;


        if (popGrowth < WS_World.minGrowth)         WS_World.minGrowth = popGrowth;
        if (popGrowth > WS_World.maxGrowth)         WS_World.maxGrowth = popGrowth;


        int lastPop = Mathf.CeilToInt(tile.population / 1000.0f);

        tile.population += popGrowth;

        // New Citizen 
        for (int i = 0; i < Mathf.CeilToInt(tile.population / 1000.0f) - lastPop; i++)
        {
            if(Random.Range(0.0f, 1.0f) < tile.farmers / (tile.population / 1000.0f) * 0.2f)
            {
                tile.builders++;
                continue;
            }
            tile.farmers++;
        }

        // Lost Citizen
        for (int i = 0; i < lastPop - Mathf.CeilToInt(tile.population / 1000.0f); i++)
        {
            if(tile.builders > 0)
            {
                tile.builders--;
                continue;
            }
            tile.farmers--;
        }

        if (tile.population <= 0.0f)
        {
            tile.population = 0.0f;
            tile.disaster = null;
            tile.culture = null;
            tile.nation = null;
            tile.storedFood = 0.0f;
        }

    }
}


public class ColonizationEvent : WS_BaseEvent
{
    public ColonizationEvent() { eventName = "Colonization"; module = EventModule.POPULATION; }

    List<WS_Tile> possibleColonization = new List<WS_Tile>();
    WS_Tile dest = null;

    protected override bool FireCheck()
    {
        possibleColonization.Clear();
        dest = null;

        bool fire = false;

        foreach (WS_Tile neighbor in tile.Neighbors())
        {
            if (neighbor.population <= 0.0f)
            {
                if (tile.culture.survivalism + tile.culture.expansionism + neighbor.habitability > 80.0f)
                {
                    possibleColonization.Add(neighbor);
                    fire = true;
                }
            }
        }


        return fire;
    }

    protected override bool SuccessCheck()
    {
        foreach(WS_Tile neighbor in possibleColonization)
        {
            if(Random.Range(0.0f, 1.0f) < tile.lastPopGrowth * 0.000003f)
            {
                dest = neighbor;
                return true;
            }
        }

        return false;
    }


    protected override void Success()
    {
        tile.population -= tile.lastPopGrowth;
        dest.population += tile.lastPopGrowth;

        dest.farmers++;
        dest.culture = tile.culture;
        dest.religion = tile.religion;
    }

}