using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_DisasterUIController : MonoBehaviour
{
    Image droughtImage  = null;
    Image floodImage    = null;
    Image tsunamiImage  = null;
    Image plagueImage   = null;

    void Start()
    {
        droughtImage = transform.GetChild(0).GetComponent<Image>();
        floodImage = transform.GetChild(1).GetComponent<Image>();
        tsunamiImage = transform.GetChild(2).GetComponent<Image>();
        plagueImage = transform.GetChild(3).GetComponent<Image>();

        droughtImage.enabled = floodImage.enabled = tsunamiImage.enabled = plagueImage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (WS_TilePanel.selectedTile != null)
        {
            if (WS_TilePanel.selectedTile.disaster != null)
            {
                switch (WS_TilePanel.selectedTile.disaster.Type())
                {
                    case DisasterType.DROUGHT: droughtImage.enabled = true; break;
                    case DisasterType.FLOOD: floodImage.enabled = true; break;
                    case DisasterType.TSUNAMI: tsunamiImage.enabled = true; break;
                    case DisasterType.PLAGUE: plagueImage.enabled = true; break;
                }
            }
            else
                droughtImage.enabled = floodImage.enabled = tsunamiImage.enabled = plagueImage.enabled = false;
        }
    }
}
