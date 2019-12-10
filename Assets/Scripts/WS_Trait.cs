using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitGroup {    EXPANSION, GROWTH, FOOD_EFFICIENCY, URBANIZATION, MORTALITY, SURVIVALISM,
                            SYNCRETISM, MAX_GROUPS }

public class WS_Trait
{
    public string traitName = "WS_Trait";

    public WS_Culture culture = null;
    public WS_Nation nation = null;
    //public Religion religion;

    virtual public void Apply() { }
    virtual public void Reverse() { }
    
    virtual public WS_Trait Upgrade() { return null; }
    virtual public WS_Trait Downgrade() { return null; }

    virtual public TraitGroup Group() { return TraitGroup.MAX_GROUPS;  }
    virtual public int Index() { return 0; }

    virtual public WS_Trait Copy() { return null; }

    public void setCulture(WS_Culture _culture) { culture = _culture; }
    public void setNation(WS_Nation _nation) { nation = _nation; }
    //void setReligion(Religion _religion) { religion = _religion; }
}


//  POPULATION - Survivalism
public class SurvivalistsTrait : WS_Trait
{
    public override WS_Trait Copy() { return new SurvivalistsTrait(); }
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }
    public override int Index() { return 0; }

    public override void Apply() { culture.survivalism += 50.0f; }
    public override void Reverse() { culture.survivalism -= 50.0f; }

    public override WS_Trait Downgrade() { return new ResilientTrait(); }
}

public class ResilientTrait : WS_Trait
{
    public override WS_Trait Copy() { return new ResilientTrait(); }
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }
    public override int Index() { return 1; }

    public override void Apply() { culture.survivalism += 20.0f; }
    public override void Reverse() { culture.survivalism -= 20.0f; }
    
    public override WS_Trait Upgrade() { return new SurvivalistsTrait(); }
    public override WS_Trait Downgrade() { return new UnadaptableTrait(); }
}

public class UnadaptableTrait : WS_Trait
{
    public override WS_Trait Copy() { return new UnadaptableTrait(); }
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }
    public override int Index() { return 2; }

    public override void Apply() { culture.survivalism -= 20.0f; }
    public override void Reverse() { culture.survivalism += 20.0f; }

    public override WS_Trait Upgrade() { return new ResilientTrait(); }
    public override WS_Trait Downgrade() { return new SybaritesTrait(); }
}

public class SybaritesTrait : WS_Trait
{
    public override WS_Trait Copy() { return new SybaritesTrait(); }
    public override TraitGroup Group() { return TraitGroup.SURVIVALISM; }
    public override int Index() { return 3; }

    public override void Apply() { culture.survivalism -= 50.0f; }
    public override void Reverse() { culture.survivalism += 50.0f; }
    
    public override WS_Trait Upgrade() { return new SybaritesTrait(); }
}

//  POPULATION - Urbanization
public class UrbanitesTrait : WS_Trait
{
    public override WS_Trait Copy() { return new UrbanitesTrait(); }
    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
    public override int Index() { return 0; }

    public override void Apply() { culture.urbanization += 25.0f; }
    public override void Reverse() { culture.urbanization -= 25.0f; }
    
    public override WS_Trait Downgrade() { return new CityDwellersTrait(); }
}

public class CityDwellersTrait : WS_Trait
{
    public override WS_Trait Copy() { return new CityDwellersTrait(); }
    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
    public override int Index() { return 1; }

    public override void Apply() { culture.urbanization += 10.0f; }
    public override void Reverse() { culture.urbanization -= 10.0f; }

    public override WS_Trait Upgrade() { return new UrbanitesTrait(); }
    public override WS_Trait Downgrade() { return new VillagePeopleTrait(); }
}

public class VillagePeopleTrait : WS_Trait
{
    public override WS_Trait Copy() { return new VillagePeopleTrait(); }
    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
    public override int Index() { return 2; }

    public override void Apply() { culture.urbanization -= 10.0f; }
    public override void Reverse() { culture.urbanization += 10.0f; }

    public override WS_Trait Upgrade() { return new CityDwellersTrait(); }
    public override WS_Trait Downgrade() { return new CountrymenTrait(); }
}

public class CountrymenTrait : WS_Trait
{
    public override WS_Trait Copy() { return new CountrymenTrait(); }
    public override TraitGroup Group() { return TraitGroup.URBANIZATION; }
    public override int Index() { return 3; }

