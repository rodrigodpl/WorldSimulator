using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WS_InfPage : MonoBehaviour
{
    private InputField sanitationField      = null;
    private InputField foodField            = null;
    private InputField healthcareField      = null;
    private InputField decadenceField       = null;
    private InputField cultureField         = null;
    private InputField religiousField       = null;
    private InputField constructionField    = null;

    void Start()
    {
        sanitationField     = transform.GetChild(0).GetComponent<InputField>();
        foodField           = transform.GetChild(1).GetComponent<InputField>();
        healthcareField     = transform.GetChild(2).GetComponent<InputField>();
        decadenceField      = transform.GetChild(3).GetComponent<InputField>();
        cultureField        = transform.GetChild(4).GetComponent<InputField>();
        religiousField      = transform.GetChild(5).GetComponent<InputField>();
        constructionField   = transform.GetChild(6).GetComponent<InputField>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            sanitationField.interactable    = true;
            foodField.interactable          = true;
            healthcareField.interactable    = true;
            decadenceField.interactable     = true;
            cultureField.interactable       = true;
            religiousField.interactable     = true;
            constructionField.interactable  = true;
        }
        else
        {
            sanitationField.interactable    = false;
            foodField.interactable          = false;
            healthcareField.interactable    = false;
            decadenceField.interactable     = false;
            cultureField.interactable       = false;
            religiousField.interactable     = false;
            constructionField.interactable  = false;

            sanitationField.text    = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.SANITATION].ToString();
            foodField.text          = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.FOOD].ToString();
            healthcareField.text    = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE].ToString();
            decadenceField.text     = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.UNREST].ToString();
            cultureField.text       = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CULTURE].ToString();
            religiousField.text     = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.RELIGION].ToString();
            constructionField.text  = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION].ToString();
        }
    }

    public void changeSanitation()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.SANITATION];
        WS_World.infrastructure[(int)InfrastructureType.SANITATION].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.SANITATION].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.SANITATION] = int.Parse(sanitationField.text);
    }

    public void changeFood()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.FOOD];
        WS_World.infrastructure[(int)InfrastructureType.FOOD].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.FOOD].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.FOOD] = int.Parse(foodField.text);
    }

    public void changeHealthcare()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE];
        WS_World.infrastructure[(int)InfrastructureType.HEALTHCARE].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.HEALTHCARE].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE] = int.Parse(healthcareField.text);
    }

    public void changeDecadence()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.UNREST];
        WS_World.infrastructure[(int)InfrastructureType.UNREST].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.UNREST].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.UNREST] = int.Parse(decadenceField.text);
    }

    public void changeCulture()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CULTURE];
        WS_World.infrastructure[(int)InfrastructureType.CULTURE].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.CULTURE].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CULTURE] = int.Parse(cultureField.text);
    }

    public void changeReligion()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.RELIGION];
        WS_World.infrastructure[(int)InfrastructureType.RELIGION].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.RELIGION].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.RELIGION] = int.Parse(religiousField.text);
    }

    public void changeConstruction()
    {
        int prevLevel = WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION];
        WS_World.infrastructure[(int)InfrastructureType.CONSTRUCTION].Reverse(WS_TilePanel.selectedTile, prevLevel);
        WS_World.infrastructure[(int)InfrastructureType.CONSTRUCTION].Apply(WS_TilePanel.selectedTile, int.Parse(sanitationField.text));

        WS_TilePanel.selectedTile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION] = int.Parse(constructionField.text);
    }
}