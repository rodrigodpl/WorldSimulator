using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Nation
{
    public List<WS_Tile> nationTiles = new List<WS_Tile>();
    public WS_Tile capital = null;

    public Color nationColor = Color.white;
    
    public WS_Culture rulingCulture        = null;
}

