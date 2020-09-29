using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_CulPage : MonoBehaviour
{
    private InputField foodEfficiencyField  = null;
    private InputField expansionismField    = null;
    private InputField healthcareField      = null;
    private InputField influenceField       = null;
    private InputField syncretismField      = null;

    void Start()
    {
        foodEfficiencyField     = transform.GetChild(0).GetComponent<InputField>();
        expansionismField       = transform.GetChild(1).GetComponent<InputField>();
        healthcareField         = transform.GetChild(2).GetComponent<InputField>();
        influenceField          = transform.GetChild(3).GetComponent<InputField>();
        syncretismField         = transform.GetChild(4).GetComponent<InputField>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            foodEfficiencyField.interactable    = true;
            expansionismField.interactable      = true;
            healthcareField.interactable        = true;
            influenceField.interactable         = true;
            syncretismField.interactable        = true;
        }
        else
        {
            foodEfficiencyField.interactable    = false;
            expansionismField.interactable      = false;
            healthcareField.interactable        = false;
            influenceField.interactable         = false;
            syncretismField.interactable        = false;

            foodEfficiencyField.text    = WS_TilePanel.selectedTile.culture.FoodEfficiency.ToString();
            expansionismField.text      = WS_TilePanel.selectedTile.culture.expansionism.ToString();
            healthcareField.text        = WS_TilePanel.selectedTile.culture.healthcare.ToString();
            influenceField.text         = WS_TilePanel.selectedTile.culture.influenceBonus.ToString();
            syncretismField.text        = WS_TilePanel.selectedTile.culture.syncretism.ToString();
        }
    }

    public void changeFoodEfficiency()
    {
        WS_TilePanel.selectedTile.culture.FoodEfficiency = int.Parse(foodEfficiencyField.text);
    }

    public void changeExpansionism()
    {
        WS_TilePanel.selectedTile.culture.expansionism = float.Parse(expansionismField.text);
    }

    public void changeHealthcare()
    {
        WS_TilePanel.selectedTile.culture.healthcare = float.Parse(healthcareField.text);
    }

    public void changeInfluence()
    {
        WS_TilePanel.selectedTile.culture.influenceBonus = float.Parse(influenceField.text);
    }

    public void changeSyncretism()
    {
        WS_TilePanel.selectedTile.culture.syncretism = float.Parse(syncretismField.text);
    }

}
