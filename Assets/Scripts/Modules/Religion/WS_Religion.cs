using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Religion : WS_Entity
{
    public static int MAX_TRAITS_RELIGION = 2;
    public static int MIN_TRAITS_RELIGION = 4;

    public int collapsed = 0;
    public bool merged = false;

    public float influenceBonus = 5.0f;
    public float influenceMul = 1.0f;
    public float syncretism = 5.0f;

    public float corruption = 1.0f;
    public float power = 1.0f;

    public WS_Religion(WS_Tile tile) { Init(tile, EntityType.RELIGION); }

    public WS_Religion(WS_Religion baseEntity, WS_Tile tile) { Init(baseEntity, tile); }

    public WS_Religion(WS_Religion baseEntityA, WS_Religion baseEntityB, WS_Tile tile) { Init(baseEntityA, baseEntityB, tile); }
}
