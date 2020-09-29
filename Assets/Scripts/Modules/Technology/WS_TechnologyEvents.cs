using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_TechSelection : WS_BaseEvent
{
    public WS_TechSelection() { eventName = "Tech Selection"; module = EventModule.TECHNOLOGY; }

    protected override bool FireCheck()
    {
        return tile.currentTech == null;
    }

    protected override void Success()
    {
        List<WS_Tech> weightedTechs = new List<WS_Tech>();

        int preferredBonus = 0;
        foreach(WS_Tech tech in tile.availableTech)
        {
            weightedTechs.Add(tech);

            if(tech.module == tile.government.preferredTech)
            {
                weightedTechs.Add(tech);
                weightedTechs.Add(tech);
            }

            int selector = Random.Range(0, weightedTechs.Count);
            preferredBonus = tech.module == tile.government.preferredTech ? 200 : 0;

            tile.currentTech = weightedTechs[selector];
        }
    }
}

public class WS_TechResearch : WS_BaseEvent
{
    public WS_TechResearch() { eventName = "Tech Research"; module = EventModule.TECHNOLOGY; }

    protected override bool FireCheck()
    {
        return tile.currentTech != null;
    }

    protected override bool SuccessCheck()
    {
        return tile.scholars + tile.storedTechPoints > Random.Range(0.8f * tile.currentTech.cost, 1.2f * tile.currentTech.cost);
    }


    protected override void Success()
    {
        tile.currentTech.Apply(tile);
        tile.availableTech.Remove(tile.currentTech);
        tile.researchedTech.Add(tile.currentTech);

        foreach(WS_Tech tech in WS_World.techs)
        {
            if(!tile.availableTech.Contains(tech) && !tile.researchedTech.Contains(tech))
            {
                bool unlock = true;

                foreach(string requirement in tech.requirements)
                {
                    if (unlock)
                    {
                        unlock = false;

                        foreach (WS_Tech researchedTech in tile.researchedTech)
                            if (researchedTech.name == requirement)
                                unlock = true;
                    }
                }

                if (unlock)
                    tile.availableTech.Add(tech);
            }
        }

        tile.currentTech = null;
        tile.storedTechPoints = 0.0f;
    }


    protected override void Fail()
    {
        tile.storedTechPoints += tile.scholars * 0.1f;
    }
}


public class WS_TechSpread : WS_BaseEvent
{
    public WS_TechSpread() { eventName = "Tech Spread"; module = EventModule.TECHNOLOGY; }

    WS_Tech spreadTech = null;

    protected override bool SuccessCheck()
    {
        spreadTech = null;

        foreach (WS_Tile neighbor in tile.Neighbors())
        {
            foreach (WS_Tech tech in neighbor.researchedTech)
                if (tile.availableTech.Contains(tech) && tile.currentTech != tech)
                {
                    spreadTech = tech;
                    return (neighbor.researchedTech.Count / Mathf.Max(1.0f, tile.researchedTech.Count)) * 0.002f > Random.Range(0.0f, 1.0f);
                }
        }

        return false;
    }

    protected override void Success()
    {
        spreadTech.Apply(tile);
        tile.availableTech.Remove(spreadTech);
        tile.researchedTech.Add(spreadTech);


        foreach (WS_Tech tech in WS_World.techs)
        {
            if (!tile.availableTech.Contains(tech) && !tile.researchedTech.Contains(tech))
            {
                bool unlock = true;

                foreach (string requirement in tech.requirements)
                {
                    if (unlock)
                    {
                        unlock = false;

                        foreach (WS_Tech researchedTech in tile.researchedTech)
                            if (researchedTech.name == requirement)
                                unlock = true;
                    }
                }

                if (unlock)
                    tile.availableTech.Add(tech);
            }
        }

    }
}