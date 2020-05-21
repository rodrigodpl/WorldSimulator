using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Religion : WS_Entity
{
    public static float MAX_TRAITS_RELIGION = 3.0f;
    public static float MIN_TRAITS_RELIGION = 3.0f;
    public static float MAX_TRAITS_TRIBAL = 3.0f;
    public static float MIN_TRAITS_TRIBAL = 1.0f;

    public float corruption = 0.0f;
    public float power = 1.0f;

    public WS_Religion(WS_Tile tile) { Init(tile, EntityType.RELIGION); }

    public WS_Religion(WS_Religion baseEntity, WS_Tile tile) { Init(baseEntity, tile); }

    public WS_Religion(WS_Religion baseEntityA, WS_Religion baseEntityB, WS_Tile tile) { Init(baseEntityA, baseEntityB, tile); }
}
