using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CultureStrengthEvent : WS_BaseEvent
{
    public CultureStrengthEvent() { eventName = "Culture Strength"; module = EventModule.CULTURE; }

    protected override void Success()
    {
        float influence = tile.cultureStrength;

        for (int c = 1; c <= Mathf.Min(tile.cultureStrength, 3); c++)
        {
            foreach (WS_Tile neighbor in tile.Neighbors(c))
            {
                if (neighbor.Population() > 0.0f)
                {
                    float populationMultiplier = ((tile.Population() / neighbor.Population()) + 1.0f) / 2.0f;
                    float stanceMultiplier = 1.0f;

                    switch (tile.nation.getAffinity(neighbor.nation).culturalStance)
                    {
                        case CulturalStance.ASSIMILATION: stanceMultiplier += 0.30f; break;
                        case CulturalStance.REPRESSION: stanceMultiplier -= 0.15f; break;
                    }

                    switch (neighbor.nation.getAffinity(tile.nation).culturalStance)
                    {
                        case CulturalStance.ASSIMILATION: stanceMultiplier += 0.15f; break;
                        case CulturalStance.REPRESSION: stanceMultiplier -= 0.30f; break;
                    }

                    neighbor.addInfluence(tile.mainCulture, influence * populationMultiplier * stanceMultiplier * WS_World.frameMult);
                }
            }

            influence--;
        }

    }

}


public class RulingCultureChangeEvent : WS_BaseEvent
{
    public WS_Culture targetCulture = null;

    public RulingCultureChangeEvent() { eventName = "Ruling Culture Change"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        return (tile.nation.capital == tile);
    }

    protected override bool SuccessCheck()
    {
        for (int i = 0; i < tile.nation.foreignPopulations.Count; i++)
        {
            if (tile.nation.foreignPopulations[i] > tile.nation.population * 0.5f)
                targetCulture = tile.nation.foreignCultures[i];
        }

        return targetCulture != null;
    }

    protected override void Success()
    {
        tile.nation.rulingCulture = targetCulture;
    }
}

public class SyncreticAssimilationEvent : WS_BaseEvent
{
    WS_Nation targetNation = null;

    public SyncreticAssimilationEvent() { eventName = "Syncretic Assimilation"; module = EventModule.CULTURE; }

    protected override bool FireCheck()
    {
        foreach (WS_Tile neighbor in tile.Neighbors())
        {
            if (neighbor.Population() > 0.0f && neighbor.nation != tile.nation)
            {
                if (tile.nation.getAffinity(neighbor.nation).culturalStance == CulturalStance.SYNCRETISM)
                    targetNation = neighbor.nation;
            }  
        }

        return targetNation != null;
    }

    protected override bool SuccessCheck()
    {
        return Random.Range(0.0f, 1.0f) < 0.1f * ((tile.nation.getAffinity(targetNation).totalAffinity + 80.0f) / 100.0f) * Mathf.Sqrt(WS_World.frameMult);
    }

    protected override void Success()
    {
        List<WS_Trait> availableTraits = new List<WS_Trait>();

        foreach (WS_Trait tgtTrait in targetNation.rulingCulture.traits)
        {
            bool valid = true;

            foreach (WS_Trait syncTrait in tile.mainCulture.traits)
                if (syncTrait.Group() == tgtTrait.Group())
                {
                    valid = false;
                    break;
                }

            if (valid)
                availableTraits.Add(tgtTrait);
        }

        if (availableTraits.Count > 0)
        {
            int index = Mathf.FloorToInt(Random.Range(0.0f, availableTraits.Count - 0.01f));

            WS_Trait newTrait = availableTraits[index].Copy();

            newTrait.setCulture(tile.mainCulture);
            newTrait.Apply();
            tile.mainCulture.traits.Add(newTrait);
            return;
        }


        List<WS_Trait> removableTraits = new List<WS_Trait>();
        foreach (WS_Trait syncTrait in tile.mainCulture.traits)
        {
            bool valid = true;

            foreach (WS_Trait tgtTrait in tile.mainCulture.traits)
                if (syncTrait.Group() == tgtTrait.Group() || syncTrait.Group() == TraitGroup.SYNCRETISM)
                {
                    valid = false;
                    break;
                }

            if (valid)
                removableTraits.Add(syncTrait);
        }

        if (removableTraits.Count > 0)
        {
            int index = Mathf.FloorToInt(Random.Range(0.0f, removableTraits.Count - 0.01f));

            WS_Trait removedTrait = removableTraits[index];

            removedTrait.Reverse();
            tile.mainCulture.traits.Remove(removedTrait);
            return;
        }

        WS_Trait syncretic = null;

        foreach (WS_Trait trait in tile.nation.rulingCulture.traits)
            if (trait.Group() == TraitGroup.SYNCRETISM)
            {
                syncretic = trait;
                break;
            }

        if (syncretic == null)
            targetNation.rulingCulture.traits.Add(new SyncreticTrait());
        else
            targetNation.rulingCulture.traits.Remove(syncretic);

        tile.nation.rulingCulture = targetNation.rulingCulture;

        tile.nation.SetCulture(targetNation.rulingCulture);

    }
}
