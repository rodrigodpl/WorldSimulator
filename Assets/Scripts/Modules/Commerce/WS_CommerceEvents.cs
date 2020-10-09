using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_CommerceCaravan
{
    public WS_ResourceStack resStack = null;
    public WS_Tile currentTile = null;

    public int travelTime = 0;
    public int currentTime = 0;
    public int money = 0;
}


public class WS_ResourceConsumptionEvent : WS_BaseEvent
{
    public WS_ResourceConsumptionEvent() { eventName = "Resource Consumption"; module = EventModule.COMMERCE; }

    protected override void Success()
    {
        float prosperityMul = 1.001f;

        float resFoodBonus = 0.8f;
        float resWarBonus = 0.8f;
        float resConstructionBonus = 0.8f;
        float resUnrestBonus = 0.8f;
        float resProsperityBonus = 0.8f;

        foreach (WS_ResourceStack stack in tile.resStacks)
        {
            if (stack.type == tile.resource.type)
            {
                stack.amount = (int)(tile.population / 10.0f);
                stack.quality = tile.resource.quality;
            }
            else if (stack.amount > 0)
            {
                stack.amount = Mathf.Max(0, stack.amount - (int)tile.population / 1000);
                switch(stack.type)
                {
                    // METALS
                    case ResourceType.IRON:     resWarBonus +=          0.3f * stack.quality; break;
                    case ResourceType.COPPER:   resWarBonus +=          0.2f * stack.quality; break;
                    case ResourceType.TIN:      resWarBonus +=          0.1f * stack.quality; break;
                    case ResourceType.SILVER:   resProsperityBonus +=   0.2f * stack.quality; break;
                    case ResourceType.GOLD:     resProsperityBonus +=   0.3f * stack.quality; break;
                    case ResourceType.COAL:     resConstructionBonus += 0.3f * stack.quality; break;
                                                
                    // STONE                    
                    case ResourceType.GRANITE:  resConstructionBonus += 0.2f * stack.quality; break;
                    case ResourceType.CLAY:     resConstructionBonus += 0.3f * stack.quality; break;
                    case ResourceType.MARBLE:   resUnrestBonus +=       0.1f * stack.quality; break;
                    case ResourceType.JADE:     resProsperityBonus +=   0.2f * stack.quality; break;
                    case ResourceType.SALT:     resFoodBonus +=         0.3f * stack.quality; break;
                    case ResourceType.WOOD:     resConstructionBonus += 0.2f * stack.quality; break;
                                                
                    // ORGANIC                  
                    case ResourceType.PASTURES: resFoodBonus +=         0.2f * stack.quality; break;
                    case ResourceType.FISH:     resFoodBonus +=         0.2f * stack.quality; break;
                    case ResourceType.HUNT:     resFoodBonus +=         0.1f * stack.quality; break;
                    case ResourceType.FURS:     resProsperityBonus +=   0.1f * stack.quality; break;
                    case ResourceType.SPICES:   resUnrestBonus +=       0.2f * stack.quality; break;
                    case ResourceType.OPIOIDS:  resUnrestBonus +=       0.3f * stack.quality; break;
                }
            }
            else
            {
                stack.quality = 0;
                prosperityMul -= 0.003f / (int)ResourceType.MAX;
            }
        
        }

        tile.resFoodBonus           = Mathf.Lerp(tile.resFoodBonus, resFoodBonus, 0.1f);
        tile.resWarBonus            = Mathf.Lerp(tile.resWarBonus, resWarBonus, 0.1f);
        tile.resConstructionBonus   = Mathf.Lerp(tile.resConstructionBonus, resConstructionBonus, 0.1f);
        tile.resUnrestBonus         = Mathf.Lerp(tile.resUnrestBonus, resUnrestBonus, 0.1f);
        tile.resProsperityBonus     = Mathf.Lerp(tile.resProsperityBonus, resProsperityBonus, 0.1f);

        tile.prosperity *= prosperityMul;
        tile.prosperity = Mathf.Lerp(tile.prosperity, tile.population, 0.01f);
    }
}


