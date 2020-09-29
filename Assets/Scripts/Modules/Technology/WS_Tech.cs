using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Tech
{
    public List<string> requirements = new List<string>();

    public string name = "";
    public int cost = 0;

    public EventModule module = EventModule.NONE;

    virtual public void Apply(WS_Tile tile) { }
}

public class WS_TechWarI : WS_Tech
{
    public WS_TechWarI()
    {
        cost = 100;
        module = EventModule.WAR;

        name = "War I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.armyBonus += 0.05f;
    }
}

public class WS_TechWarII : WS_Tech
{
    public WS_TechWarII()
    {
        cost = 200;
        module = EventModule.WAR;

        requirements.Add("War I");

        name = "War II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.armyBonus += 0.15f;
    }
}

public class WS_TechWarIII : WS_Tech
{
    public WS_TechWarIII()
    {
        cost = 400;
        module = EventModule.WAR;

        requirements.Add("War II");

        name = "War III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.armyBonus += 0.3f;
    }
}

public class WS_TechWarIV : WS_Tech
{
    public WS_TechWarIV()
    {
        cost = 800;
        module = EventModule.WAR;

        requirements.Add("War III");
        name = "War IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.armyBonus += 0.5f;
    }
}


public class WS_TechDefenseI : WS_Tech
{
    public WS_TechDefenseI()
    {
        cost = 250;
        module = EventModule.WAR;

        requirements.Add("War I");
        name = "Defense I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.defenseBonus += 0.05f;
    }
}

public class WS_TechDefenseII : WS_Tech
{
    public WS_TechDefenseII()
    {
        cost = 500;
        module = EventModule.WAR;

        requirements.Add("Construction II");
        requirements.Add("Defense I");

        name = "Defense II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.defenseBonus += 0.15f;
    }
}

public class WS_TechDefenseIII : WS_Tech
{
    public WS_TechDefenseIII()
    {
        cost = 1000;
        module = EventModule.WAR;

        requirements.Add("War III");
        requirements.Add("Construction III");
        requirements.Add("Defense II");

        name = "Defense III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.defenseBonus += 0.3f;
    }
}

public class WS_TechConstructionI : WS_Tech
{
    public WS_TechConstructionI()
    {
        cost = 100;
        module = EventModule.INFRASTRUCTURE;

        name = "Construction I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.constructionBonus += 0.05f;
    }
}

public class WS_TechConstructionII : WS_Tech
{
    public WS_TechConstructionII()
    {
        cost = 200;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Construction I");

        name = "Construction II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.constructionBonus += 0.15f;
    }
}

public class WS_TechConstructionIII : WS_Tech
{
    public WS_TechConstructionIII()
    {
        cost = 400;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Construction II");

        name = "Construction III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.constructionBonus += 0.3f;
    }
}

public class WS_TechConstructionIV : WS_Tech
{
    public WS_TechConstructionIV()
    {
        cost = 800;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Construction III");

        name = "Construction IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.constructionBonus += 0.5f;
    }
}

public class WS_TechSanitationI : WS_Tech
{
    public WS_TechSanitationI()
    {
        cost = 250;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Construction I");

        name = "Sanitation I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.sanitation += 10.0f;
    }
}

public class WS_TechSanitationII : WS_Tech
{
    public WS_TechSanitationII()
    {
        cost = 500;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Culture II");
        requirements.Add("Sanitation I");

        name = "Sanitation II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.sanitation += 30.0f;
    }
}

public class WS_TechSanitationIII : WS_Tech
{
    public WS_TechSanitationIII()
    {
        cost = 1000;
        module = EventModule.INFRASTRUCTURE;

        requirements.Add("Construction III");
        requirements.Add("Culture III");
        requirements.Add("Sanitation II");

        name = "Sanitation III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.sanitation += 60.0f;
    }
}

public class WS_TechCultureI : WS_Tech
{
    public WS_TechCultureI()
    {
        cost = 100;
        module = EventModule.CULTURE;

        name = "Culture I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.cultureBonus += 0.5f;
    }
}

public class WS_TechCultureII : WS_Tech
{
    public WS_TechCultureII()
    {
        cost = 200;
        module = EventModule.CULTURE;

        requirements.Add("Culture I");

        name = "Culture II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.cultureBonus += 1.5f;
    }
}


public class WS_TechCultureIII : WS_Tech
{
    public WS_TechCultureIII()
    {
        cost = 400;
        module = EventModule.CULTURE;

        requirements.Add("Culture II");

        name = "Culture III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.cultureBonus += 3.0f;
    }
}

public class WS_TechCultureIV : WS_Tech
{
    public WS_TechCultureIV()
    {
        cost = 800;
        module = EventModule.CULTURE;

        requirements.Add("Culture III");

        name = "Culture IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.cultureBonus += 5.0f;
    }
}

public class WS_TechExploitationI : WS_Tech
{
    public WS_TechExploitationI()
    {
        cost = 250;
        module = EventModule.COMMERCE;

        requirements.Add("Culture I");
        requirements.Add("Commerce I");

        name = "Exploitation I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.exploitationBonus += 0.15f;
    }
}

public class WS_TechExploitationII : WS_Tech
{
    public WS_TechExploitationII()
    {
        cost = 500;
        module = EventModule.CULTURE;

        requirements.Add("Exploitation I");

        name = "Exploitation II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.exploitationBonus += 0.35f;
    }
}

