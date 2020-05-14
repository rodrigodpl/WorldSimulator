using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WS_UI_Panels { NONE, TILE, NATION, CULTURE}

public class WS_UIController : MonoBehaviour
{
    public WS_World world = null;
    public GameObject tilePanel = null;

    void Start()
    {
        world = GameObject.Find("GameController").GetComponent<WS_World>();
        tilePanel = GameObject.Find("TilePanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int tilePos = new Vector2Int(Mathf.FloorToInt((mousePos.x / world.realSize.x) * WS_World.sizeX), Mathf.FloorToInt((mousePos.y / world.realSize.y) * WS_World.sizeY));

            WS_TilePanel.selectedTile = world.GetTile(tilePos);
        }

        if (WS_TilePanel.selectedTile != null)
        {
            if (!WS_TilePanel.selectedTile.seaBody)
            {
                tilePanel.SetActive(true);

                if (WS_TilePanel.selectedTile.population <= 0.0f)
                    tilePanel.GetComponent<WS_TilePanel>().setPage(0);
                
                return;
            }
        }

        tilePanel.SetActive(false);
    }


}
