using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  RELIGION - Power
public class GoverningChurchTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power += 0.25f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power -= 0.25f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class PowerfulPriestsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AgnosticsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AtheistsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power -= 0.25f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power += 0.25f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}



//  RELIGION - Corruption
public class MoneyHungryTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption += 3.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption -= 3.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class ChurchDonationsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption += 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AltruistsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption -= 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AsceticsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption -= 3.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption += 3.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


