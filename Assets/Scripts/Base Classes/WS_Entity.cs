using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType { NONE, CULTURE, RELIGION, GOVERNMENT }

public class WS_Entity
{
    public EntityType type = EntityType.NONE;
    public Sprite sprite = null;

    float minTraits = 0.0f;
    float maxTraits = 0.0f;

    public Color color = Color.white;

    public WS_Tile capital = null;
    public string name = "";

    public List<WS_Trait> traits = new List<WS_Trait>();

    public WS_Entity() { }

    public void Init(WS_Tile tile, EntityType _type)
    {
        type = _type;

        switch (type)
        {
            case EntityType.CULTURE:
                minTraits = WS_Culture.MIN_TRAITS_CULTURE;
                maxTraits = WS_Culture.MAX_TRAITS_CULTURE;
                name = WS_WordCreator.Create();
                break;
            case EntityType.RELIGION:
                minTraits = WS_Religion.MIN_TRAITS_RELIGION;
                maxTraits = WS_Religion.MAX_TRAITS_RELIGION;
                name = WS_WordCreator.Create();
                break;
            case EntityType.GOVERNMENT:
                minTraits = WS_Government.MIN_TRAITS_GOVERNMENT;
                maxTraits = WS_Government.MAX_TRAITS_GOVERNMENT;
                break;
        }

        float selector = 1.0f;

        while (selector > 0.0f)
        {
            addRandomTrait(tile);
            selector = Random.Range(minTraits - traits.Count - 0.99f, maxTraits - traits.Count - 0.1f);
        }

        if (type == EntityType.GOVERNMENT)
            ((WS_Government)this).Rename();

        capital = tile;
        color = new Color(Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f));
        sprite = WS_CoatManager.GetCoat();
    }


    public void Init(WS_Entity base_entity, WS_Tile tile)  
    {
        type = base_entity.type;
        name = WS_WordCreator.Create();

        switch (type)
        {
            case EntityType.CULTURE:
                minTraits = WS_Culture.MIN_TRAITS_CULTURE;
                maxTraits = WS_Culture.MAX_TRAITS_CULTURE;
                break;
            case EntityType.RELIGION:
                minTraits = WS_Religion.MIN_TRAITS_RELIGION;
                maxTraits = WS_Religion.MAX_TRAITS_RELIGION;
                break;
        }


        foreach (WS_Trait trait in base_entity.traits)
        {
            trait.Apply(this);
            traits.Add(trait);
        }

        int changeNum = Mathf.CeilToInt(Random.Range(0.25f * base_entity.traits.Count, 0.5f * base_entity.traits.Count));

        while (changeNum > 0)
        {
            addRandomTrait(base_entity.capital);
            changeNum--;
        }

        capital = tile;
        float r = base_entity.color.r; float g = base_entity.color.g; float b = base_entity.color.b;

        color = new Color(Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f));
        sprite = WS_CoatManager.GetCoat();
    }


    public void Init(WS_Entity base_entity_A, WS_Entity base_entity_B, WS_Tile tile)  
    {
        type = base_entity_A.type;
        name = WS_WordCreator.Create();

        if (type == EntityType.CULTURE) ((WS_Culture)this).merged = true;
        if (type == EntityType.RELIGION) ((WS_Religion)this).merged = true;

        minTraits = Mathf.Max(base_entity_A.minTraits, base_entity_B.minTraits);
        maxTraits = Mathf.Max(base_entity_A.maxTraits, base_entity_B.maxTraits);

        int traitNum = (base_entity_A.traits.Count + base_entity_B.traits.Count) / 2;

        for (int i = 0; i < traitNum; i++)
        {
            bool valid_A = false;
            bool valid_B = false;


            if (traitNum < base_entity_A.traits.Count)
                if (!traits.Contains(base_entity_A.traits[traitNum]))
                {
                    valid_A = true;

                    foreach (WS_Trait ownedTrait in traits)
                    {
                        if (ownedTrait.Group() == base_entity_A.traits[traitNum].Group())
                        {
                            valid_A = false;
                            break;
                        }
                    }
                }

            if (traitNum < base_entity_B.traits.Count)
                if (!traits.Contains(base_entity_B.traits[traitNum]))
                {
                    valid_B = true;

                    foreach (WS_Trait ownedTrait in traits)
                    {
                        if (ownedTrait.Group() == base_entity_B.traits[traitNum].Group())
                        {
                            valid_B = false;
                            break;
                        }
                    }
                }


            if (valid_A && !valid_B)
                traits.Add(base_entity_A.traits[traitNum]);

            else if (!valid_A && valid_B)
                traits.Add(base_entity_B.traits[traitNum]);

            else if (valid_A && valid_B)
            {
                float selector = Random.Range(0.0f, 1.0f);

                if (selector < 0.5f)
                    traits.Add(base_entity_A.traits[traitNum]);
                else
                    traits.Add(base_entity_B.traits[traitNum]);
            }

            if (valid_A || valid_B)
                traits[traits.Count - 1].Apply(this);
        }

        for (int i = traits.Count; i < minTraits; i++)
            addRandomTrait(tile);

        capital = tile;
        color = (base_entity_A.color + base_entity_B.color) / 2.0f;
        sprite = WS_CoatManager.GetCoat();
    }


    public bool addRandomTrait(WS_Tile tile)
    {
        if (traits.Count == maxTraits)
            return false;


        List<WS_Trait> possibleTraits = new List<WS_Trait>();
        float totalChance = 0.0f;

        List<WS_Trait> availableTraits = null;

        switch(type)
        {
            case EntityType.CULTURE: availableTraits = WS_World.cultureTraits; break;
            case EntityType.RELIGION: availableTraits = WS_World.religionTraits; break;
            case EntityType.GOVERNMENT: availableTraits = WS_World.governmentTraits; break;
        }

        foreach (WS_Trait trait in availableTraits)
        {
            bool valid = true;

            foreach (WS_Trait ownedTrait in traits)
                if (ownedTrait.Group() == trait.Group())
                {
                    valid = false;
                    break;
                }

            float chance = trait.Chance(tile);

            if (chance > 0.0f && valid)
            {
                totalChance += chance;
                possibleTraits.Add(trait);
            }
        }


        float selector = Random.Range(0.0f, totalChance);

        foreach (WS_Trait trait in possibleTraits)
        {
            float chance = trait.Chance(tile);

            if (selector <= chance)
            {
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
        if (traits.Count == minTraits)
            return false;

        float totalChance = 0.0f;

        foreach (WS_Trait trait in traits)
        {
            float chance = trait.Chance(tile);

            if (chance > 0.0f)
                totalChance += 1.0f - chance;

        }

        float selector = Random.Range(0.0f, totalChance);

        foreach (WS_Trait trait in traits)
        {
            float chance = trait.Chance(tile);

            if (selector <= 1.0f - chance)
            {
                trait.Reverse(this);
                traits.Remove(trait);
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
        {
            if (addRandomTrait(tile))
                valid = true;
        }
        else if (addRandomTrait(tile))
        {
            if (removeRandomTrait(tile))
                valid = true;
        }

        return valid;
    }
}
