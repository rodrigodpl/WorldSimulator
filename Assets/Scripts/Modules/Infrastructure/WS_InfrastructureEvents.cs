using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureProductionEvent : WS_BaseEvent
{
    public InfrastructureProductionEvent() { eventName = "Infrastructure Production"; module = EventModule.INFRASTRUCTURE; }

    protected override bool FireCheck()
    {
        if (tile.plannedInfrastructure == null)
        {
            float totalChance = 0.0f;
            List<WS_Infrastructure> availableInf = new List<WS_Infrastructure>();

            foreach (WS_Infrastructure infrastructure in WS_World.infrastructure)
                if (tile.infrastructureLevels[(int)infrastructure.type] != 10)
                    availableInf.Add(infrastructure);


            List<WS_Infrastructure> desiredInf = new List<WS_Infrastructure>();
            foreach (WS_Infrastructure infrastructure in availableInf)
            {
                float chance = infrastructure.Chance(tile);

                if (chance > 0.0f)
                {
                    desiredInf.Add(infrastructure);
                    totalChance += chance;
                }
            }

            if (desiredInf.Count > 0)
            {
                float selector = Random.Range(0.0f, totalChance - 0.0001f);

                foreach(WS_Infrastructure infrastructure in desiredInf)
                {
                    float chance = infrastructure.Chance(tile);

                    if (selector < chance)
                    {
                        tile.plannedInfrastructure = infrastructure;
                        break;
                    }
                    else
                        selector -= chance;
                }
            }
            else if(availableInf.Count > 0)
            {
                int randIndex = Mathf.FloorToInt(Random.Range(0.0f, availableInf.Count - 0.01f));
                tile.plannedInfrastructure = availableInf[randIndex]; 
            }
        }

        return tile.plannedInfrastructure != null && tile.builders > 0;
    }

    protected override bool SuccessCheck()
    {
        float newPoints = 0.0f;

        newPoints += Random.Range(0.8f, 1.2f) * (tile.builders + tile.baseCitizens);

        newPoints *= ((1.0f + tile.constructionBonus) + tile.resConstructionBonus) / 2.0f;

        tile.infrastructurePoints += newPoints;

        float cost = tile.plannedInfrastructure.Cost(tile, tile.infrastructureLevels[(int)tile.plannedInfrastructure.type]);

        return cost < tile.infrastructurePoints;
    }

    protected override void Success()
    {
        if (tile.infrastructureLevels[(int)tile.plannedInfrastructure.type] > 1)
            WS_World.infrastructure[(int)tile.plannedInfrastructure.type].Reverse(tile, tile.infrastructureLevels[(int)tile.plannedInfrastructure.type]);

        tile.infrastructureLevels[(int)tile.plannedInfrastructure.type]++;
        WS_World.infrastructure[(int)tile.plannedInfrastructure.type].Apply(tile, tile.infrastructureLevels[(int)tile.plannedInfrastructure.type]);

        tile.plannedInfrastructure = null;
        tile.infrastructurePoints = 0;
    }
}
