﻿using System.Collections;
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
            habitability += Mathf.Min(tile.culture.expansionism, 100.0f - tile.habitability);

        float habitabilityBonus = 1.0f;

        // Spring

        float springValue = Random.Range(0.5f, 1.5f) * habitability;

        if (springValue < 60.0f)        habitabilityBonus -= 0.2f;
        else if (springValue < 80.0f)   habitabilityBonus -= 0.1f;
        else if (springValue < 120.0f)  habitabilityBonus += 0.2f;
        else                            habitabilityBonus += 0.3f;

        // Winter

        float winterValue = Random.Range(0.5f, 1.5f) * habitability;

        if (winterValue < 60.0f)        habitabilityBonus -= 0.2f;
        else if (winterValue < 80.0f)   habitabilityBonus -= 0.1f;
        else if (winterValue < 120.0f)  habitabilityBonus += 0.1f;
        else                            habitabilityBonus += 0.2f;

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

        // Prosperity bonus
        float prosperityBonus = Mathf.Sqrt(Mathf.Sqrt(tile.prosperity / tile.population));
        prosperityBonus = (prosperityBonus + tile.resFoodBonus) / 2.0f;

        // Growth

        tile.foodUnits = (tile.farmers + tile.baseCitizens) * tile.foodEfficiency * tile.culture.FoodEfficiency * habitabilityBonus * healthcareBonus * prosperityBonus;
        tile.foodUnits += neighborBonus;
        tile.foodUnits *= 1000.0f;

        tile.storedFood = (tile.foodUnits - tile.population) * 0.0005f;

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
            float selector = Random.Range(0.0f, 1.0f);

            if (selector < 0.15f)       tile.soldiers++;
            else if (selector < 0.30f)  tile.builders++;
            else if (selector < 0.45f)  tile.traders++;
            else if (selector < 0.55f)  tile.scholars++;
            else                        tile.farmers++;
        }

        // Lost Citizen
        for (int i = 0; i < lastPop - Mathf.CeilToInt(tile.population / 1000.0f); i++)
        {
            if (tile.scholars > 0)      tile.scholars--;
            else if (tile.builders > 0) tile.builders--;
            else if (tile.traders > 0)  tile.traders--;
            else if (tile.soldiers > 0) tile.soldiers--;
            else                        tile.farmers--;
        }

        if (tile.population <= 0.0f)
            tile.ClearPopulation();

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

        if (tile.lastPopGrowth < 0.0f)
        {
            foreach (WS_Tile neighbor in tile.Neighbors())
            {
                if (neighbor.population <= 0.0f)
                {
                    if (tile.culture.expansionism + neighbor.habitability > 80.0f)
                    {
                        possibleColonization.Add(neighbor);
                        fire = true;
                    }
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

        dest.farmers = Mathf.Max(1,(int)tile.lastPopGrowth / 1000);
        dest.culture = tile.culture;
        dest.religion = tile.religion;
        dest.government = tile.government;

        dest.availableTech = tile.availableTech;

        foreach(WS_Tech tech in tile.researchedTech)
        {
            tech.Apply(dest);
            dest.researchedTech.Add(tech);
        }
    }

}