public class WS_CaravanJourneyEvent : WS_BaseEvent
{
    public WS_CaravanJourneyEvent() { eventName = "Caravan Journey"; module = EventModule.COMMERCE; }

    protected override void Success()
    {
        WS_CommerceCaravan caravan = tile.caravan;

        if (caravan == null)
        {
            int traders = 2 + tile.baseCitizens;
            WS_CommerceCaravan newCaravan = new WS_CommerceCaravan();

            WS_ResourceStack resStack = new WS_ResourceStack(tile.resource.type);
            resStack.quality = tile.resource.quality * tile.qualityBonus;
            resStack.amount = (int)(50.0f * traders * tile.resource.abundance * tile.exploitationBonus);

            newCaravan.travelTime = traders;
            newCaravan.currentTile = tile;
            newCaravan.resStack = resStack;

            tile.caravan = newCaravan;
            return;

        }
        else
        {
            WS_Tile dest = caravan.currentTile;

            int currentAmount = caravan.currentTile.resStacks[(int)caravan.resStack.type].amount;

            bool trading = false;
            bool embargo = false;

            foreach (WS_Treaty treaty in tile.government.treaties)
                if (treaty.target == dest.government)
                {
                    if (treaty.type == TreatyType.TRADE_AGREEMENT || treaty.type == TreatyType.ALLIANCE)
                        trading = true;
                    else if (treaty.type == TreatyType.TRADE_EMBARGO || treaty.type == TreatyType.WAR)
                        embargo = true;
                }

            float bestPrice = 1.0f - (currentAmount / (caravan.currentTile.population / 10.0f)) * caravan.resStack.Price();

            if (trading) bestPrice *= 1.15f;
            else if (embargo) bestPrice = 0;

            foreach (WS_Tile neighbor in caravan.currentTile.Neighbors())
            {
                trading = false;
                embargo = false;

                foreach (WS_Treaty treaty in tile.government.treaties)
                    if (treaty.target == neighbor.government)
                    {
                        if (treaty.type == TreatyType.TRADE_AGREEMENT || treaty.type == TreatyType.ALLIANCE)
                            trading = true;
                        else if (treaty.type == TreatyType.TRADE_EMBARGO || treaty.type == TreatyType.WAR)
                            embargo = true;
                    }

                int neighborAmount = neighbor.resStacks[(int)caravan.resStack.type].amount;
                float expectedPrice = (1.0f - (neighborAmount / (neighbor.population / 10.0f))) * caravan.resStack.Price();

                if (trading) expectedPrice *= 1.15f;
                else if (embargo) expectedPrice = 0;

                if (expectedPrice > bestPrice)
                {
                    bestPrice = expectedPrice;
                    dest = neighbor;
                }
            }

            caravan.currentTile = dest;

            if (bestPrice > 0)
            {
                int unitsSold = caravan.resStack.amount / caravan.travelTime;

                WS_ResourceStack destStack = caravan.currentTile.resStacks[(int)caravan.resStack.type];

                destStack.quality = (caravan.resStack.quality * unitsSold + destStack.quality * destStack.amount) / (destStack.amount + unitsSold);
                destStack.amount += unitsSold;

                caravan.money += unitsSold * (int)bestPrice;
                dest.prosperity += unitsSold * (int)bestPrice * 0.1f * dest.tradingbonus;

                if (tile.culture != dest.culture)
                {
                    tile.culture.syncretism += 1.0f;
                    dest.culture.syncretism += 1.0f;
                }
                if (tile.religion != dest.religion)
                {
                    tile.religion.syncretism += 1.0f;
                    dest.religion.syncretism += 1.0f;
                }
            }

            caravan.currentTime++;

            if (caravan.currentTime >= caravan.travelTime)
            {
                float prosperityGain = caravan.money * tile.resProsperityBonus;
                tile.religion.influenceMul += prosperityGain * tile.religion.corruption * 0.001f;
                prosperityGain *= (1.0f / tile.religion.corruption);

                tile.prosperity += prosperityGain;
                tile.caravan = null;
            }

        }
    }
}
