using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TilePages { GEO, POP, CUL, TEC }

public class WS_TilePanel : MonoBehaviour
{
    public static WS_Tile selectedTile = null;

    private GameObject GeoPage = null;
    private GameObject PopPage = null;
    private GameObject CulPage = null;
    private GameObject RelPage = null;

    private GameObject lastPage = null;

    private Outline GeoTabOutline = null;
    private Outline PopTabOutline = null;
    private Outline CulTabOutline = null;
    private Outline RelTabOutline = null;

    private Outline lastOutline = null;

    void Start()
    {
        GeoTabOutline = transform.GetChild(0).GetChild(0).GetComponent<Outline>();
        PopTabOutline = transform.GetChild(0).GetChild(1).GetComponent<Outline>();
        CulTabOutline = transform.GetChild(0).GetChild(2).GetComponent<Outline>();
        RelTabOutline = transform.GetChild(0).GetChild(3).GetComponent<Outline>();

        GeoPage = transform.GetChild(2).gameObject;
        PopPage = transform.GetChild(3).gameObject;
        CulPage = transform.GetChild(4).gameObject;
        RelPage = transform.GetChild(5).gameObject;

        lastPage = GeoPage;
        lastOutline = GeoTabOutline;
    }

    public void setPage(int page)
    {
        if (page != 0 && selectedTile.population <= 0.0f)
            return;

        lastPage.SetActive(false);
        lastOutline.enabled = false;

        switch ((TilePages)page)
        {
            case TilePages.GEO: GeoPage.SetActive(true); lastPage = GeoPage; GeoTabOutline.enabled = true; lastOutline = GeoTabOutline; break;
            case TilePages.POP: PopPage.SetActive(true); lastPage = PopPage; PopTabOutline.enabled = true; lastOutline = PopTabOutline; break;
            case TilePages.CUL: CulPage.SetActive(true); lastPage = CulPage; CulTabOutline.enabled = true; lastOutline = CulTabOutline; break;
            case TilePages.TEC: RelPage.SetActive(true); lastPage = RelPage; RelTabOutline.enabled = true; lastOutline = RelTabOutline; break;
        }
    }

}
