using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  POPULATION - Food Efficiency
public class MasterFarmersTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency += 0.15f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency -= 0.15f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class AgriculturalFocusedTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class NeglectedFarmsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IncompetentFarmersTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency -= 0.15f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).FoodEfficiency += 0.15f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  POPULATION - Expansion
public class FarAndBeyondTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).expansionism += 20.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).expansionism -= 20.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability < 85.0f) return 0.3f;
        else if (tile.habitability < 100.0f) return 0.1f;
        else return 0.0f;
    }
}

public class ExpansionistsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).expansionism += 10.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).expansionism -= 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability < 85.0f) return 0.2f;
        else if (tile.habitability < 100.0f) return 0.1f;
        else return 0.0f;
    }
}

public class ShortHorizonsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).expansionism -= 10.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).expansionism += 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability > 100.0f) return 0.1f;
        else if (tile.habitability > 120.0f) return 0.2f;
        else return 0.0f;
    }
}

public class NothingLiketHomeTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).expansionism -= 20.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).expansionism += 20.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability > 100.0f) return 0.1f;
        else if (tile.habitability > 120.0f) return 0.3f;
        else return 0.0f;
    }
}


//  POPULATION - Mortality
public class HealthyTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).healthcare += 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).healthcare -= 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class DurableTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).healthcare += 0.03f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).healthcare -= 0.03f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class HighMortalityTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).healthcare -= 0.03f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).healthcare += 0.03f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class DecayingHealthTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).healthcare -= 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).healthcare += 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


