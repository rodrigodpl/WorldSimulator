using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {  NONE = -1,
                            IRON, COPPER, TIN, LEAD, SILVER, GOLD,
                            WOOD, PASTURES, FISH, HUNT, COAL, FURS, SPICES, OPIOIDS,
                            GRANITE, CLAY, MARBLE, JADE, SALT,
                            MAX}

public class WS_ResourceStack
{
    public ResourceType type = ResourceType.NONE;
    public float quality = 0.0f;
    public int amount = 0;

    public WS_ResourceStack(ResourceType _type) { type = _type; }

    public int Price()
    {
        switch(type)
        {
            // METALS
            case ResourceType.IRON:     return (int)(10 * quality);
            case ResourceType.COPPER:   return (int)(12 * quality);
            case ResourceType.LEAD:     return (int)(15 * quality);
            case ResourceType.TIN:      return (int)(15 * quality);
            case ResourceType.SILVER:   return (int)(20 * quality);
            case ResourceType.GOLD:     return (int)(30 * quality);
                                               
            // STONE                           
            case ResourceType.GRANITE:  return (int)(10 * quality);
            case ResourceType.CLAY:     return (int)(12 * quality);
            case ResourceType.MARBLE:   return (int)(15 * quality);
            case ResourceType.JADE:     return (int)(20 * quality);
            case ResourceType.SALT:     return (int)(30 * quality);
                                               
            // ORGANIC                        
            case ResourceType.WOOD:     return (int)(10 * quality);
            case ResourceType.PASTURES: return (int)(12 * quality);
            case ResourceType.FISH:     return (int)(12 * quality);
            case ResourceType.HUNT:     return (int)(15 * quality);
            case ResourceType.FURS:     return (int)(15 * quality);
            case ResourceType.SPICES:   return (int)(20 * quality);
            case ResourceType.COAL:     return (int)(20 * quality);
            case ResourceType.OPIOIDS:  return (int)(30 * quality);
        }
        return 0;
    }
}

public class WS_ResourceSource
{
    public float abundance = 0.0f;
    public float quality = 0.0f;

    public ResourceType type = ResourceType.NONE;

    public WS_ResourceSource(ResourceType _type) { type = _type; }

    public bool isMetal()
    {
        return (type == ResourceType.IRON || type == ResourceType.COPPER || type == ResourceType.TIN ||
                type == ResourceType.LEAD || type == ResourceType.GOLD);
    }

    public bool isOrganic()
    {
        return (type == ResourceType.WOOD || type == ResourceType.PASTURES || type == ResourceType.FISH ||
                type == ResourceType.HUNT || type == ResourceType.COAL || type == ResourceType.FURS ||
                type == ResourceType.SPICES || type == ResourceType.OPIOIDS);
    }

    public bool isStone()
    {
        return (type == ResourceType.GRANITE || type == ResourceType.CLAY || type == ResourceType.MARBLE ||
                type == ResourceType.JADE || type == ResourceType.SALT);
    }

    public float Chance(WS_Tile tile)
    {
        float value = 0.0f;
        switch (type)
        {
            // METALS
            case ResourceType.IRON:     value = 0.5f; break;
            case ResourceType.COPPER:   value = 0.4f; break;
            case ResourceType.LEAD:     value = 0.3f; break;
            case ResourceType.TIN:      value = 0.3f; break;
            case ResourceType.SILVER:   value = 0.3f; break;
            case ResourceType.GOLD:     value = (tile.riverDirection.Count > 0 ? 0.1f : 0.05f); break;
                

            // STONE
            case ResourceType.GRANITE:  value = 0.5f; break;
            case ResourceType.CLAY:     value = 0.4f; break;
            case ResourceType.MARBLE:   value = 0.3f; break;
            case ResourceType.JADE:     value = 0.2f; break;
            case ResourceType.SALT:     value = (tile.IsCoastal() ? 0.1f : 0.05f); break;


            // ORGANIC
            case ResourceType.WOOD:

                if (tile.biome == Biome.BOREAL_FOREST || tile.biome == Biome.TROPICAL_JUNGLE ||
                    tile.biome == Biome.ALPINE_FOREST || tile.biome == Biome.TEMPERATE_FOREST)
                    value = 1.0f;

                break;

            case ResourceType.PASTURES:

                if (tile.biome == Biome.TEMPERATE_GRASSLAND || tile.biome == Biome.TROPICAL_GRASSLAND)
                    value = 1.0f;
                else if (tile.biome == Biome.SAVANNAH)
                    value = 0.5f;

                break;

            case ResourceType.FISH:

                if (tile.IsCoastal() || tile.biome == Biome.WETLANDS || tile.riverDirection.Count > 0)
                    value = 1.0f;

                break;

            case ResourceType.HUNT:
                if (tile.biome == Biome.TROPICAL_JUNGLE || tile.biome == Biome.SAVANNAH ||
                    tile.biome == Biome.ALPINE_FOREST || tile.biome == Biome.TEMPERATE_FOREST)
                    value = 1.0f;

                break;

            case ResourceType.FURS:
                if (tile.biome == Biome.TUNDRA || tile.biome == Biome.ALPINE)
                    value = 1.0f;

                break;

            case ResourceType.SPICES:
                if (tile.biome == Biome.TROPICAL_JUNGLE || tile.biome == Biome.TROPICAL_GRASSLAND || 
                    tile.biome == Biome.TEMPERATE_SHRUBLAND)
                    value = 1.0f;

                break;

            case ResourceType.OPIOIDS:
                if (tile.biome == Biome.TEMPERATE_SHRUBLAND || tile.biome == Biome.ALPINE_SHRUBLAND)
                    value = 1.0f;

                break;

            case ResourceType.COAL:
                value = 0.2f;
                break;

        }

        return value;
    }
}