public class WS_TechExploitationIII : WS_Tech
{
    public WS_TechExploitationIII()
    {
        cost = 1000;
        module = EventModule.COMMERCE;

        requirements.Add("Exploitation II");
        requirements.Add("Culture III");
        requirements.Add("COmmerce III");

        name = "Exploitation III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.exploitationBonus += 0.6f;
    }
}

public class WS_TechCommerceI : WS_Tech
{
    public WS_TechCommerceI()
    {
        cost = 100;
        module = EventModule.COMMERCE;

        name = "Commerce I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.qualityBonus += 0.05f;
    }
}

public class WS_TechCommerceII : WS_Tech
{
    public WS_TechCommerceII()
    {
        cost = 200;
        module = EventModule.COMMERCE;

        requirements.Add("Commerce I");

        name = "Commerce II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.qualityBonus += 0.15f;
    }
}

public class WS_TechCommerceIII : WS_Tech
{
    public WS_TechCommerceIII()
    {
        cost = 400;
        module = EventModule.COMMERCE;

        requirements.Add("Commerce II");

        name = "Commerce I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.qualityBonus += 0.3f;
    }
}

public class WS_TechCommerceIV : WS_Tech
{
    public WS_TechCommerceIV()
    {
        cost = 800;
        module = EventModule.COMMERCE;

        requirements.Add("Commerce III");

        name = "Commerce IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.qualityBonus += 0.5f;
    }
}

public class WS_TechAdministrationI : WS_Tech
{
    public WS_TechAdministrationI()
    {
        cost = 250;
        module = EventModule.POPULATION;

        requirements.Add("Population I");

        name = "Administration I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.unrestDecay -= 0.05f;
    }
}

public class WS_TechAdministrationII : WS_Tech
{
    public WS_TechAdministrationII()
    {
        cost = 500;
        module = EventModule.POPULATION;

        requirements.Add("Commerce II");
        requirements.Add("Administration I");

        name = "Administration II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.unrestDecay -= 0.1f;
    }
}

public class WS_TechAdministrationIII : WS_Tech
{
    public WS_TechAdministrationIII()
    {
        cost = 1000;
        module = EventModule.POPULATION;

        requirements.Add("Commerce III");
        requirements.Add("Administration II");
        requirements.Add("Population III");

        name = "Administration III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.unrestDecay -= 0.15f;
    }
}

public class WS_TechPopulationI : WS_Tech
{
    public WS_TechPopulationI()
    {
        cost = 100;
        module = EventModule.POPULATION;

        name = "Population I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.foodEfficiency += 0.02f;
    }
}

public class WS_TechPopulationII : WS_Tech
{
    public WS_TechPopulationII()
    {
        cost = 200;
        module = EventModule.POPULATION;

        requirements.Add("Population I");

        name = "Population II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.foodEfficiency += 0.03f;
    }
}

public class WS_TechPopulationIII : WS_Tech
{
    public WS_TechPopulationIII()
    {
        cost = 400;
        module = EventModule.POPULATION;

        requirements.Add("Population II");

        name = "Population III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.foodEfficiency += 0.05f;
    }
}

public class WS_TechPopulationIV : WS_Tech
{
    public WS_TechPopulationIV()
    {
        cost = 800;
        module = EventModule.POPULATION;

        requirements.Add("Population III");

        name = "Population IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.foodEfficiency += 0.1f;
    }
}

public class WS_TechHealthcareI : WS_Tech
{
    public WS_TechHealthcareI()
    {
        cost = 250;
        module = EventModule.RELIGION;

        requirements.Add("Religion I");

        name = "Healthcare I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.healthcare += 0.05f;
    }
}

public class WS_TechHealthcareII : WS_Tech
{
    public WS_TechHealthcareII()
    {
        cost = 500;
        module = EventModule.RELIGION;

        requirements.Add("Population II");
        requirements.Add("Healthcare I");

        name = "Healthcare II";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.healthcare += 0.1f;
    }
}

public class WS_TechHealthcareIII : WS_Tech
{
    public WS_TechHealthcareIII()
    {
        cost = 1000;
        module = EventModule.RELIGION;

        requirements.Add("Population III");
        requirements.Add("Healthcare II");
        requirements.Add("Religion III");

        name = "Healthcare III";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.healthcare += 0.2f;
    }
}

public class WS_TechReligionI : WS_Tech
{
    public WS_TechReligionI()
    {
        cost = 100;
        module = EventModule.RELIGION;

        name = "Religion I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.religionBonus += 0.5f;
    }
}

public class WS_TechReligionII : WS_Tech
{
    public WS_TechReligionII()
    {
        cost = 200;
        module = EventModule.RELIGION;

        requirements.Add("Religion I");

        name = "Religion I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.religionBonus += 1.5f;
    }
}

public class WS_TechReligionIII : WS_Tech
{
    public WS_TechReligionIII()
    {
        cost = 400;
        module = EventModule.RELIGION;

        requirements.Add("Religion II");

        name = "Religion I";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.religionBonus += 3.0f;
    }
}

public class WS_TechReligionIV : WS_Tech
{
    public WS_TechReligionIV()
    {
        cost = 800;
        module = EventModule.RELIGION;

        requirements.Add("Religion III");

        name = "Religion IV";
    }

    public override void Apply(WS_Tile tile)
    {
        tile.religionBonus += 5.0f;
    }
}
