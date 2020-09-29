using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyRecruitmentEvent : WS_BaseEvent
{
    public ArmyRecruitmentEvent() { eventName = "Army Recruitment"; module = EventModule.WAR; }

    protected override bool SuccessCheck()
    {
        tile.government.battleFought = false;

        return tile.government.warNum > 0;
    }

    protected override void Success()
    {
        int newTroops = 0;
        float professionalism = 1.0f;

        if (tile.soldiers > 0)
        {
            newTroops = tile.soldiers;
            tile.soldiers -= newTroops;
            professionalism = tile.armyBonus;

        }
        else if (tile.unrest < 30.0f && Random.Range(0.0f, 100.0f) > tile.unrest && tile.government.warScore < 30.0f)
        {
            newTroops = Mathf.CeilToInt(0.05f * (tile.population / 1000.0f));
            tile.unrest += 10.0f;
            professionalism = 0.7f;
        }

        if (newTroops > 0)
        {
            tile.government.soldierPool += newTroops * 10;
            tile.population -= newTroops * 1000.0f;
            tile.government.armyProfessionalism = Mathf.Lerp(tile.government.armyProfessionalism, professionalism, 
                newTroops / tile.government.soldierPool);
        }
    }

    protected override void Fail()
    {
        if(tile.government.soldierPool > 0)
        {
            tile.population += 1000.0f;
            tile.soldiers++;
            tile.government.soldierPool = Mathf.Max(0, tile.government.soldierPool - 10);

            if (tile.government.soldierPool == 0)
                tile.government.armyProfessionalism = 1.0f;
        }
    }
}

//war capital taken problem


public class BattleFoughtEvent : WS_BaseEvent
{
    WS_Tile target = null;
    int atkSoldiers = 0;
    int defSoldiers = 0;
    float powerRatio = 0.0f;

    public BattleFoughtEvent() { eventName = "BattleFought"; module = EventModule.WAR; }

    protected override bool FireCheck()
    {
        target = null;

        if (tile.government.warNum == 0)
            return false;

        if (tile.government.soldierPool > 0 && !tile.government.battleFought)
        {
            foreach (WS_Treaty treaty in tile.government.treaties)
            {
                if (treaty.type == TreatyType.WAR)
                {
                    foreach (WS_Tile neighbor in tile.Neighbors())
                        if (treaty.target == neighbor.government)
                            target = neighbor;
                }
            }
        }

        if (target != null)
        {
            if (target.government.soldierPool <= 0)
            {
                if (Random.Range(0.0f, 1.0f) < 0.3f)
                {
                    target.prosperity *= 0.9f;
                    target.government = tile.government;
                    target.unrest += 20.0f;
                    tile.government.battleFought = true;

                    float warScore = 0.0f;

                    if (target == target.government.capital || target == tile.government.capital)
                        warScore += 10.0f;
                    else
                        warScore += 3.0f;

                    tile.government.warScore += warScore;
                    target.government.warScore -= warScore;
                }
            }
            else 
                return Random.Range(0.0f, 1.0f) < 0.2f;
        }

        return false;
    }

    protected override bool SuccessCheck()
    {
        tile.government.battleFought = true;

        atkSoldiers = Mathf.CeilToInt(tile.government.soldierPool * tile.government.commandPower * Random.Range(0.5f, 1.5f));
        defSoldiers = Mathf.FloorToInt(Mathf.Min(target.government.soldierPool, atkSoldiers * Random.Range(0.8f, 1.5f)));

        float professionalismRatio = tile.government.armyProfessionalism / target.government.armyProfessionalism;

        float atkPower = atkSoldiers * professionalismRatio * Random.Range(0.3f, 1.0f);
        float defPower = Random.Range(0.0f, defSoldiers) * tile.defenseBonus;

        powerRatio = atkPower / defPower;

        return powerRatio > 1.0f;
    }

    protected override void Success()
    {
        int enemyCasualties = Mathf.CeilToInt(((-defSoldiers / powerRatio) + defSoldiers) * 0.5f);
        int alliedCasualties = Mathf.FloorToInt(enemyCasualties * (1.0f/ powerRatio));

        float warScore = (enemyCasualties * 80.0f / target.government.soldierPool);

        tile.government.soldierPool = Mathf.Max(0, tile.government.soldierPool - alliedCasualties);
        target.government.soldierPool = Mathf.Max(0, target.government.soldierPool - enemyCasualties);

        if (target == target.government.capital || target == tile.government.capital)
            warScore += 12.0f;
        else
            warScore += 5.0f;

        tile.government.warScore += warScore;
        target.government.warScore -= warScore;

        tile.government.armyProfessionalism += 0.1f;

        target.prosperity *= 0.9f;
        target.government = tile.government;
        target.unrest += 20.0f;
    }

    protected override void Fail()
    {
        int alliedCasualties = Mathf.CeilToInt((atkSoldiers * (1.0f - powerRatio)) * 0.5f);
        int enemyCasualties = Mathf.FloorToInt(powerRatio * alliedCasualties);

        float warScore = (alliedCasualties * 100.0f / tile.government.soldierPool);

        tile.government.soldierPool = Mathf.Max(0, tile.government.soldierPool - alliedCasualties);
        target.government.soldierPool = Mathf.Max(0, target.government.soldierPool - enemyCasualties);

        if (target == target.government.capital || target == tile.government.capital)
            warScore += 12.0f;
        else
            warScore += 5.0f;

        tile.government.armyProfessionalism -= 0.1f;

        tile.government.warScore -= warScore;
        target.government.warScore += warScore;
    }
}

public class MoveCapitalEvent : WS_BaseEvent
{
    List<WS_Tile> nationTiles = new List<WS_Tile>();
    protected override bool FireCheck()
    {
        return tile.government.capital.government != tile.government;
    }

    protected override bool SuccessCheck()
    {
        return nationTiles.Contains(tile);
    }


    protected override void Success()
    {
        WS_Tile higherPop = nationTiles[0];

        foreach (WS_Tile nat_tile in nationTiles)
            if (nat_tile.population > higherPop.population)
                higherPop = nat_tile;

        tile.government.capital = higherPop;

        nationTiles.Clear();
    }

    protected override void Fail()
    {
        if (nationTiles.Count != 0)
            if (nationTiles[0].government != tile.government)
                return;

        nationTiles.Add(tile);
    }
}

public class EndWarEvent : WS_BaseEvent
{
    WS_Treaty war = null;

    public EndWarEvent() { eventName = "End War"; module = EventModule.WAR; }

    protected override bool FireCheck()
    {
        war = null;

        if (tile.government.warNum > 0 && tile == tile.government.capital)
        {
            foreach (WS_Treaty treaty in tile.government.treaties)
            {
                if (treaty.type == TreatyType.WAR)
                {
                    if (!tile.government.borderingGovernments.Contains(treaty.target) || tile.government.warScore >= 100.0f)
                        war = treaty;
                }

            }
        }

        return war != null;
    }


    protected override void Success()
    {
        foreach(WS_Treaty treaty in war.target.treaties)
            if(treaty.target == tile.government && treaty.type == TreatyType.WAR)
            {
                war.type = treaty.type = TreatyType.TRUCE;
                war.remainingDuration = treaty.remainingDuration = 30;

                war.target.warNum--;
                tile.government.warNum--;

                tile.government.warScore = 50.0f;
                war.target.warScore = 50.0f;
            }
    }
}
