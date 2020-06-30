using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UprisingResult { DISCONTENT, IMPROVED_RIGHTS, SCHISM, RULER_CHANGE}

public class UprisingReligiousEvent : WS_BaseEvent
{
    UprisingResult uprisingResult = UprisingResult.DISCONTENT;

    public UprisingReligiousEvent() { eventName = "Religious Uprising"; module = EventModule.GOVERNMENT; }

    protected override bool FireCheck()
    {
        if (tile.religion != tile.government.rulingReligion)
        {
            tile.government.legitimacy = Mathf.Max(tile.government.legitimacy - 0.003f, -500.0f);
            return Random.Range(0.0f, 1.0f) < -tile.government.legitimacy * 0.0001f;
        }
        else
            tile.government.legitimacy = Mathf.Min(tile.government.legitimacy + 0.001f, 500.0f);

        return false;
    }

    protected override bool SuccessCheck()
    {
        uprisingResult = UprisingResult.DISCONTENT; 
        float result = Random.Range(-0.75f, 0.25f) + tile.government.legitimacy * 0.001f;

        if (result > 0.0f)
        {
            if (result > 0.5f)      uprisingResult = UprisingResult.RULER_CHANGE;
            else if (result > 0.4f) uprisingResult = UprisingResult.SCHISM;
            else if (result > 0.2f) uprisingResult = UprisingResult.IMPROVED_RIGHTS;

            return true;
        }
        else
            return false;
    }

    protected override void Success()
    {
        switch(uprisingResult)
        {
            case UprisingResult.DISCONTENT:         tile.government.rulingReligion.influenceBonus -= 0.5f; break;
            case UprisingResult.IMPROVED_RIGHTS:    tile.religion.influenceBonus += 0.5f; break;
            case UprisingResult.SCHISM:             /* asdasdasdasdasdasdasdasdasdasdasd */ break;
            case UprisingResult.RULER_CHANGE:       tile.government.rulingReligion = tile.religion; break;
        }

        tile.government.legitimacy = 0.0f;
    }

    protected override void Fail()
    {
        tile.religion.influenceBonus = -0.5f;
        tile.government.legitimacy = 0.0f;
    }
}


public class UprisingCulturalEvent : WS_BaseEvent
{
    UprisingResult uprisingResult = UprisingResult.DISCONTENT;

    public UprisingCulturalEvent() { eventName = "Cultural Uprising"; module = EventModule.GOVERNMENT; }

    protected override bool FireCheck()
    {
        if (tile.culture != tile.government.rulingCulture)
        {
            tile.government.legitimacy = Mathf.Max(tile.government.legitimacy - 0.003f, -500.0f);
            return Random.Range(0.0f, 1.0f) < -tile.government.legitimacy * 0.0001f;
        }
        else
            tile.government.legitimacy = Mathf.Min(tile.government.legitimacy + 0.001f, 500.0f);

        return false;
    }

    protected override bool SuccessCheck()
    {
        uprisingResult = UprisingResult.DISCONTENT;
        float result = Random.Range(-0.75f, 0.25f) + tile.government.legitimacy * 0.001f;

        if (result > 0.0f)
        {
            if (result > 0.5f)      uprisingResult = UprisingResult.RULER_CHANGE;
            else if (result > 0.4f) uprisingResult = UprisingResult.SCHISM;
            else if (result > 0.2f) uprisingResult = UprisingResult.IMPROVED_RIGHTS;

            return true;
        }
        else
            return false;
    }

    protected override void Success()
    {
        switch (uprisingResult)
        {
            case UprisingResult.DISCONTENT:         tile.government.rulingCulture.influenceBonus -= 0.5f; break;
            case UprisingResult.IMPROVED_RIGHTS:    tile.culture.influenceBonus += 0.5f; break;
            case UprisingResult.SCHISM:             /* asdasdasdasdasdasdasdasdasdasdasd */ break;
            case UprisingResult.RULER_CHANGE:       tile.government.rulingCulture = tile.culture; break;
        }

        tile.government.legitimacy = 0.0f;
    }

    protected override void Fail()
    {
        tile.culture.influenceBonus = -0.5f;
        tile.government.legitimacy = 0.0f;
    }
}
