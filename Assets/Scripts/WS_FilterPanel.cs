using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum WorldRenderMode { GEOGRAPHY, POPULATION, CULTURE, RESOURCE, DISASTER }

public enum GeoFilter { ALTITUDE, TEMPERATURE, HUMIDITY, EROSION_STRENGTH, RIVER_STRENGTH, BIOME, HABITABILITY }
public enum PopFilter { NATION, POPULATION, GROWTH }
public enum CulFilter { CULTURE, MAX }
public enum ResFilter { RESOURCE, MAX }

public class WS_FilterPanel : MonoBehaviour
{
    public static GeoFilter geoFilter = GeoFilter.BIOME;
    public static PopFilter popFilter = PopFilter.NATION;
    public static CulFilter culFilter = CulFilter.CULTURE;

    public static WorldRenderMode rendermode = WorldRenderMode.GEOGRAPHY;

    public GameObject ModuleDropdown = null;

    public GameObject GeographicDropdown = null;
    public GameObject PopulationDropdown = null;
    public GameObject CultureDropdown = null;
    public GameObject ResourceDropdown = null;
    public GameObject DisasterDropdown = null;

    public GameObject lastDropdown = null;

    private void Start()
    {
        ModuleDropdown = GameObject.Find("ModuleDropdown");

        GeographicDropdown  = GameObject.Find("GeographicDropdown");
        PopulationDropdown  = GameObject.Find("PopulationDropdown");
        CultureDropdown     = GameObject.Find("CultureDropdown");
        ResourceDropdown    = GameObject.Find("ResourceDropdown");
        DisasterDropdown    = GameObject.Find("DisasterDropdown");


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

