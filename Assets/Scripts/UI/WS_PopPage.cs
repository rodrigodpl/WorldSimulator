using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_PopPage : MonoBehaviour
{
    private InputField populationField  = null;
    private InputField sanitationField  = null;
    private InputField growthField      = null;
    private InputField foodField        = null;

    void Start()
    {
        populationField = transform.GetChild(0).GetComponent<InputField>();
        sanitationField = transform.GetChild(1).GetComponent<InputField>();
        growthField     = transform.GetChild(2).GetComponent<InputField>();
        foodField       = transform.GetChild(3).GetComponent<InputField>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            populationField.interactable = true;
            sanitationField.interactable = true;
        }
        else
        {
            populationField.interactable = false;
            sanitationField.interactable = false;

            populationField.text    = WS_TilePanel.selectedTile.population.ToString();
            sanitationField.text    = WS_TilePanel.selectedTile.sanitation.ToString();
            growthField.text        = WS_TilePanel.selectedTile.lastPopGrowth.ToString();
            foodField.text          = WS_TilePanel.selectedTile.foodUnits.ToString();
        }
    }

    public void changePopulation()
    {
        WS_TilePanel.selectedTile.population = float.Parse(populationField.text);
    }

    public void changeSanitation()
    {
        WS_TilePanel.selectedTile.sanitation = float.Parse(sanitationField.text);
    }

}
