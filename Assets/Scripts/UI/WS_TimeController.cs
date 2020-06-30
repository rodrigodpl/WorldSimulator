using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SimulationSpeed { PAUSED, SLOW, NORMAL, FAST, FASTEST }

public class WS_TimeController : MonoBehaviour
{
    Text text = null;

    Outline outlinePause = null;
    Outline outlineSlow = null;
    Outline outlinePlay = null;
    Outline outlineFast = null;
    Outline outlineFastest = null;

    Outline lastOutline = null;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();

        outlinePause = transform.GetChild(1).GetComponent<Outline>();
        outlineSlow = transform.GetChild(2).GetComponent<Outline>();
        outlinePlay = transform.GetChild(3).GetComponent<Outline>();
        outlineFast = transform.GetChild(4).GetComponent<Outline>();
        outlineFastest = transform.GetChild(5).GetComponent<Outline>();

        lastOutline = outlinePlay;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = WS_World.year.ToString();
    }

    public void SetSpeed(int speed)
    {
        WS_World.speed = (SimulationSpeed) speed;

        lastOutline.enabled = false;

        switch(WS_World.speed)
        {
            case SimulationSpeed.PAUSED:    outlinePause.enabled = true; lastOutline = outlinePause; break;
            case SimulationSpeed.SLOW:      outlineSlow.enabled = true; lastOutline = outlineSlow; break;
            case SimulationSpeed.NORMAL:    outlinePlay.enabled = true; lastOutline = outlinePlay; break;
            case SimulationSpeed.FAST:      outlineFast.enabled = true; lastOutline = outlineFast; break;
            case SimulationSpeed.FASTEST:   outlineFastest.enabled = true; lastOutline = outlineFastest; break;
        }
    }

}