    public override void Apply() { culture.urbanization -= 25.0f; }
    public override void Reverse() { culture.urbanization += 25.0f; }

    public override WS_Trait Upgrade() { return new VillagePeopleTrait(); }
}

//  POPULATION - Food Efficiency
public class MasterFarmersTrait : WS_Trait
{
    public override WS_Trait Copy() { return new MasterFarmersTrait(); }
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }
    public override int Index() { return 0; }

    public override void Apply() { culture.FoodEfficiency += 0.3f; }
    public override void Reverse() { culture.FoodEfficiency -= 0.3f; }
    
    public override WS_Trait Downgrade() { return new AgriculturalFocusedTrait(); }
}

public class AgriculturalFocusedTrait : WS_Trait
{
    public override WS_Trait Copy() { return new AgriculturalFocusedTrait(); }
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }
    public override int Index() { return 1; }

    public override void Apply() { culture.FoodEfficiency += 0.1f; }
    public override void Reverse() { culture.FoodEfficiency -= 0.1f; }

    public override WS_Trait Upgrade() { return new MasterFarmersTrait(); }
    public override WS_Trait Downgrade() { return new NeglectedFarmsTrait(); }
}

public class NeglectedFarmsTrait : WS_Trait
{
    public override WS_Trait Copy() { return new NeglectedFarmsTrait(); }
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }
    public override int Index() { return 2; }

    public override void Apply() { culture.FoodEfficiency -= 0.1f; }
    public override void Reverse() { culture.FoodEfficiency += 0.1f; }

    public override WS_Trait Upgrade() { return new AgriculturalFocusedTrait(); }
    public override WS_Trait Downgrade() { return new IncompetentFarmersTrait(); }
}

public class IncompetentFarmersTrait : WS_Trait
{
    public override WS_Trait Copy() { return new IncompetentFarmersTrait(); }
    public override TraitGroup Group() { return TraitGroup.FOOD_EFFICIENCY; }
    public override int Index() { return 3; }

    public override void Apply() { culture.FoodEfficiency -= 0.3f; }
    public override void Reverse() { culture.FoodEfficiency += 0.3f; }

    public override WS_Trait Upgrade() { return new NeglectedFarmsTrait(); }
}

//  POPULATION - Expansion
public class FarAndBeyondTrait : WS_Trait
{
    public override WS_Trait Copy() { return new FarAndBeyondTrait(); }
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }
    public override int Index() { return 0; }

    public override void Apply() { culture.expansionBonus += 0.5f; }
    public override void Reverse() { culture.expansionBonus -= 0.5f; }
    
    public override WS_Trait Downgrade() { return new ExpansionistsTrait(); }
}

public class ExpansionistsTrait : WS_Trait
{
    public override WS_Trait Copy() { return new ExpansionistsTrait(); }
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }
    public override int Index() { return 1; }

    public override void Apply() { culture.expansionBonus += 0.2f; }
    public override void Reverse() { culture.expansionBonus -= 0.2f; }

    public override WS_Trait Upgrade() { return new FarAndBeyondTrait(); }
    public override WS_Trait Downgrade() { return new ShortHorizonsTrait(); }
}

public class ShortHorizonsTrait : WS_Trait
{
    public override WS_Trait Copy() { return new ShortHorizonsTrait(); }
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }
    public override int Index() { return 2; }

    public override void Apply() { culture.expansionBonus -= 0.2f; }
    public override void Reverse() { culture.expansionBonus += 0.2f; }

    public override WS_Trait Upgrade() { return new ExpansionistsTrait(); }
    public override WS_Trait Downgrade() { return new NothingLiketHomeTrait(); }
}

public class NothingLiketHomeTrait : WS_Trait
{
    public override WS_Trait Copy() { return new NothingLiketHomeTrait(); }
    public override TraitGroup Group() { return TraitGroup.EXPANSION; }
    public override int Index() { return 3; }

    public override void Apply() { culture.expansionBonus -= 0.5f; }
    public override void Reverse() { culture.expansionBonus += 0.5f; }

    public override WS_Trait Upgrade() { return new ShortHorizonsTrait(); }
}


//  POPULATION - Growth
public class BreedLikeRabbitsTrait : WS_Trait
{
    public override WS_Trait Copy() { return new BreedLikeRabbitsTrait(); }
    public override TraitGroup Group() { return TraitGroup.GROWTH; }
    public override int Index() { return 0; }

