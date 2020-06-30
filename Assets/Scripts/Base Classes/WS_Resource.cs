using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {  NONE,
                            IRON, COPPER, TIN, LEAD, SILVER, GOLD, URANIUM,
                            WOOD, PASTURES, FISH, HUNT, COAL, FURS, SPICES, OPIOIDS, OIL,
                            GRANITE, CLAY, MARBLE, JADE, SALT,
                            MAX_TYPES}

public class WS_Resource 
{
    public float amount = 0.0f;
    public float quality = 0.0f;
     
    public ResourceType type = ResourceType.NONE;

    public bool isMetal() {
        return (type == ResourceType.IRON || type == ResourceType.COPPER || type == ResourceType.TIN ||
                type == ResourceType.LEAD || type == ResourceType.GOLD || type == ResourceType.URANIUM);
    }

    public bool isOrganic()
    {
        return (type == ResourceType.WOOD || type == ResourceType.PASTURES || type == ResourceType.FISH ||
                type == ResourceType.HUNT || type == ResourceType.COAL || type == ResourceType.FURS ||
                type == ResourceType.SPICES || type == ResourceType.OPIOIDS || type == ResourceType.OIL);
    }

    public bool isStone()
    {
        return (type == ResourceType.GRANITE || type == ResourceType.CLAY || type == ResourceType.MARBLE ||
                type == ResourceType.JADE || type == ResourceType.SALT);
    }
}
