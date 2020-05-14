using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultureTraits
{
    public enum TraitGroup { EXPANSION, FOOD_EFFICIENCY, HEALTHCARE, SURVIVALISM, INFLUENCE, SYNCRETISM, MAX_GROUPS }

    public class WS_CultureTrait
    {
        public string traitName = "WS_Trait";

        virtual public void Apply(WS_Culture culture) { }
        virtual public void Reverse(WS_Culture culture) { }

        virtual public TraitGroup Group() { return TraitGroup.MAX_GROUPS; }

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
            if (tile.habitability < 85.0f) return 0.3f;
            else if (tile.habitability < 100.0f) return 0.1f;
            else return 0.0f;
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
            return 0.1f;
        }

    }

    public class ExpansionistsTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.EXPANSION; }

        public override void Apply(WS_Culture culture) { culture.expansionism += 5.0f; }
        public override void Reverse(WS_Culture culture) { culture.expansionism -= 5.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.2f;
        }
    }

    public class ShortHorizonsTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.EXPANSION; }

        public override void Apply(WS_Culture culture) { culture.expansionism -= 5.0f; }
        public override void Reverse(WS_Culture culture) { culture.expansionism += 5.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.2f;
        }
    }

    public class NothingLiketHomeTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.EXPANSION; }

        public override void Apply(WS_Culture culture) { culture.expansionism -= 10.0f; }
        public override void Reverse(WS_Culture culture) { culture.expansionism += 10.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }


    //  POPULATION - Mortality
    public class HealthyTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

        public override void Apply(WS_Culture culture) { culture.healthcare += 0.05f; }
        public override void Reverse(WS_Culture culture) { culture.healthcare -= 0.05f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class DurableTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

        public override void Apply(WS_Culture culture) { culture.healthcare += 0.03f; }
        public override void Reverse(WS_Culture culture) { culture.healthcare -= 0.03f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.2f;
        }
    }

    public class HighMortalityTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

        public override void Apply(WS_Culture culture) { culture.healthcare -= 0.03f; }
        public override void Reverse(WS_Culture culture) { culture.healthcare += 0.03f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.2f;
        }
    }

    public class DecayingHealthTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.HEALTHCARE; }

        public override void Apply(WS_Culture culture) { culture.healthcare -= 0.05f; }
        public override void Reverse(WS_Culture culture) { culture.healthcare += 0.05f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }



    //  CULTURE - Influence
    public class InfluentialTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

        public override void Apply(WS_Culture culture) { culture.influenceBonus += 2.0f; }
        public override void Reverse(WS_Culture culture) { culture.influenceBonus -= 2.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class OutwardnessTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

        public override void Apply(WS_Culture culture) { culture.influenceBonus += 1.0f; }
        public override void Reverse(WS_Culture culture) { culture.influenceBonus -= 1.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class InwardnessTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

        public override void Apply(WS_Culture culture) { culture.influenceBonus -= 1.0f; }
        public override void Reverse(WS_Culture culture) { culture.influenceBonus += 1.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class IsolationistsTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

        public override void Apply(WS_Culture culture) { culture.influenceBonus -= 2.0f; }
        public override void Reverse(WS_Culture culture) { culture.influenceBonus += 2.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }


    //  CULTURE - Influence
    public class SyncreticTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

        public override void Apply(WS_Culture culture) { culture.syncretism += 2.0f; }
        public override void Reverse(WS_Culture culture) { culture.syncretism -= 2.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class TolerantTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

        public override void Apply(WS_Culture culture) { culture.syncretism += 1.0f; }
        public override void Reverse(WS_Culture culture) { culture.syncretism -= 1.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class IntolerantTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

        public override void Apply(WS_Culture culture) { culture.syncretism -= 1.0f; }
        public override void Reverse(WS_Culture culture) { culture.syncretism += 1.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

    public class RepressiveTrait : WS_CultureTrait
    {
        public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

        public override void Apply(WS_Culture culture) { culture.syncretism -= 2.0f; }
        public override void Reverse(WS_Culture culture) { culture.syncretism += 2.0f; }

        public override float Chance(WS_Tile tile)
        {
            return 0.1f;
        }
    }

}