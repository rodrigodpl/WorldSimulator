using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//  GOVERNMENT - Power Distribution
public class AutocracyTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_DISTRIBUTION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).unrestMul += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).unrestMul -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class OligarchyTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_DISTRIBUTION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).unrestMul += 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).unrestMul -= 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.15f;
    }
}

public class RulingCouncilTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_DISTRIBUTION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).unrestMul -= 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).unrestMul += 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class DemocracyTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_DISTRIBUTION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).unrestMul -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).unrestMul += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.5f;
    }
}

//  GOVERNMENT - Power Holder
public class RulerHolderTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_HOLDER; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism += 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism -= 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.3f;
    }
}

public class NobilityHolderTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_HOLDER; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class ChurchHolderTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_HOLDER; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class PeopleHolderTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.POWER_HOLDER; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism -= 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseProfessionalism += 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

//  GOVERNMENT - Centralization
public class CentralizedTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.CENTRALIZATION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).commandPower += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).commandPower -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class HierarchichalTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.CENTRALIZATION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).commandPower += 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).commandPower -= 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.15f;
    }
}

public class DistributedTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.CENTRALIZATION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).commandPower -= 0.05f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).commandPower += 0.05f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class LocalControlTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.CENTRALIZATION; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).commandPower -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).commandPower += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.25f;
    }
}


//  GOVERNMENT - Authoritarianism
public class NationalistTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.AUTHORITARIANISM; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseRepression += 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseRepression -= 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class RepressiveTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.AUTHORITARIANISM; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseRepression += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseRepression -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class TolerantTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.AUTHORITARIANISM; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseRepression -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseRepression += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class OpenViewsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.AUTHORITARIANISM; }

    public override void Apply(WS_Entity entity) { ((WS_Government)entity).baseRepression -= 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Government)entity).baseRepression += 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


