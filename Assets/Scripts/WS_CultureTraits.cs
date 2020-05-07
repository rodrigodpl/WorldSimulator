using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitGroup { EXPANSION, FOOD_EFFICIENCY, HEALTHCARE, SURVIVALISM, MAX_GROUPS }

public class WS_CultureTrait
{
    public string traitName = "WS_Trait";

    virtual public void Apply(WS_Culture culture) { }
    virtual public void Reverse(WS_Culture culture) { }

    virtual public TraitGroup Group() { return TraitGroup.MAX_GROUPS;  }

    virtual public float Chance(WS_Tile tile) { return 0.0f; }
}


//  POPULATION - Survivalism
public class SurvivalistsTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }

    public override void Apply(WS_Culture culture) { culture.survivalism += 20.0f; }
    public override void Reverse(WS_Culture culture) { culture.survivalism -= 20.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability < 85.0f)          return 0.3f;
        else if (tile.habitability < 100.0f)    return 0.1f;
        else                                    return 0.0f;
    }

}

public class ResilientTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }

    public override void Apply(WS_Culture culture) { culture.survivalism += 10.0f; }
    public override void Reverse(WS_Culture culture) { culture.survivalism -= 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability < 85.0f) return 0.2f;
        else if (tile.habitability < 100.0f) return 0.1f;
        else return 0.0f;
    }

}

public class UnadaptableTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }

    public override void Apply(WS_Culture culture) { culture.survivalism -= 10.0f; }
    public override void Reverse(WS_Culture culture) { culture.survivalism += 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability > 100.0f) return 0.1f;
        else if (tile.habitability > 120.0f) return 0.2f;
        else return 0.0f;
    }

}

public class SybaritesTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }

    public override void Apply(WS_Culture culture) { culture.survivalism -= 20.0f; }
    public override void Reverse(WS_Culture culture) { culture.survivalism += 20.0f; }

    public override float Chance(WS_Tile tile)
    {
        if (tile.habitability > 100.0f) return 0.1f;
        else if (tile.habitability > 120.0f) return 0.3f;
        else return 0.0f;
    }
}


////  POPULATION - Urbanization
//public class UrbanitesTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new UrbanitesTrait(); }
//    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
//    public override int Index() { return 0; }

//    public override void Apply() { culture.urbanization += 25.0f; }
//    public override void Reverse() { culture.urbanization -= 25.0f; }
    
//    public override WS_Trait Downgrade() { return new CityDwellersTrait(); }
//}

//public class CityDwellersTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new CityDwellersTrait(); }
//    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
//    public override int Index() { return 1; }

//    public override void Apply() { culture.urbanization += 10.0f; }
//    public override void Reverse() { culture.urbanization -= 10.0f; }

//    public override WS_Trait Upgrade() { return new UrbanitesTrait(); }
//    public override WS_Trait Downgrade() { return new VillagePeopleTrait(); }
//}

//public class VillagePeopleTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new VillagePeopleTrait(); }
//    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
//    public override int Index() { return 2; }

//    public override void Apply() { culture.urbanization -= 10.0f; }
//    public override void Reverse() { culture.urbanization += 10.0f; }

//    public override WS_Trait Upgrade() { return new CityDwellersTrait(); }
//    public override WS_Trait Downgrade() { return new CountrymenTrait(); }
//}

//public class CountrymenTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new CountrymenTrait(); }
//    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
//    public override int Index() { return 3; }

//    public override void Apply() { culture.urbanization -= 25.0f; }
//    public override void Reverse() { culture.urbanization += 25.0f; }

//    public override WS_Trait Upgrade() { return new VillagePeopleTrait(); }
//}


//  POPULATION - Food Efficiency
public class MasterFarmersTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Culture culture) { culture.FoodEfficiency += 0.15f; }
    public override void Reverse(WS_Culture culture) { culture.FoodEfficiency -= 0.15f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}
    

public class AgriculturalFocusedTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Culture culture) { culture.FoodEfficiency += 0.1f; }
    public override void Reverse(WS_Culture culture) { culture.FoodEfficiency -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }

}

public class NeglectedFarmsTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Culture culture) { culture.FoodEfficiency -= 0.1f; }
    public override void Reverse(WS_Culture culture) { culture.FoodEfficiency += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }

}

