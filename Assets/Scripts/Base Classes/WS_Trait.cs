using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitGroup { EXPANSION, FOOD_EFFICIENCY, HEALTHCARE, SURVIVALISM, INFLUENCE, SYNCRETISM, 
                         RELIGION_POWER, RELIGION_CORRUPTION, POWER_DISTRIBUTION, POWER_HOLDER, CENTRALIZATION, 
                         AUTHORITARIANISM, MAX_GROUPS }

public class WS_Trait
{
    virtual public string traitName() { return "WS_Trait"; }
    virtual public string traitDesc() { return "WS_TraitDesc"; }

    virtual public void Apply(WS_Entity entity) { }
    virtual public void Reverse(WS_Entity entity) { }

    virtual public TraitGroup Group() { return TraitGroup.MAX_GROUPS; }

    virtual public float Chance(WS_Tile tile) { return 0.0f; }
}
