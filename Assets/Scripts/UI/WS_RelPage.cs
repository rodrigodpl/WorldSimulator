using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_RelPage : MonoBehaviour
{
    private InputField powerField           = null;
    private InputField corruptionField      = null;
    private InputField influenceField       = null;
    private InputField syncretismField      = null;

    void Start()
    {
        powerField              = transform.GetChild(0).GetComponent<InputField>();
        corruptionField         = transform.GetChild(1).GetComponent<InputField>();
        influenceField          = transform.GetChild(2).GetComponent<InputField>();
        syncretismField         = transform.GetChild(3).GetComponent<InputField>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            powerField.interactable             = true;
            corruptionField.interactable        = true;
            influenceField.interactable         = true;
            syncretismField.interactable        = true;
        }
        else
        {
            powerField.interactable             = false;
            corruptionField.interactable        = false;
            influenceField.interactable         = false;
            syncretismField.interactable        = false;

            powerField.text             = WS_TilePanel.selectedTile.religion.power.ToString();
            corruptionField.text        = WS_TilePanel.selectedTile.religion.corruption.ToString();
            influenceField.text         = WS_TilePanel.selectedTile.religion.influenceBonus.ToString();
            syncretismField.text        = WS_TilePanel.selectedTile.religion.syncretism.ToString();
        }
    }

    public void changePower()
    {
        WS_TilePanel.selectedTile.religion.power = float.Parse(powerField.text);
    }

    public void changeCorruption()
    {
        WS_TilePanel.selectedTile.religion.corruption = float.Parse(corruptionField.text);
    }

    public void changeInfluence()
    {
        WS_TilePanel.selectedTile.religion.influenceBonus = float.Parse(influenceField.text);
    }

    public void changeSyncretism()
    {
        WS_TilePanel.selectedTile.religion.syncretism = float.Parse(syncretismField.text);
    }

}
