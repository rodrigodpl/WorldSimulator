using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Settlement {  VILLAGE, TOWN, CITY }

public class River
{
    public List<WS_Tile> path;
    public string name = "";
}

public class WS_Tile
{
    // Utility data
    public bool isCoastal = false;
    public bool isFrontier = false;
    public bool isBorder = false;

    public WS_TileUtility utility = null;
    public WS_TileRenderer tileRenderer = null;

    //Geographical Data
    public float altitude = float.NaN;
    public float avgTemperature = float.NaN;
    public float humidity = float.NaN;
    public float riverStrength = float.NaN;
    public float erosionStrength = float.NaN;
    public float habitability = float.NaN;
    public float airPressure = float.NaN;

    public Biome biome = Biome.MAX_BIOMES;
    public List<WS_Resource> Resources = new List<WS_Resource>();
    //public GeographicalRegion;

    public WS_Tile erosionDirection = null;
    public List<WS_Tile> riverDirection =  new List<WS_Tile>();

    public bool seaBody = false;

    // Population Data
    public float storedFood = 0.0f;
    private float population = 0;
    public float urbanPercentile = 0;
    public float settlerPercentile = 0.0f;
    public float lastCycleGrowth = 0.0f;
    public float lastCycleNewPop = 0.0f;

    public Settlement settlement = Settlement.VILLAGE;

    public WS_Nation nation = null;

    // Culture Data
    public WS_Culture mainCulture = null;
    public float cultureStrength = 0.0f;
    public List<WS_Culture> cultures = new List<WS_Culture>();
    public List<float> culturePopulations = new List<float>();
    
    public float Population() { return population; }

    public void Populate(float _population) { population = _population; }

    public void addPopulation(WS_Culture migratingCulture, float ruralPop, float urbanPop, bool growth = true)
    {
        urbanPercentile = (population * urbanPercentile + urbanPop) / (population + ruralPop);
        population += ruralPop + urbanPop;

        if(growth)
            lastCycleNewPop += ruralPop + urbanPop;

        bool found = false;
        foreach(WS_Culture culture in cultures)
        {
            if(culture == migratingCulture)
            {
                culturePopulations[cultures.IndexOf(culture)] += ruralPop + urbanPop;
                found = true;
                break;
            }
        }

        if(!found)
        {
            cultures.Add(migratingCulture);
            culturePopulations.Add(ruralPop + urbanPop);
        }
    }

    public void addInfluence(WS_Culture influencingCulture, float strength)
    {
        int index = cultures.IndexOf(influencingCulture);
        if (index != -1)
        {
            culturePopulations[index] += strength;

            for (int i = 0; i < culturePopulations.Count; i++)
                if (i != index)
                    culturePopulations[i] -= strength / (culturePopulations.Count - 1.0f);
        }
        
    }

    public void updateData()
    {

        switch(settlement)
        {
            case Settlement.VILLAGE:    if (urbanPercentile > 0.4f) settlement = Settlement.TOWN; break;

            case Settlement.TOWN:       if (urbanPercentile < 0.3f) settlement = Settlement.VILLAGE;
                                        if (urbanPercentile > 0.7f) settlement = Settlement.CITY; break;

            case Settlement.CITY:       if (urbanPercentile < 0.6f) settlement = Settlement.TOWN; break;

        }

        lastCycleGrowth = lastCycleNewPop / population;

        if (lastCycleGrowth < WS_World.minGrowth) WS_World.minGrowth = lastCycleGrowth;
        else if (lastCycleGrowth > WS_World.maxGrowth) WS_World.maxGrowth = lastCycleGrowth;

        foreach (WS_Tile neighbor in Neighbors())
        {
            if (neighbor.seaBody) isCoastal = true;
            if (neighbor.population > 0.0f && neighbor.nation != nation) isBorder = true;
            if (neighbor.habitability + (mainCulture != null ? mainCulture.survivalism : 0.0f) > 10.0f && neighbor.population == 0.0f && !neighbor.seaBody) isFrontier = true;
        }

        for (int i = 0; i < cultures.Count; i++)
        {
            if (culturePopulations[i] > culturePopulations[cultures.IndexOf(mainCulture)])
                mainCulture = cultures[i];
            else if(culturePopulations[i] < 0.0f)
            {
                culturePopulations.RemoveAt(i);
                cultures.RemoveAt(i);
            }
        }

        if (settlement == Settlement.CITY) cultureStrength = 4.0f;
        else if (settlement == Settlement.TOWN) cultureStrength = 3.0f;
        else cultureStrength = 2.0f;

        if (mainCulture != null)
        {
            if (mainCulture.birthBonus > 0.1f)
            {
                cultureStrength += mainCulture.birthBonus;
                mainCulture.birthBonus *= 0.99f;
            }

            float strengthMultiplier = mainCulture.influence;
            strengthMultiplier += utility.DistanceTo(nation.capital.utility) / -20.0f;

            cultureStrength *= strengthMultiplier;
        }

        lastCycleNewPop = 0.0f;
    }


    public List<WS_Tile> Neighbors(int radius = 1)     // Returns, or loads if unloaded, the tile's neigbors in (radius)
    {
        switch (radius)
        {
            case 1: if (utility.neighbors_1.Count == 0) utility.LoadNeighbors1(); return utility.neighbors_1;
            case 2: if (utility.neighbors_2.Count == 0) utility.LoadNeighbors2(); return utility.neighbors_2;
            case 3: if (utility.neighbors_3.Count == 0) utility.LoadNeighbors3(); return utility.neighbors_3;
            default: return null;
        }
    }
    
    

}
