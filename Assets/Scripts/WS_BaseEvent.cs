using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventModule { NONE, POPULATION, CULTURE, DISASTER}

public class WS_BaseEvent 
{
    public string eventName = "Event";
    public EventModule module = EventModule.NONE;

    protected WS_Tile tile = null;

    public void Execute(WS_Tile _tile)
    {
        tile = _tile;

        if (FireCheck())
        {
            if (SuccessCheck())     Success();
            else                    Fail();
        }

    }

    virtual protected void Success() { }
    virtual protected void Fail() { }
            
    virtual protected bool FireCheck() { return true; }
    virtual protected bool SuccessCheck() { return true; }
}
