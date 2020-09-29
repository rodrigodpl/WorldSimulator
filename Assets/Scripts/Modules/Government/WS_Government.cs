using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerDistribution  { NONE, AUTOCRACY, OLIGARCHY, RULING_COUNCIL, DEMOCRACY }
public enum PowerHolder        { NONE, PEOPLE, CHURCH, NOBILITY, RULER}

public class WS_Government : WS_Entity
{
    public static int MAX_TRAITS_GOVERNMENT = 4;
    public static int MIN_TRAITS_GOVERNMENT = 4;

    public WS_Culture rulingCulture = null;
    public WS_Religion rulingReligion = null;
     
    public float unrest = 0.0f;
    public float unrestCultural = 0.0f;
    public float unrestReligious = 0.0f;
    public float unrestMul = 0.0f;
    public float baseRepression = 1.0f;
    public float repression = 1.0f;

    public WS_Tile rebels = null;
    public WS_Tile rebelsCultural = null;
    public WS_Tile rebelsReligious = null;
     
    public string name = "";
     
    //TODO
    //    add main menu
    //    add world creation menu
    //    add tile viewer
    //    add entity viewer
    //    add tech tree?
    //    add war view?

    public List<WS_Government> borderingGovernments = new List<WS_Government>();
    public List<float> borderingOpinions = new List<float>();

    public List<WS_Treaty> treaties = new List<WS_Treaty>();


    public int soldierPool = 0;
    public float baseProfessionalism = 0.7f;
    public float armyProfessionalism = 1.0f;
    public float warScore = 50.0f;
    public float commandPower = 0.2f;
    public int warNum = 0;
    public bool battleFought = false;

    public EventModule preferredTech = EventModule.POPULATION;

    public WS_Government(WS_Tile tile) { Init(tile, EntityType.GOVERNMENT); Rename(); }

    public void Rename()
    {
        PowerDistribution powerDistribution = PowerDistribution.AUTOCRACY;
        PowerHolder powerHolder = PowerHolder.PEOPLE;

        //foreach(WS_Trait trait in traits)
        //{
        //    if(trait.Group() == TraitGroup.POWER_DISTRIBUTION)
        //    {

        //    }
        //}

        switch (powerDistribution)
        {
            case PowerDistribution.AUTOCRACY:

                switch(powerHolder)
                {
                    case PowerHolder.PEOPLE:    name = "Elective Monarchy";  break;
                    case PowerHolder.CHURCH:    name = "Papacy"; break;
                    case PowerHolder.NOBILITY:  name = "Electorate"; break;
                    case PowerHolder.RULER:     name = "Empire"; break;
                }
                break;

            case PowerDistribution.OLIGARCHY:

                switch(powerHolder)
                {
                    case PowerHolder.PEOPLE:    name = "Plutocracy";  break;
                    case PowerHolder.CHURCH:    name = "Ecclesiastical State"; break;
                    case PowerHolder.NOBILITY:  name = "Oligarchy"; break;
                    case PowerHolder.RULER:     name = "Feudal State"; break;
                }
                break;


            case PowerDistribution.RULING_COUNCIL:

                switch (powerHolder)
                {
                    case PowerHolder.PEOPLE:    name = "Democratical Assembly"; break;
                    case PowerHolder.CHURCH:    name = "Theocracy"; break;
                    case PowerHolder.NOBILITY:  name = "War Council"; break;
                    case PowerHolder.RULER:     name = "Regent Council"; break;
                }
                break;


            case PowerDistribution.DEMOCRACY:

                switch (powerHolder)
                {
                    case PowerHolder.PEOPLE:    name = "Democratic State"; break;
                    case PowerHolder.CHURCH:    name = "Clergy State"; break;
                    case PowerHolder.NOBILITY:  name = "Republic"; break;
                    case PowerHolder.RULER:     name = "Demarchy"; break;
                }
                break;
        }
    }


    public void CreateSchism(WS_Tile origin, EventModule module)
    {
        List<WS_Tile> closedTiles = new List<WS_Tile>();
        List<WS_Tile> openTiles = new List<WS_Tile>();

        float unrestPower = 500.0f;

        openTiles.Add(origin);

        while(openTiles.Count > 0)
        {
            WS_Tile currentTile = openTiles[0];

            foreach(WS_Tile neighbor in currentTile.Neighbors())
            {
                if (neighbor.population == 0.0f)
                    continue;

                if (neighbor.government != origin.government || origin.government.capital == neighbor)
                    continue;

                switch(module)
                {
                    case EventModule.POPULATION:
                        if(unrestPower > 100.0f && !openTiles.Contains(neighbor) && !closedTiles.Contains(neighbor))
                        {
                            unrestPower -= (100.0f - currentTile.unrest);
                            openTiles.Add(neighbor);
                        }
                        break;

                    case EventModule.CULTURE:
                        if (neighbor.culture == origin.culture && !openTiles.Contains(neighbor) && !closedTiles.Contains(neighbor))
                            openTiles.Add(neighbor);
                        break;

                    case EventModule.RELIGION:
                        if (neighbor.religion == origin.religion && !openTiles.Contains(neighbor) && !closedTiles.Contains(neighbor))
                            openTiles.Add(neighbor);
                        break;
                }
            }

            closedTiles.Add(currentTile);
            openTiles.RemoveAt(0);
        }

        if(closedTiles.Count > 4)
        {
            WS_Government newGov = new WS_Government(origin);
            newGov.rulingCulture = origin.culture;
            newGov.rulingReligion = origin.religion;
            newGov.preferredTech = (EventModule)Mathf.FloorToInt(Random.Range(0, (int)EventModule.MAX - 1));

            foreach (WS_Tile tile in closedTiles)
            {
                tile.government = newGov;
                tile.unrest = tile.unrestCultural = tile.unrestReligious = 0.0f;
            }
        }
    }

}
