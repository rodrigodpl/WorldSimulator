﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitGroup { EXPANSION, FOOD_EFFICIENCY, HEALTHCARE, SURVIVALISM, INFLUENCE, SYNCRETISM, 
                         RELIGION_POWER, RELIGION_CORRUPTION, MAX_GROUPS }

public class WS_Trait
{
    public string traitName = "WS_Trait";

    virtual public void Apply(WS_Entity entity) { }
    virtual public void Reverse(WS_Entity entity) { }

    virtual public TraitGroup Group() { return TraitGroup.MAX_GROUPS; }

    virtual public float Chance(WS_Tile tile) { return 0.0f; }
}
