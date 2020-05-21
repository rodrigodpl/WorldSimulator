using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Culture : WS_Entity
{
    public static float MAX_TRAITS_CULTURE = 3.0f;
    public static float MIN_TRAITS_CULTURE = 3.0f;
    public static float MAX_TRAITS_TRIBAL = 3.0f;
    public static float MIN_TRAITS_TRIBAL = 1.0f;

    public float FoodEfficiency = 1.15f;  
    public float survivalism = 0.0f;  
    public float expansionism = 0.0f;
    public float healthcare = 0.05f;

    public WS_Culture(WS_Tile tile) { Init(tile, EntityType.CULTURE); }

    public WS_Culture(WS_Culture baseEntity, WS_Tile tile) { Init(baseEntity, tile); }

    public WS_Culture(WS_Culture baseEntityA, WS_Culture baseEntityB, WS_Tile tile) { Init(baseEntityA, baseEntityB, tile); }
}