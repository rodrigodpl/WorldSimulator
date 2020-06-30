using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreatyType { NONE, TRADE_AGREEMENT, TRADE_EMBARGO, ALLIANCE, WAR}

public class WS_Treaty 
{
    public List<WS_Government> members = new List<WS_Government>();
    public List<WS_Government> targets = new List<WS_Government>();

    int remainingDuration = 0;
    public TreatyType type = TreatyType.NONE;
}
