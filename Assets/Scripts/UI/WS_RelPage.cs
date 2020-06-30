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
    private InputField decadenceField       = null;

    void Start()
    {
        powerField              = transform.GetChild(0).GetComponent<InputField>();
        corruptionField         = transform.GetChild(1).GetComponent<InputField>();
        influenceField          = transform.GetChild(2).GetComponent<InputField>();
        syncretismField         = transform.GetChild(3).GetComponent<InputField>();
        decadenceField          = transform.GetChild(4).GetComponent<InputField>();

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
            decadenceField.interactable         = true;
        }
        else
        {
            powerField.interactable             = false;
            corruptionField.interactable        = false;
            influenceField.interactable         = false;
            syncretismField.interactable        = false;
            decadenceField.interactable         = false;

            powerField.text             = WS_TilePanel.selectedTile.religion.power.ToString();
            corruptionField.text        = WS_TilePanel.selectedTile.religion.corruption.ToString();
            influenceField.text         = WS_TilePanel.selectedTile.religion.influenceBonus.ToString();
            syncretismField.text        = WS_TilePanel.selectedTile.religion.syncretism.ToString();
            decadenceField.text         = WS_TilePanel.selectedTile.religion.decadence.ToString();
        }
    }

    public void changePower()
    {
        WS_TilePanel.selectedTile.culture.FoodEfficiency = float.Parse(powerField.text);
    }

    public void changeCorruption()
    {
        WS_TilePanel.selectedTile.culture.survivalism = float.Parse(corruptionField.text);
    }

    public void changeInfluence()
    {
        WS_TilePanel.selectedTile.culture.influenceBonus = float.Parse(influenceField.text);
    }

    public void changeSyncretism()
    {
        WS_TilePanel.selectedTile.culture.syncretism = float.Parse(syncretismField.text);
    }

    public void changeDecadence()
    {
        WS_TilePanel.selectedTile.culture.decadence = float.Parse(decadenceField.text);
    }
}
