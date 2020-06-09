using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfrastructureType { SANITATION, FOOD, HEALTHCARE, DECADENCE, CULTURE, RELIGION, CONSTRUCTION, MAX}

public class WS_Infrastructure 
{
    public int baseCost = 0;
    public InfrastructureType type = InfrastructureType.MAX;

    public string name1_3 = "";
    public string name4_6 = "";
    public string name7_9 = "";
    public string nameWonder = "";

    public float Cost(WS_Tile tile, int upgradeLevel) 
    {
        return ((tile.population / 1000.0f) * 0.5f) * (baseCost * upgradeLevel);
    }

    public virtual float Chance(WS_Tile tile) { return 0.0f; }
    public virtual void Apply(WS_Tile tile, int upgradeLevel) {}
    public virtual void Reverse(WS_Tile tile, int upgradeLevel) {}

}

public class WS_SanitationInfrastructure : WS_Infrastructure
{
    public WS_SanitationInfrastructure()
    {
        name1_3 = "Aqueduct";
        name4_6 = "Canals";
        name7_9 = "Waterworks";
        nameWonder = "Great Sewers";

        type = InfrastructureType.SANITATION;
        baseCost = 7;
    }

    public override float Chance(WS_Tile tile)
    {
        return (tile.population / 1000.0f) - tile.sanitation;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.sanitation += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.SANITATION], 2) * 5.0f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.sanitation -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.SANITATION], 2) * 5.0f;
    }
}

public class WS_FoodInfrastructure : WS_Infrastructure
{
    public WS_FoodInfrastructure()
    {
        name1_3 = "Irrigation";
        name4_6 = "Pens";
        name7_9 = "Orchards";
        nameWonder = "Hanging Garden";

        type = InfrastructureType.FOOD;
        baseCost = 7;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency += (Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.FOOD], 2) * 0.01f);
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency -= (Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.FOOD], 2) * 0.01f);
    }
}

public class WS_HealthcareInfrastructure : WS_Infrastructure
{
    public WS_HealthcareInfrastructure()
    {
        name1_3 = "Herbalist";
        name4_6 = "Physician";
        name7_9 = "Hospital";
        nameWonder = "Royal Hospital";

        type = InfrastructureType.HEALTHCARE;
        baseCost = 8;
    }

    public override float Chance(WS_Tile tile)
    {
        if (tile.lastPopGrowth < 0)
            return Mathf.Sqrt(Mathf.Sqrt(-tile.lastPopGrowth));
        else
            return 0;
    }
    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency += tile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE] * 0.03f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency -= tile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE] * 0.03f;
    }
}

public class WS_DecadenceInfrastructure : WS_Infrastructure
{
    public WS_DecadenceInfrastructure()
    {
        name1_3 = "Magistrate";
        name4_6 = "Governor";
        name7_9 = "Administrator";
        nameWonder = "Monumental Parliament";

        type = InfrastructureType.DECADENCE;
        baseCost = 8;
    }

    public override float Chance(WS_Tile tile)
    {
        return tile.culture.decadence + tile.religion.decadence;
    }
}


public class WS_CultureInfrastructure : WS_Infrastructure
{
    public WS_CultureInfrastructure()
    {
        name1_3 = "Sculptor";
        name4_6 = "Paint School";
        name7_9 = "Opera house";
        nameWonder = "Art College";

        type = InfrastructureType.CULTURE;
        baseCost = 10;
    }

    public override float Chance(WS_Tile tile)
    {
        if (tile.culture.influenceBonus < 0)
            return Mathf.Sqrt(-tile.culture.influenceBonus);
        else
            return 1.0f / (tile.culture.influenceBonus + 0.5f);
    }
}


public class WS_ReligionInfrastructure : WS_Infrastructure
{
    public WS_ReligionInfrastructure()
    {
        name1_3 = "Temple";
        name4_6 = "Church";
        name7_9 = "Cathedral";
        nameWonder = "Golden Basilica";

        type = InfrastructureType.RELIGION;
        baseCost = 10;
    }

    public override float Chance(WS_Tile tile)
    {
        if (tile.religion.influenceBonus < 0)
            return Mathf.Sqrt(-tile.religion.influenceBonus);
        else
            return 1.0f / (tile.religion.influenceBonus + 0.5f);
    }
}

public class WS_ConstructionInfrastructure : WS_Infrastructure
{
    public WS_ConstructionInfrastructure()
    {
        name1_3 = "Sawing Mill";
        name4_6 = "Architect";
        name7_9 = "Quarry";
        nameWonder = "Builder's Guildhall";

        type = InfrastructureType.CONSTRUCTION;
        baseCost = 10;
    }

    public override float Chance(WS_Tile tile)
    {
        float chance = 0.0f;

        for (int i = 0; i < tile.infrastructureLevels.Length; i++)
            chance += 10 - tile.infrastructureLevels[i];

        chance /= tile.infrastructureLevels.Length;

        return chance * 0.1f;
    }
}