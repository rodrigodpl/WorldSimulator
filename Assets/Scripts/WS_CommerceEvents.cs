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
        foreach(WS_ResourceStack stack in tile.resStacks)
        {
            if (stack.type == tile.resource.type)
            {
                stack.amount = (int)(tile.population / 10.0f);
                stack.quality = tile.resource.quality;
            }
            else
                stack.amount = Mathf.Max(0, stack.amount - (int)tile.population / 1000);
        }
    }
}


public class WS_CaravanJourneyEvent : WS_BaseEvent
{
    public WS_CaravanJourneyEvent() { eventName = "Caravan Journey"; module = EventModule.COMMERCE; }

    protected override void Success()
    {
        WS_CommerceCaravan caravan = tile.caravan;

        if(caravan == null)
        {
            if (tile.traders > 0)
            {
                WS_CommerceCaravan newCaravan = new WS_CommerceCaravan();

                WS_ResourceStack resStack = new WS_ResourceStack(tile.resource.type);
                resStack.quality = tile.resource.quality;
                resStack.amount = (int)(100.0f * tile.traders * tile.resource.abundance);

                newCaravan.travelTime = tile.traders;
                newCaravan.currentTile = tile;
                newCaravan.resStack = resStack;

                tile.caravan = newCaravan;
                return;
            }
        }
        else
        {
            WS_Tile dest = caravan.currentTile;

            int currentAmount = caravan.currentTile.resStacks[(int)caravan.resStack.type].amount;
            float bestPrice = 1.0f - (currentAmount / (caravan.currentTile.population / 10.0f)) * caravan.resStack.Price();

            foreach (WS_Tile neighbor in caravan.currentTile.Neighbors())
            {
                int neighborAmount = neighbor.resStacks[(int)caravan.resStack.type].amount;
                float expectedPrice = 1.0f - (neighborAmount / (neighbor.population / 10.0f)) * caravan.resStack.Price();  

                if(expectedPrice > bestPrice)
                {
                    bestPrice = expectedPrice;
                    dest = neighbor;
                }
            }

            caravan.currentTile = dest;
            int unitsSold = caravan.resStack.amount / caravan.travelTime;

            WS_ResourceStack destStack = caravan.currentTile.resStacks[(int)caravan.resStack.type];

            destStack.quality = (caravan.resStack.quality * unitsSold + destStack.quality * destStack.amount) / (destStack.amount + unitsSold);
            destStack.amount += unitsSold;

            caravan.money += unitsSold * (int)bestPrice;

            caravan.currentTime++;

            if(caravan.currentTime >= caravan.travelTime)
            {
                tile.prosperity += caravan.money;
                tile.caravan = null;
            }
        }
    }
}
