using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CultureTraits;

public class WS_Culture
{
    static float MAX_TRAITS_CULTURE = 3.0f;
    static float MIN_TRAITS_CULTURE = 3.0f;
    static float MAX_TRAITS_TRIBAL = 3.0f;
    static float MIN_TRAITS_TRIBAL = 1.0f;

    public float FoodEfficiency = 1.15f;  
    public float survivalism = 0.0f;  
    public float expansionism = 0.0f;
    public float healthcare = 0.05f;

    public float influenceBonus = -3.0f;  
    public float syncretism = 0.0f;
    public float decadence = 0.0f;

    public bool tribal = true;

    public Color cultureColor = Color.white;

    public WS_Tile capital = null;

    public List<WS_CultureTrait> traits = new List<WS_CultureTrait>();

    public bool merged = false;


    public WS_Culture(WS_Tile tile)  // tribal
    {
        float selector = Random.Range(MIN_TRAITS_TRIBAL - traits.Count, MAX_TRAITS_TRIBAL - traits.Count);

        while (selector > 0.0f)
        {
            addRandomTrait(tile);
            selector = Random.Range(MIN_TRAITS_TRIBAL - traits.Count, MAX_TRAITS_TRIBAL - traits.Count);
        }

        capital = tile;
        cultureColor = new Color(Random.Range(0.1f, 0.35f), Random.Range(0.1f, 0.35f), Random.Range(0.1f, 0.35f)); // dark tones
    }


    public WS_Culture(WS_Culture base_culture, WS_Tile tile)  // for culture births / cultural collapses
    {
        tribal = false;
        influenceBonus = 0.0f;

        float selector = Random.Range(MIN_TRAITS_CULTURE - traits.Count, MAX_TRAITS_CULTURE - traits.Count);

        foreach (WS_CultureTrait trait in base_culture.traits)
        {
            trait.Apply(this);
            traits.Add(trait);
        }

        while (selector > 0.0f)
        {
            addRandomTrait(base_culture.capital);
            selector = Random.Range(MIN_TRAITS_CULTURE - traits.Count, MAX_TRAITS_CULTURE - traits.Count);
        }

        selector = Random.Range(traits.Count - MAX_TRAITS_CULTURE, traits.Count - MIN_TRAITS_CULTURE);

        while (selector < 0.0f)
        {
            removeRandomTrait(base_culture.capital);
            selector = Random.Range(traits.Count - MAX_TRAITS_CULTURE, traits.Count - MIN_TRAITS_CULTURE);
        }

        capital = tile;
        cultureColor = new Color(Random.Range(0.55f, 0.9f), Random.Range(0.55f, 0.9f), Random.Range(0.55f, 0.9f));
    }


    public WS_Culture(WS_Culture base_culture_A, WS_Culture base_culture_B, WS_Tile tile)  // for cultural merges
    {
        tribal = false;
        merged = true;

        int traitNum = (base_culture_A.traits.Count + base_culture_B.traits.Count) / 2;

        for(int i = 0; i < traitNum; i++)
        {
            bool valid_A = false;
            bool valid_B = false;


            if (traitNum < base_culture_A.traits.Count)
                if (!traits.Contains(base_culture_A.traits[traitNum]))
                {
                    valid_A = true;

                    foreach(WS_CultureTrait ownedTrait in traits)
                    {
                        if(ownedTrait.Group() == base_culture_A.traits[traitNum].Group())
                        {
                            valid_A = false;
                            break;
                        }
                    }
                }

            if (traitNum < base_culture_B.traits.Count)
                if (!traits.Contains(base_culture_B.traits[traitNum]))
                {
                    valid_B = true;

                    foreach (WS_CultureTrait ownedTrait in traits)
                    {
                        if (ownedTrait.Group() == base_culture_B.traits[traitNum].Group())
                        {
                            valid_B = false;
                            break;
                        }
                    }
                }


            if (valid_A && !valid_B)
                traits.Add(base_culture_A.traits[traitNum]);

            else if(!valid_A && valid_B)
                traits.Add(base_culture_B.traits[traitNum]);

            else if(valid_A && valid_B)
            {
                float selector = Random.Range(0.0f, 1.0f);
                
                if(selector < 0.5f)
                    traits.Add(base_culture_A.traits[traitNum]);
                else
                    traits.Add(base_culture_B.traits[traitNum]);
            }

            if (valid_A || valid_B)
                traits[traits.Count - 1].Apply(this);
        }

        for (int i = traits.Count; i < MIN_TRAITS_CULTURE; i++)
            addRandomTrait(tile);

        capital = tile;
        cultureColor = (base_culture_A.cultureColor + base_culture_B.cultureColor) / 2.0f;
    }


    public bool addRandomTrait(WS_Tile tile)
    {
        if (traits.Count == (int)TraitGroup.MAX_GROUPS || traits.Count > (tribal ? MAX_TRAITS_TRIBAL : MAX_TRAITS_CULTURE))
            return false;


        List<WS_CultureTrait> possibleTraits = new List<WS_CultureTrait>();
        float totalChance = 0.0f;

        foreach (WS_CultureTrait trait in WS_World.cultureTraits)
        {
            if (traits.Contains(trait))
                continue;

            float chance = trait.Chance(tile);

            if (chance > 0.0f)
            {
                totalChance += chance;
                possibleTraits.Add(trait);
            }
        }


        float selector = Random.Range(0.0f, totalChance);

        foreach (WS_CultureTrait trait in possibleTraits)
        {
            float chance = trait.Chance(tile);

            if (selector <= chance)
            {
                WS_CultureTrait removeTrait = null;

                foreach(WS_CultureTrait ownedTrait in traits)
                {
                    if(ownedTrait.Group() == trait.Group())
                    {
                        removeTrait = ownedTrait;
                        break;
                    }
                }

                if (removeTrait != null)
                    traits.Remove(removeTrait);

                trait.Apply(this);
                traits.Add(trait);
                return true;
            }
            else
                selector -= chance;
        }

        return false;
    }


    public bool removeRandomTrait(WS_Tile tile)
    {
        if (traits.Count < (tribal ? MIN_TRAITS_TRIBAL : MIN_TRAITS_CULTURE))
            return false;

        float totalChance = 0.0f;

        foreach (WS_CultureTrait trait in traits)
        {
            float chance = trait.Chance(tile);

            if (chance > 0.0f)
                totalChance += 1.0f - chance;
            
        }

        float selector = Random.Range(0.0f, totalChance);

        foreach (WS_CultureTrait trait in traits)
        {
            float chance = trait.Chance(tile);

            if (selector <= 1.0f - chance)
            {
                WS_CultureTrait removeTrait = trait;
                trait.Reverse(this);
                traits.Remove(removeTrait);
                return true;
            }
            else
                selector -= chance;
        }

        return false;
    }


    public bool changeRandomTrait(WS_Tile tile)
    {
        bool valid = false;

        if (removeRandomTrait(tile))
            if (addRandomTrait(tile))
                valid = true;

        else if (addRandomTrait(tile))
                if (removeRandomTrait(tile))
                    valid = true;

        return valid;
    }
}