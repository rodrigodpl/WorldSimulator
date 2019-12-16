using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Culture
{
    public float urbanization = 100; // 10 / 300.0
    public float FoodEfficiency = 1.35f;  // 1.3 / 10.0
    public float survivalism = 0.0f;  // -50.0 / 50.0
    public float settlerCap = 3.0f;  
    public float nationSettlerCap = 1.0f;
    public float expansionBonus = 1.0f;
    public float growthBonus = 1.0f;  // 0.5 / 1.5
    public float mortalityRate = 1.0f;  // 0.5 / 1.5

    public float influence = 1.0f;  // 0.5 / 2.0
    public float birthBonus = 0.0f;

    public Color cultureColor = Color.white;

    public List<WS_Trait> traits = new List<WS_Trait>();

    public WS_Culture(int newTraits, WS_Culture base_culture = null)
    {
        if (base_culture != null)
        {
            foreach (WS_Trait trait in base_culture.traits)
                addTrait(trait);
        }

        for (int i = 0; i < newTraits; i++)
        {
            float rand = Random.Range(0.0f, 3.0f);

            if (rand < 1.0f)
            {
                if (!addRandomTrait())
                    removeRandomTrait();
            }
            else if (rand < 2.0f)
            {
                if (!changeRandomTrait())
                    addRandomTrait();
            }
            else
            {
                if (!removeRandomTrait())
                    addRandomTrait();
            }
        }

        if (base_culture != null)
        {
            Color prevColor = base_culture.cultureColor;
            cultureColor = new Color(prevColor.r + Random.Range(-0.15f, 0.15f), prevColor.g + Random.Range(-0.15f, 0.15f), prevColor.b + Random.Range(-0.15f, 0.15f));
        }
        else
            cultureColor = new Color(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f));

        birthBonus += 20.0f;
    }

    public void addTrait(WS_Trait trait)
    {
        WS_Trait newTrait = trait.Copy();
        newTrait.setCulture(this);
        newTrait.Apply();
        traits.Add(newTrait);
    }

    public bool addRandomTrait()
    {
        if (traits.Count == (int)TraitGroup.MAX_GROUPS)
            return false;

        while (true)
        {
            int traitId = Mathf.FloorToInt(Random.Range(0.0f, WS_World.allTraits.Count - 0.01f));

            bool valid = true;

            foreach (WS_Trait trait in traits)
                if (trait.Group() == WS_World.allTraits[traitId].Group())
                {
                    valid = false;
                    break;
                }

            if (valid)
            {
                WS_Trait newTrait = WS_World.allTraits[traitId].Copy();
                newTrait.setCulture(this);
                newTrait.Apply();
                traits.Add(newTrait);

                return true;
            }
        }
    }

    public bool removeRandomTrait()
    {
        if (traits.Count <= WS_WorldGenerator.minTraits)
            return false;

        int traitId = Mathf.FloorToInt(Random.Range(0.0f, traits.Count - 0.01f));

        traits[traitId].Reverse();
        traits.RemoveAt(traitId);
        return true;
    }

    public bool changeRandomTrait()
    {
        if (traits.Count == 0)
            return false;

        int traitId = Mathf.FloorToInt(Random.Range(0.0f, traits.Count - 0.01f));
        TraitGroup removedGroup = traits[traitId].Group();

        traits[traitId].Reverse();
        traits.RemoveAt(traitId);

        while (true)
        {
            int newTraitId = Mathf.FloorToInt(Random.Range(0.0f, WS_World.allTraits.Count - 0.01f));

            foreach (WS_Trait trait in traits)
                if (trait.Group() == WS_World.allTraits[newTraitId].Group() || trait.Group() == removedGroup)
                    continue;

            WS_Trait newTrait = WS_World.allTraits[newTraitId].Copy();
            newTrait.setCulture(this);
            newTrait.Apply();
            traits.Add(newTrait);

            return true;
        }

    }
}