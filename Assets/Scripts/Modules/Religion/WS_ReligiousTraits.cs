using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  RELIGION - Power
public class GoverningChurchTrait : WS_Trait
{
    public override string traitName() { return "Governing Church"; }
    public override string traitDesc() { return "Power ++"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power += 0.5f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power -= 0.5f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class PowerfulPriestsTrait : WS_Trait
{
    public override string traitName() { return "Powerful Priests"; }
    public override string traitDesc() { return "Power +"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power += 0.25f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power -= 0.25f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AgnosticsTrait : WS_Trait
{
    public override string traitName() { return "Agnostics"; }
    public override string traitDesc() { return "Power -"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power -= 0.25f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power += 0.25f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AtheistsTrait : WS_Trait
{
    public override string traitName() { return "Atheists"; }
    public override string traitDesc() { return "Power --"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_POWER; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).power -= 0.5f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).power += 0.5f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}



//  RELIGION - Corruption
public class MoneyHungryTrait : WS_Trait
{
    public override string traitName() { return "Money Hungry"; }
    public override string traitDesc() { return "Corruption ++"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption += 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption -= 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class ChurchDonationsTrait : WS_Trait
{
    public override string traitName() { return "Church Donations"; }
    public override string traitDesc() { return "Corruption +"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption += 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption -= 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AltruistsTrait : WS_Trait
{
    public override string traitName() { return "Altruists"; }
    public override string traitDesc() { return "Corruption -"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption -= 0.1f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption += 0.1f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class AsceticsTrait : WS_Trait
{
    public override string traitName() { return "Ascetics"; }
    public override string traitDesc() { return "Corruption --"; }
    public override TraitGroup Group() { return TraitGroup.RELIGION_CORRUPTION; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).corruption -= 0.2f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).corruption += 0.2f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}



//  RELIGION - Influence
public class InfluentialRelTrait : WS_Trait
{
    public override string traitName() { return "Influential"; }
    public override string traitDesc() { return "Influence ++"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).influenceBonus += 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).influenceBonus -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class OutwardnessRelTrait : WS_Trait
{
    public override string traitName() { return "Outwardness"; }
    public override string traitDesc() { return "Influence +"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).influenceBonus += 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).influenceBonus -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class InwardnessRelTrait : WS_Trait
{
    public override string traitName() { return "Inwardness"; }
    public override string traitDesc() { return "Influence -"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).influenceBonus -= 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).influenceBonus += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IsolationistsRelTrait : WS_Trait
{
    public override string traitName() { return "Isolationists"; }
    public override string traitDesc() { return "Influence --"; }
    public override TraitGroup Group() { return TraitGroup.INFLUENCE; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).influenceBonus -= 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).influenceBonus += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


//  RELIGION - Syncretism
public class SyncreticRelTrait : WS_Trait
{
    public override string traitName() { return "Syncretic"; }
    public override string traitDesc() { return "Syncretism ++"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).syncretism += 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).syncretism -= 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}

public class TolerantRelTrait : WS_Trait
{
    public override string traitName() { return "Tolerant"; }
    public override string traitDesc() { return "Syncretism +"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).syncretism += 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).syncretism -= 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class IntolerantRelTrait : WS_Trait
{
    public override string traitName() { return "Intolerant"; }
    public override string traitDesc() { return "Syncretism -"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).syncretism -= 1.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).syncretism += 1.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.2f;
    }
}

public class RepressiveRelTrait : WS_Trait
{
    public override string traitName() { return "Repressive"; }
    public override string traitDesc() { return "Syncretism --"; }
    public override TraitGroup Group() { return TraitGroup.SYNCRETISM; }

    public override void Apply(WS_Entity entity) { ((WS_Religion)entity).syncretism -= 2.0f; }
    public override void Reverse(WS_Entity entity) { ((WS_Religion)entity).syncretism += 2.0f; }

    public override float Chance(WS_Tile tile)
    {
        return 0.1f;
    }
}


