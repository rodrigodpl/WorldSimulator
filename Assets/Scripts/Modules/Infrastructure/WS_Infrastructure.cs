using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfrastructureType { SANITATION, FOOD, HEALTHCARE, UNREST, CULTURE, RELIGION, CONSTRUCTION, WAR, MAX}

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
        baseCost = 10;
    }

    public override float Chance(WS_Tile tile)
    {
        return (tile.population / 500.0f) - tile.sanitation;
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
        baseCost = 10;
    }

    public override float Chance(WS_Tile tile)
    {
        return (tile.population - tile.foodUnits) * 0.01f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.FOOD], 2) * 0.01f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.foodEfficiency -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.FOOD], 2) * 0.01f;
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
        baseCost = 12;
    }

    public override float Chance(WS_Tile tile)
    {
        if (tile.lastPopGrowth < 0)
            return Mathf.Sqrt(-tile.lastPopGrowth);
        else
            return 0;
    }
    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.healthcare += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE], 2) * 0.003f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.healthcare -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE], 2) * 0.003f;
    }
}

public class WS_UnrestInfrastructure : WS_Infrastructure
{
    public WS_UnrestInfrastructure()
    {
        name1_3 = "Magistrate";
        name4_6 = "Governor";
        name7_9 = "Administrator";
        nameWonder = "Monumental Parliament";

        type = InfrastructureType.UNREST;
        baseCost = 12;
    }

    public override float Chance(WS_Tile tile)
    {
        return (tile.unrest + tile.unrestCultural + tile.unrestReligious) * 0.05f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.unrestDecay += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.UNREST], 2) * 0.002f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.unrest -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.UNREST], 2) * 0.002f;
    }
}


public class WS_WarInfrastructure : WS_Infrastructure
{
    public WS_WarInfrastructure()
    {
        name1_3 = "Recruitment Camp";
        name4_6 = "Barracks";
        name7_9 = "Quartermaster";
        nameWonder = "High Castle";

        type = InfrastructureType.WAR;
        baseCost = 12;
    }

    public override float Chance(WS_Tile tile)
    {
        return (1 / tile.armyBonus) + (1 / tile.government.armyProfessionalism) * 2.0f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.armyBonus += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION], 2) * 0.01f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.armyBonus -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION], 2) * 0.01f;
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
        baseCost = 15;
    }

    public override float Chance(WS_Tile tile)
    {
        return (1.0f / tile.culture.influenceMul) * 2.0f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.cultureBonus += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CULTURE], 2) * 0.1f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.cultureBonus -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CULTURE], 2) * 0.1f;
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
        baseCost = 15;
    }

    public override float Chance(WS_Tile tile)
    {
        return (1.0f / tile.religion.influenceMul) * 2.0f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.religionBonus += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.RELIGION], 2) * 0.1f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.religionBonus -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.RELIGION], 2) * 0.1f;
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
        baseCost = 20;
    }

    public override float Chance(WS_Tile tile)
    {
        float chance = 0.0f;

        for (int i = 0; i < tile.infrastructureLevels.Length; i++)
            chance += 10 - tile.infrastructureLevels[i];

        chance /= tile.infrastructureLevels.Length;

        return chance * 0.03f;
    }

    public override void Apply(WS_Tile tile, int upgradeLevel)
    {
        tile.constructionBonus += Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION], 2) * 0.02f;
    }

    public override void Reverse(WS_Tile tile, int upgradeLevel)
    {
        tile.constructionBonus -= Mathf.Pow(tile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION], 2) * 0.02f;
    }
}

