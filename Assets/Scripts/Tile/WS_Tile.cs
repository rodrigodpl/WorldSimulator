using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Tile
{
    // Utility data
    public WS_TileUtility utility = null;
    public WS_TileRenderer tileRenderer = null;

    //Geographical Data
    public float altitude           = float.NaN;
    public float avgTemperature     = float.NaN;
    public float humidity           = float.NaN;
    public float riverStrength      = float.NaN;
    public float erosionStrength    = float.NaN;
    public float habitability       = float.NaN;
    public float airPressure        = float.NaN;

    public Biome biome = Biome.MAX_BIOMES;

    public WS_Tile erosionDirection = null;
    public List<WS_Tile> riverDirection =  new List<WS_Tile>();

    public bool seaBody = false;

    // Population Data
    public float population     = 0.0f;
    public int farmers          = 0;
    public int builders         = 0;
    public int traders          = 0;
    public int soldiers         = 0;
    public int scholars         = 0;
    public float foodUnits      = 0.0f;
    public float foodEfficiency = 1.1f;
    public float sanitation     = 30;
    public float healthcare     = -0.2f;
    public float lastPopGrowth  = 0.0f;
    public float storedFood     = 0.0f;

    // Culture Data
    public WS_Culture culture   = null;
    public float cultureBonus = 0.0f;

    // Disaster Data
    public WS_Disaster disaster = null;
    public int disasterDuration = 0;

    // Religion Data
    public WS_Religion religion = null;
    public float religionBonus = 0.0f;

    // Infrastructure Data
    public float infrastructurePoints = 0.0f;
    public int[] infrastructureLevels = new int[(int)InfrastructureType.MAX];
    public WS_Infrastructure plannedInfrastructure = null;
    public float constructionBonus = 0.0f;

    // Government Data
    public WS_Government government = null;
    public float unrest = 0.0f;
    public float unrestCultural = 0.0f;
    public float unrestReligious = 0.0f;
    public float unrestDecay = 0.8f;

    // War
    public float armyBonus = 1.0f;
    public float defenseBonus = 1.0f;

    // Commerce Data
    public float prosperity = 0.0f;
    public WS_ResourceSource resource = null;
    public WS_ResourceStack[] resStacks = new WS_ResourceStack[(int)ResourceType.MAX];
    public WS_CommerceCaravan caravan = null;
    public float qualityBonus = 1.0f;
    public float exploitationBonus = 1.0f;

    // Tech Data
    public WS_Tech currentTech = null;
    public float storedTechPoints = 0.0f;
    public List<WS_Tech> availableTech = new List<WS_Tech>();
    public List<WS_Tech> researchedTech = new List<WS_Tech>();

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

    public List<WS_Tile> CulturedNeighbors()     // Returns all neighbors with an assigned culture
    {
        List<WS_Tile> culturedNeighbors = new List<WS_Tile>();

        foreach (WS_Tile neighbor in utility.neighbors_1)
            if (neighbor.culture != null)
                culturedNeighbors.Add(neighbor);

        return culturedNeighbors;
    }

    public int DistanceTo(WS_Tile tile)   // distances between this tile and (tile) in array positions
    {
        Vector2Int position = utility.getPosition();
        Vector2Int position2 = tile.utility.getPosition();

        int distanceX = int.MaxValue;
        if (Mathf.Abs(position.x - position2.x) < distanceX) distanceX = Mathf.Abs(position.x - position2.x);
        if (Mathf.Abs((position.x + WS_World.sizeX) - position2.x) < distanceX) distanceX = Mathf.Abs((position.x + WS_World.sizeX) - position2.x);
        if (Mathf.Abs(position.x - (position2.x + WS_World.sizeX)) < distanceX) distanceX = Mathf.Abs(position.x - (position2.x + WS_World.sizeX));


        int distanceY = int.MaxValue;
        if (Mathf.Abs(position.y - position2.y) < distanceY) distanceY = Mathf.Abs(position.y - position2.y);
        if (Mathf.Abs((position.y + WS_World.sizeY) - position2.y) < distanceY) distanceY = Mathf.Abs((position.y + WS_World.sizeY) - position2.y);
        if (Mathf.Abs(position.y - (position2.y + WS_World.sizeY)) < distanceY) distanceY = Mathf.Abs(position.y - (position2.y + WS_World.sizeY));

        return distanceX + distanceY;
    }

    public bool IsCoastal()
    {
        foreach (WS_Tile neighbor in Neighbors())
            if (neighbor.seaBody)
                return true;

        return false;
    }

    public void ClearPopulation()
    {
        population = 0.0f;
        farmers = builders = traders = soldiers = scholars = 0;
        foodUnits = 0.0f;
        foodEfficiency = 1.1f;
        sanitation = 30;
        healthcare = -0.2f;
        unrestDecay = 0.8f;
        lastPopGrowth = 0.0f;
        storedFood = 0.0f;
        unrest = unrestCultural = unrestReligious = 0.0f;

        culture = null;
        cultureBonus = 0.0f;

        disaster = null;
        disasterDuration = 0;

        religion = null;
        religionBonus = 0.0f;

        infrastructurePoints = 0.0f;

        for (int i = 0; i < infrastructureLevels.Length; i++)
            infrastructureLevels[i] = 1;

        plannedInfrastructure = null;
        constructionBonus = 0.0f;

        government = null;

        prosperity = 0.0f;

        foreach (WS_ResourceStack stack in resStacks)
            stack.amount = 0;

        caravan = null;

        qualityBonus = 1.0f;
        exploitationBonus = 1.0f;

        armyBonus = 1.0f;
        defenseBonus = 1.0f;

        currentTech = null;
        storedTechPoints = 0.0f;
        availableTech.Clear();
        researchedTech.Clear();
    }

}
