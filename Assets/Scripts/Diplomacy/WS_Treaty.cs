using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreatyType {  WAR, TRADE_EMBARGO, PEACE, TRADE_AGREEMENT, ALLIANCE }

public class WS_Treaty
{ 
    public WS_Government target = null;

    public int remainingDuration = 0;
    public TreatyType type = TreatyType.PEACE;
}
