using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_PopulationPanel : MonoBehaviour
{
    private InputField populationField = null;
    private InputField foodField = null;
    private InputField foodEfficiencyField = null;
    private InputField sanitationField = null;
    private InputField healthcareField = null;
    private InputField growthField = null;
    private InputField prosperityField = null;

    void Start()
    {
        populationField = transform.Find("PopulationInputField").GetComponent<InputField>();
        foodField = transform.Find("FoodInputField").GetComponent<InputField>();
        foodEfficiencyField = transform.Find("FoodEfficiencyInputField").GetComponent<InputField>();
        sanitationField = transform.Find("SanitationInputField").GetComponent<InputField>();
        healthcareField = transform.Find("HealthcareInputField").GetComponent<InputField>();
        growthField = transform.Find("GrowthInputField").GetComponent<InputField>();
        prosperityField = transform.Find("ProsperityInputField").GetComponent<InputField>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                populationField.interactable = true;
                foodField.interactable = true;
                foodEfficiencyField.interactable = true;
                sanitationField.interactable = true;
                healthcareField.interactable = true;
                growthField.interactable = true;
                prosperityField.interactable = true;
            }
            else
            {
                populationField.interactable = false;
                foodField.interactable = false;
                foodEfficiencyField.interactable = false;
                sanitationField.interactable = false;
                healthcareField.interactable = false;
                growthField.interactable = false;
                prosperityField.interactable = false;

                populationField.text = WSUI_Controller.selectedTile.population.ToString();
                foodField.text = WSUI_Controller.selectedTile.foodUnits.ToString();
                foodEfficiencyField.text = WSUI_Controller.selectedTile.foodEfficiency.ToString();
                sanitationField.text = WSUI_Controller.selectedTile.sanitation.ToString();
                healthcareField.text = WSUI_Controller.selectedTile.healthcare.ToString();
                growthField.text = WSUI_Controller.selectedTile.lastPopGrowth.ToString();
                prosperityField.text = WSUI_Controller.selectedTile.prosperity.ToString();
            }
        }
    }

    public void changePopulation()
    {
        WSUI_Controller.selectedTile.population = float.Parse(populationField.text);
    }

    public void changeFood()
    {
        WSUI_Controller.selectedTile.foodUnits = float.Parse(foodField.text);
    }

    public void changeFoodEfficiency()
    {
        WSUI_Controller.selectedTile.foodEfficiency = float.Parse(foodEfficiencyField.text);
    }

    public void changeSanitation()
    {
        WSUI_Controller.selectedTile.sanitation = int.Parse(sanitationField.text);
    }

    public void changeHealthcare()
    {
        WSUI_Controller.selectedTile.healthcare = float.Parse(healthcareField.text);
    }

    public void changeGrowth()
    {
        WSUI_Controller.selectedTile.lastPopGrowth = float.Parse(growthField.text);
    }

    public void changeProsperity()
    {
        WSUI_Controller.selectedTile.prosperity = float.Parse(prosperityField.text);
    }

}
