using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreatyType {  WAR, TRADE_EMBARGO, NON_AGGRESSION, TRADE_AGREEMENT, ALLIANCE, TRUCE, NONE }

public class WS_Treaty
{ 
    public WS_Government target = null;

    public int remainingDuration = 0;
    public TreatyType type = TreatyType.NON_AGGRESSION;
}
