using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_TilePanel : MonoBehaviour
{
    public static WS_Tile selectedTile = null;

    Text popText = null;
    Text urbanText = null;

    void Start()
    {
        popText = transform.Find("Variable/Population").GetComponent<Text>();
        urbanText = transform.Find("Variable/UrbanPer").GetComponent<Text>();
    }

    private void Update()
    {
        popText.text = selectedTile.Population().ToString("F2");
        urbanText.text = selectedTile.urbanPercentile.ToString("F2");
    }
}
