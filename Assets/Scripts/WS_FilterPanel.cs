using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum WorldRenderMode { GEOGRAPHY, POPULATION, CULTURE, RESOURCE, DISASTER, RELIGION }

public enum GeoFilter { ALTITUDE, TEMPERATURE, HUMIDITY, EROSION_STRENGTH, RIVER_STRENGTH, BIOME, HABITABILITY }
public enum PopFilter { NATION, POPULATION, GROWTH }
public enum CulFilter { CULTURE }
public enum ResFilter { RESOURCE }
public enum RelFilter { RELIGION }

public class WS_FilterPanel : MonoBehaviour
{
    public static GeoFilter geoFilter = GeoFilter.BIOME;
    public static PopFilter popFilter = PopFilter.NATION;
    public static CulFilter culFilter = CulFilter.CULTURE;
    public static ResFilter resFilter = ResFilter.RESOURCE;
    public static RelFilter relFilter = RelFilter.RELIGION;

    public static WorldRenderMode rendermode = WorldRenderMode.GEOGRAPHY;

    private GameObject ModuleDropdown = null;

    private GameObject GeographicDropdown = null;
    private GameObject PopulationDropdown = null;
    private GameObject CultureDropdown = null;
    private GameObject ResourceDropdown = null;
    private GameObject DisasterDropdown = null;
    private GameObject ReligionDropdown = null;

    private  GameObject lastDropdown = null;

    private void Start()
    {
        ModuleDropdown = GameObject.Find("ModuleDropdown");

        GeographicDropdown  = GameObject.Find("GeographicDropdown");
        PopulationDropdown  = GameObject.Find("PopulationDropdown");
        CultureDropdown     = GameObject.Find("CultureDropdown");
        ResourceDropdown    = GameObject.Find("ResourceDropdown");
        DisasterDropdown    = GameObject.Find("DisasterDropdown");
        ReligionDropdown    = GameObject.Find("ReligionDropdown");


        ModuleDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
            delegate { onRenderModeChange(ModuleDropdown.GetComponent<Dropdown>()); });


        GeographicDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
            delegate { onGeoFilterChanged(GeographicDropdown.GetComponent<Dropdown>()); });

        PopulationDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
            delegate { onPopFilterChanged(PopulationDropdown.GetComponent<Dropdown>()); });

        CultureDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
            delegate { onCulFilterChanged(CultureDropdown.GetComponent<Dropdown>()); });


        lastDropdown = GeographicDropdown;

        PopulationDropdown.SetActive(false);
        CultureDropdown.SetActive(false);
        ResourceDropdown.SetActive(false);
        DisasterDropdown.SetActive(false);
        ReligionDropdown.SetActive(false);
    }

    public void onRenderModeChange(Dropdown dropdown)
    {
        rendermode = (WorldRenderMode)dropdown.value;

        lastDropdown.SetActive(false);

        switch(rendermode)
        {
            case WorldRenderMode.GEOGRAPHY:     GeographicDropdown.SetActive(true); lastDropdown = GeographicDropdown; break;
            case WorldRenderMode.POPULATION:    PopulationDropdown.SetActive(true); lastDropdown = PopulationDropdown; break;
            case WorldRenderMode.CULTURE:       CultureDropdown.SetActive(true); lastDropdown = CultureDropdown; break;
            case WorldRenderMode.RESOURCE:      ResourceDropdown.SetActive(true); lastDropdown = ResourceDropdown; break;
            case WorldRenderMode.DISASTER:      DisasterDropdown.SetActive(true); lastDropdown = DisasterDropdown; break;
            case WorldRenderMode.RELIGION:      ReligionDropdown.SetActive(true); lastDropdown = ReligionDropdown; break;
        }
    }




    public void onGeoFilterChanged(Dropdown dropdown)
    {
        geoFilter = (GeoFilter)dropdown.value;
    }

    public void onPopFilterChanged(Dropdown dropdown)
    {
        popFilter = (PopFilter)dropdown.value;
    }

    public void onCulFilterChanged(Dropdown dropdown)
    {
        culFilter = (CulFilter)dropdown.value;
    }
}