    public override void Apply() { culture.growthBonus += 0.25f; }
    public override void Reverse() { culture.growthBonus -= 0.25f; }
    
    public override WS_Trait Downgrade() { return new HighNatalityTrait(); }
}

public class HighNatalityTrait : WS_Trait
{
    public override WS_Trait Copy() { return new HighNatalityTrait(); }
    public override TraitGroup Group() { return TraitGroup.GROWTH; }
    public override int Index() { return 1; }

    public override void Apply() { culture.growthBonus += 0.1f; }
    public override void Reverse() { culture.growthBonus -= 0.1f; }

    public override WS_Trait Upgrade() { return new BreedLikeRabbitsTrait(); }
    public override WS_Trait Downgrade() { return new LowNatalityTrait(); }
}

public class LowNatalityTrait : WS_Trait
{
    public override WS_Trait Copy() { return new LowNatalityTrait(); }
    public override TraitGroup Group() { return TraitGroup.GROWTH; }
    public override int Index() { return 2; }

    public override void Apply() { culture.growthBonus -= 0.1f; }
    public override void Reverse() { culture.growthBonus += 0.1f; }

    public override WS_Trait Upgrade() { return new HighNatalityTrait(); }
    public override WS_Trait Downgrade() { return new DwindlingPopulationTrait(); }
}

public class DwindlingPopulationTrait : WS_Trait
{
    public override WS_Trait Copy() { return new DwindlingPopulationTrait(); }
    public override TraitGroup Group() { return TraitGroup.GROWTH; }
    public override int Index() { return 3; }

    public override void Apply() { culture.growthBonus -= 0.25f; }
    public override void Reverse() { culture.growthBonus += 0.25f; }

    public override WS_Trait Upgrade() { return new LowNatalityTrait(); }
}


//  POPULATION - Mortality
public class HealthyTrait : WS_Trait
{
    public override WS_Trait Copy() { return new DwindlingPopulationTrait(); }
    public override TraitGroup Group() { return TraitGroup.MORTALITY; }
    public override int Index() { return 0; }

    public override void Apply() { culture.mortalityRate -= 0.25f; }
    public override void Reverse() { culture.mortalityRate += 0.25f; }
    
    public override WS_Trait Downgrade() { return new DurableTrait(); }
}

public class DurableTrait : WS_Trait
{
    public override WS_Trait Copy() { return new DurableTrait(); }
    public override TraitGroup Group() { return TraitGroup.MORTALITY; }
    public override int Index() { return 1; }

    public override void Apply() { culture.mortalityRate -= 0.1f; }
    public override void Reverse() { culture.mortalityRate += 0.1f; }

    public override WS_Trait Upgrade() { return new HealthyTrait(); }
    public override WS_Trait Downgrade() { return new HighMortalityTrait(); }
}

public class HighMortalityTrait : WS_Trait
{
    public override WS_Trait Copy() { return new HighNatalityTrait(); }
    public override TraitGroup Group() { return TraitGroup.MORTALITY; }
    public override int Index() { return 2; }

    public override void Apply() { culture.mortalityRate += 0.1f; }
    public override void Reverse() { culture.mortalityRate -= 0.1f; }

    public override WS_Trait Upgrade() { return new DurableTrait(); }
    public override WS_Trait Downgrade() { return new DecayingHealthTrait(); }
}

public class DecayingHealthTrait : WS_Trait
{
    public override WS_Trait Copy() { return new BreedLikeRabbitsTrait(); }
    public override TraitGroup Group() { return TraitGroup.MORTALITY; }
    public override int Index() { return 3; }

    public override void Apply() { culture.mortalityRate += 0.25f; }
    public override void Reverse() { culture.mortalityRate -= 0.25f; }

    public override WS_Trait Upgrade() { return new HighMortalityTrait(); }
}


//  CULTURE - Syncretism
public class SyncreticTrait : WS_Trait
{
    public override WS_Trait Copy() { return new SyncreticTrait(); }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }
    public override int Index() { return 0; }

    public override WS_Trait Downgrade() { return new WelcomingTrait(); }
}

public class WelcomingTrait : WS_Trait
{
    public override WS_Trait Copy() { return new WelcomingTrait(); }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }
    public override int Index() { return 1; }

    public override WS_Trait Upgrade() { return new SyncreticTrait(); }
}
