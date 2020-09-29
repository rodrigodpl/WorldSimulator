using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Culture : WS_Entity
{
    public static int MAX_TRAITS_CULTURE = 4;
    public static int MIN_TRAITS_CULTURE = 2;

    public int collapsed = 0;
    public bool merged = false;

    public float FoodEfficiency = 1.15f;  
    public float expansionism = 0.0f;
    public float healthcare = 0.05f;

    public float influenceBonus = 5.0f;
    public float influenceMul = 1.0f;
    public float syncretism = 5.0f;

    public WS_Culture(WS_Tile tile) { Init(tile, EntityType.CULTURE); }

    public WS_Culture(WS_Culture baseEntity, WS_Tile tile) { Init(baseEntity, tile); }

    public WS_Culture(WS_Culture baseEntityA, WS_Culture baseEntityB, WS_Tile tile) { Init(baseEntityA, baseEntityB, tile); }
}