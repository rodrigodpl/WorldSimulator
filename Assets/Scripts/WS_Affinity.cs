using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CulturalStance { TOLERANCE, ASSIMILATION, REPRESSION, SYNCRETISM }


public class WS_Affinity
{
    public float totalAffinity = 0.0f;
    public float culturalAffinity = 0.0f;
    public float ideologicalAffinity = 0.0f;
    public float religiousAffinity = 0.0f;

    public WS_Nation nation = null;
    public WS_Nation other = null;

    public CulturalStance culturalStance = CulturalStance.TOLERANCE;


    public void CalculateStance()
    {
        foreach (WS_Trait trait in nation.rulingCulture.traits)
        {
            if (trait.Group() == TraitGroup.SYNCRETISM)
            {
                if (trait.Index() == 0)
                    culturalStance = CulturalStance.SYNCRETISM;
                else if (Random.Range(0.0f, 1.0f) > 0.5f)
                    culturalStance = CulturalStance.SYNCRETISM;

                return;
            }
        }

        if (culturalAffinity > 0.0f && nation.culturalStrength > other.culturalStrength)
            culturalStance = CulturalStance.ASSIMILATION;
        else if (culturalAffinity < 0.0f && nation.culturalStrength < other.culturalStrength)
            culturalStance = CulturalStance.REPRESSION;

        culturalStance = CulturalStance.TOLERANCE;
    }

    public void RecalculateCulture()
    {
        culturalAffinity = 0.0f;

        foreach (WS_Trait trait1 in nation.rulingCulture.traits)
            foreach (WS_Trait trait2 in other.rulingCulture.traits)
            {
                if (trait1.Group() == trait2.Group())
                {
                    float traitAffinity = Mathf.Abs(trait1.Index() - trait2.Index()) * 10.0f;

                    if (traitAffinity <= 0.0f)
                        traitAffinity -= 10.0f;

                    culturalAffinity += traitAffinity;
                }
            }

        totalAffinity = culturalAffinity + religiousAffinity + ideologicalAffinity;
    }

    public void CalculateAffinity(WS_Nation _nation, WS_Nation _other)
    {
        nation = _nation;
        other = _other;

        if (nation.rulingCulture != null && other.rulingCulture != null)
        {
            totalAffinity = culturalAffinity = ideologicalAffinity = religiousAffinity = 0.0f;

            foreach (WS_Trait trait1 in nation.rulingCulture.traits)
                foreach (WS_Trait trait2 in other.rulingCulture.traits)
                {
                    if (trait1.Group() == trait2.Group())
                    {
                        float traitAffinity = (Mathf.Abs(trait1.Index() - trait2.Index()) * -10.0f) + 10.0f;

                        culturalAffinity += traitAffinity;
                    }
                }
        }
        // ideological

        // religious

        totalAffinity = culturalAffinity + religiousAffinity + ideologicalAffinity;
    }
}