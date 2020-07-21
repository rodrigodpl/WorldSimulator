using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_TechSelection : WS_BaseEvent
{
    public WS_TechSelection() { eventName = "Tech Selection"; module = EventModule.TECHNOLOGY; }

    protected override bool SuccessCheck()
    {
        tile.government.battleFought = false;

        return tile.government.warNum > 0;
    }

    protected override void Success()
    {
    }
}
