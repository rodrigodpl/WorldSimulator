using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WorldRenderMode { GEOGRAPHY, POPULATION, CULTURE, MAX}

public enum GeoFilter { ALTITUDE, TEMPERATURE, HUMIDITY, EROSION_STRENGTH, RIVER_STRENGTH, BIOME, HABITABILITY, MAX }
public enum PopFilter { NATION, GROWTH, URBAN_PERCENTILE, POPULATION, MAX }
public enum CulFilter { CULTURE, MAX }

public class WS_RenderPanel : MonoBehaviour
{
    public GameObject filterTab = null;

    public static GeoFilter geoFilter = GeoFilter.BIOME;
    public static PopFilter popFilter = PopFilter.NATION;
    public static CulFilter culFilter = CulFilter.CULTURE;

    public static WorldRenderMode rendermode = WorldRenderMode.GEOGRAPHY;

    public void onDisplayClicked(int mode)
    {
        filterTab.SetActive(false);
        filterTab = transform.GetChild(1).GetChild(mode).gameObject;
        filterTab.SetActive(true);

        rendermode = (WorldRenderMode)mode;
    }


    public void onGeoFilterClicked(int mode)
    {
        geoFilter = (GeoFilter)mode;
    }

    public void onPopFilterClicked(int mode)
    {
        popFilter = (PopFilter)mode;
    }

    public void onCulFilterClicked(int mode)
    {
        culFilter = (CulFilter)mode;
    }
}
