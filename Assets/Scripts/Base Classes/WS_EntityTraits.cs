using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  CULTURE - Influence
public class InfluentialTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { entity.influenceBonus += 2.0f; }
    public override void Reverse(WS_Entity entity) { entity.influenceBonus -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class OutwardnessTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { entity.influenceBonus += 1.0f; }
    public override void Reverse(WS_Entity entity) { entity.influenceBonus -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class InwardnessTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { entity.influenceBonus -= 1.0f; }
    public override void Reverse(WS_Entity entity) { entity.influenceBonus += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IsolationistsTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { entity.influenceBonus -= 2.0f; }
    public override void Reverse(WS_Entity entity) { entity.influenceBonus += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  CULTURE - Influence
public class SyncreticTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { entity.syncretism += 2.0f; }
    public override void Reverse(WS_Entity entity) { entity.syncretism -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class TolerantTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { entity.syncretism += 1.0f; }
    public override void Reverse(WS_Entity entity) { entity.syncretism -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IntolerantTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { entity.syncretism -= 1.0f; }
    public override void Reverse(WS_Entity entity) { entity.syncretism += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class RepressiveTrait : WS_Trait
{
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { entity.syncretism -= 2.0f; }
    public override void Reverse(WS_Entity entity) { entity.syncretism += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

