using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WS_UI_Panels { NONE, TILE, NATION, CULTURE}

public class WS_UIController : MonoBehaviour
{
    public WS_World world = null;

    WS_TilePanel tilePanel = null;
    WS_TilePanel nationPanel = null;
    WS_TilePanel culturePanel = null;

    void Start()
    {
        tilePanel = transform.GetChild(1).gameObject.GetComponent<WS_TilePanel>();
        nationPanel = transform.GetChild(2).gameObject.GetComponent<WS_TilePanel>();
        culturePanel = transform.GetChild(3).gameObject.GetComponent<WS_TilePanel>();

        tilePanel.gameObject.SetActive(false);
        nationPanel.gameObject.SetActive(false);
        culturePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int tilePos = new Vector2Int(Mathf.FloorToInt((mousePos.x / world.realSize.x) * WS_World.sizeX), Mathf.FloorToInt((mousePos.y / world.realSize.y) * WS_World.sizeY));

            WS_Tile tile = world.GetTile(tilePos);

            if (!tile.seaBody)
            {
                switch (WS_RenderPanel.rendermode)
                {
                    case WorldRenderMode.POPULATION: nationPanel.gameObject.SetActive(true); WS_TilePanel.selectedTile = tile; break;
                    case WorldRenderMode.CULTURE: culturePanel.gameObject.SetActive(true); WS_TilePanel.selectedTile = tile; break;
                    default: tilePanel.gameObject.SetActive(true); WS_TilePanel.selectedTile = tile; break;
                }
            }
        }
    }


}