public class IncompetentFarmersTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }

    public override void Apply(WS_Culture culture) { culture.FoodEfficiency -= 0.15f; }
    public override void Reverse(WS_Culture culture) { culture.FoodEfficiency += 0.15f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  POPULATION - Expansion
public class FarAndBeyondTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Culture culture) { culture.expansionism += 10.0f; }
    public override void Reverse(WS_Culture culture) { culture.expansionism -= 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }

}

public class ExpansionistsTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Culture culture) { culture.expansionism += 5.0f; }
    public override void Reverse(WS_Culture culture) { culture.expansionism -= 5.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class ShortHorizonsTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Culture culture) { culture.expansionism -= 5.0f; }
    public override void Reverse(WS_Culture culture) { culture.expansionism += 5.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class NothingLiketHomeTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }

    public override void Apply(WS_Culture culture) { culture.expansionism -= 10.0f; }
    public override void Reverse(WS_Culture culture) { culture.expansionism += 10.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}


////  POPULATION - Growth
//public class BreedLikeRabbitsTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new BreedLikeRabbitsTrait(); }
//    public override TraitGroup Group() { return TraitGroup.GROWTH; }
//    public override int Index() { return 0; }

//    public override void Apply() { culture.growthBonus += 0.25f; }
//    public override void Reverse() { culture.growthBonus -= 0.25f; }
    
//    public override WS_Trait Downgrade() { return new HighNatalityTrait(); }
//}

//public class HighNatalityTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new HighNatalityTrait(); }
//    public override TraitGroup Group() { return TraitGroup.GROWTH; }
//    public override int Index() { return 1; }

//    public override void Apply() { culture.growthBonus += 0.1f; }
//    public override void Reverse() { culture.growthBonus -= 0.1f; }

//    public override WS_Trait Upgrade() { return new BreedLikeRabbitsTrait(); }
//    public override WS_Trait Downgrade() { return new LowNatalityTrait(); }
//}

//public class LowNatalityTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new LowNatalityTrait(); }
//    public override TraitGroup Group() { return TraitGroup.GROWTH; }
//    public override int Index() { return 2; }

//    public override void Apply() { culture.growthBonus -= 0.1f; }
//    public override void Reverse() { culture.growthBonus += 0.1f; }

//    public override WS_Trait Upgrade() { return new HighNatalityTrait(); }
//    public override WS_Trait Downgrade() { return new DwindlingPopulationTrait(); }
//}

//public class DwindlingPopulationTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new DwindlingPopulationTrait(); }
//    public override TraitGroup Group() { return TraitGroup.GROWTH; }
//    public override int Index() { return 3; }

//    public override void Apply() { culture.growthBonus -= 0.25f; }
//    public override void Reverse() { culture.growthBonus += 0.25f; }

//    public override WS_Trait Upgrade() { return new LowNatalityTrait(); }
//}


//  POPULATION - Mortality
public class HealthyTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Culture culture) { culture.healthcare -= 0.05f; }
    public override void Reverse(WS_Culture culture) { culture.healthcare += 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class DurableTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Culture culture) { culture.healthcare -= 0.03f; }
    public override void Reverse(WS_Culture culture) { culture.healthcare += 0.03f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class HighMortalityTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Culture culture) { culture.healthcare += 0.03f; }
    public override void Reverse(WS_Culture culture) { culture.healthcare -= 0.03f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class DecayingHealthTrait : WS_CultureTrait
{
    public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

    public override void Apply(WS_Culture culture) { culture.healthcare += 0.05f; }
    public override void Reverse(WS_Culture culture) { culture.healthcare -= 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


////  CULTURE - Syncretism
//public class SyncreticTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new SyncreticTrait(); }
//    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }
//    public override int Index() { return 0; }

//    public override WS_Trait Downgrade() { return new WelcomingTrait(); }
//}

//public class WelcomingTrait : WS_Trait
//{
//    public override WS_Trait Copy() { return new WelcomingTrait(); }
//    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }
//    public override int Index() { return 1; }

//    public override WS_Trait Upgrade() { return new SyncreticTrait(); }
//}
