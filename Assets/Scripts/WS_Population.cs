using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowthEvent : WS_BaseEvent
{
    public PopulationGrowthEvent() { eventName = "Pop Growth"; module = EventModule.POPULATION; }

    protected override void Success()
    {
        if(tile.population <= 0.0f)
        {
            tile.population = 0.0f;
            return;
        }

        // Habitability

        float habitability = tile.habitability;

        if (tile.habitability - 100.0f < 0.0f)
            habitability += Mathf.Min(tile.culture.survivalism, 100.0f - tile.habitability);

        float habitabilityBonus = 1.0f;

        // Spring

        float springValue = Random.Range(0.5f, 1.5f) * habitability;

        if (springValue < 60.0f)        habitabilityBonus -= 0.1f;
        else if (springValue < 80.0f)   habitabilityBonus += 0.0f;
        else if (springValue < 120.0f)  habitabilityBonus += 0.1f;
        else                            habitabilityBonus += 0.2f;

        // Winter

        float winterValue = Random.Range(0.5f, 1.5f) * habitability;

        if (winterValue < 60.0f)        habitabilityBonus -= 0.2f;
        else if (winterValue < 80.0f)   habitabilityBonus -= 0.1f;
        else if (winterValue < 120.0f)  habitabilityBonus -= 0.0f;
        else                            habitabilityBonus += 0.1f;

        // Healthcare

        float healthcareBonus = 1.0f + (tile.culture.healthcare * 0.01f) + (tile.sanitation * 0.01f);

        // Neighbors

        float neighborBonus = 0.0f;

        foreach (WS_Tile neighbor in tile.Neighbors())
            neighborBonus += neighbor.storedFood;

        neighborBonus /= 6;

        //Prosperity

        float prosperityBonus = 1.0f;
        // ============================= ADADADADADADADADADADADADADA!!!!!!

        // Growth

        tile.foodUnits = (tile.farmers * tile.culture.FoodEfficiency * habitabilityBonus) + neighborBonus;

        tile.storedFood = (tile.foodUnits - tile.population) * (tile.storage * 0.01f) * 0.01f;

        float popGrowth = 0;
        popGrowth += tile.population * (tile.foodUnits - tile.population);
        popGrowth += tile.population * (tile.sanitation - tile.population) * 0.5f;
        popGrowth *= 0.0001f;

        tile.lastPopGrowth = popGrowth;
        int lastPop = Mathf.FloorToInt(tile.population / 1000.0f);

        tile.population += popGrowth;

        // New Citizen 
        
        if(Mathf.FloorToInt(tile.population / 1000.0f) > lastPop)
        {
            tile.farmers++;

            // ========================================= ADADADADADADADADADA!!!!!!!!!!
        }

        // Lost Citizen

        else if (Mathf.FloorToInt(tile.population / 1000.0f) < lastPop)
        {
            tile.farmers--;


            // ========================================= ADADADADADADADADADA!!!!!!!!!!
        }


    }
}


public class ColonizationEvent : WS_BaseEvent
{
    public ColonizationEvent() { eventName = "Colonization"; module = EventModule.POPULATION; }

    List<WS_Tile> settlers = null;
    WS_Tile origin = null;

    protected override bool FireCheck()
    {
        settlers.Clear();
        origin = null;

        bool fire = false;
        if (tile.population <= 0.0f)
        {
            foreach (WS_Tile neighbor in tile.Neighbors())
            {
                if (neighbor.culture.survivalism + neighbor.culture.expansionism + tile.habitability > 90.0f)
                {
                    settlers.Add(neighbor);
                    fire = true;
                }
            }
        }

        return fire;
    }

    protected override bool SuccessCheck()
    {
        foreach(WS_Tile neighbor in settlers)
        {
            if(Random.Range(0.0f, 1.0f) < neighbor.lastCycleGrowth / tile.population)
            {
                origin = neighbor;
                return true;
            }
        }

        return false;
    }


    protected override void Success()
    {
        tile.population += origin.lastPopGrowth;
        origin.population -= origin.lastPopGrowth;

        tile.farmers++;
        tile.culture = origin.culture;

        settlers.Clear();
        origin = null;
    }

}