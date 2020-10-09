using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  POPULATION - Food Efficiency
public class MasterFarmersTrait : WS_Trait
{
    public override string traitName() { return "Master Farmers"; }
    public override string traitDesc() { return "Food ++"; }
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
    public override string traitName() { return "Agricultural Focused"; }
    public override string traitDesc() { return "Food +"; }
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
    public override string traitName() { return "Neglected Farms"; }
    public override string traitDesc() { return "Food -"; }
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
    public override string traitName() { return "Incompetent Farmers"; }
    public override string traitDesc() { return "Food --"; }
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
    public override string traitName() { return "Far And Beyond"; }
    public override string traitDesc() { return "Colonization ++"; }
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
    public override string traitName() { return "Expansionists"; }
    public override string traitDesc() { return "Colonization +"; }
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
    public override string traitName() { return "Short Horizons"; }
    public override string traitDesc() { return "Colonization -"; }
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

public class NothingLikeHomeTrait : WS_Trait
{
    public override string traitName() { return "Nothing Like Home"; }
    public override string traitDesc() { return "Colonization --"; }
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
    public override string traitName() { return "Healthy"; }
    public override string traitDesc() { return "Healthcare ++"; }
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
    public override string traitName() { return "Durable"; }
    public override string traitDesc() { return "Healthcare +"; }
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
    public override string traitName() { return "High Mortality"; }
    public override string traitDesc() { return "Healthcare -"; }
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
    public override string traitName() { return "Decaying Health"; }
    public override string traitDesc() { return "Healthcare --"; }
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).healthcare -= 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).healthcare += 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  CULTURE - Influence
public class InfluentialCulTrait : WS_Trait
{
    public override string traitName() { return "Influential"; }
    public override string traitDesc() { return "Influence ++"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).influenceBonus += 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).influenceBonus -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class OutwardnessCulTrait : WS_Trait
{
    public override string traitName() { return "Outwardness"; }
    public override string traitDesc() { return "Influence +"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).influenceBonus += 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).influenceBonus -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class InwardnessCulTrait : WS_Trait
{
    public override string traitName() { return "Inwardness"; }
    public override string traitDesc() { return "Influence -"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).influenceBonus -= 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).influenceBonus += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IsolationistsCulTrait : WS_Trait
{
    public override string traitName() { return "Isolationists"; }
    public override string traitDesc() { return "Influence --"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).influenceBonus -= 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).influenceBonus += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  CULTURE - Syncretism
public class SyncreticCulTrait : WS_Trait
{
    public override string traitName() { return "Syncretic"; }
    public override string traitDesc() { return "Syncretism ++"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).syncretism += 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).syncretism -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class TolerantCulTrait : WS_Trait
{
    public override string traitName() { return "Tolerant"; }
    public override string traitDesc() { return "Syncretism +"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).syncretism += 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).syncretism -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IntolerantCulTrait : WS_Trait
{
    public override string traitName() { return "Intolerant"; }
    public override string traitDesc() { return "Syncretism -"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).syncretism -= 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).syncretism += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class RepressiveCulTrait : WS_Trait
{
    public override string traitName() { return "Repressive"; }
    public override string traitDesc() { return "Syncretism --"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Culture)entity).syncretism -= 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Culture)entity).syncretism += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}



