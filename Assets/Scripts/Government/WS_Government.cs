using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerDistribution  { NONE, AUTOCRACY, OLIGARCHY, RULING_COUNCIL, DEMOCRACY }
public enum PowerHolder        { NONE, PEOPLE, CHURCH, NOBILITY, RULER}
public enum Centralization     { NONE, CENTRALIZED, HIERARCHICAL, DISTRIBUTED, LOCAL}
public enum Authoritarianism   { NONE, NATIONALIST, REPRESSIVE, TOLERANT, OPEN }

public class WS_Government 
{
    public WS_Culture rulingCulture = null;
    public WS_Religion rulingReligion = null;
     
    public float legitimacy = 0.0f;
     
    public PowerDistribution powerDistribution = PowerDistribution.NONE;
    public PowerHolder powerHolder             = PowerHolder.NONE;
    public Centralization centralization       = Centralization.NONE;
    public Authoritarianism authoritarianism   = Authoritarianism.NONE;
     
    public string name = "";
     
    public Color nationColor = Color.white;
     
    public WS_Tile capital = null;

    public List<WS_Government> borderingGovernments = new List<WS_Government>();
    public List<float> borderingOpinions = new List<float>();

    public List<WS_Treaty> treaties = new List<WS_Treaty>();

    public WS_Government(WS_Tile tile)
    {
        powerDistribution   = (PowerDistribution)Mathf.CeilToInt(Random.Range(0.01f, 4.0f));
        powerHolder         = (PowerHolder)Mathf.CeilToInt(Random.Range(0.01f, 4.0f));
        centralization      = (Centralization)Mathf.CeilToInt(Random.Range(0.01f, 4.0f));
        authoritarianism    = (Authoritarianism)Mathf.CeilToInt(Random.Range(0.01f, 4.0f));

        capital = tile;

        nationColor = new Color(Random.Range(0.1f, 0.95f), Random.Range(0.1f, 0.95f), Random.Range(0.1f, 0.95f)); // dark tones

        Rename();
    }

    public void Rename()
    {
        switch(powerDistribution)
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
}
