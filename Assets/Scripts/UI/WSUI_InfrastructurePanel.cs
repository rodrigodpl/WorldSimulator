using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_InfrastructurePanel : MonoBehaviour
{
    private Slider foodSlider = null;
    private Slider sanitationSlider = null;
    private Slider cultureSlider = null;
    private Slider unrestSlider = null;
    private Slider healthcareSlider = null;

    private Slider religionSlider = null;
    private Slider constructionSlider = null;
    private Slider warSlider = null;
    private Slider commerceSlider = null;
    private Slider technologySlider = null;

    void Start()
    {
        foodSlider = transform.Find("FoodSlider").GetComponent<Slider>();
        sanitationSlider = transform.Find("SanitationSlider").GetComponent<Slider>();
        cultureSlider = transform.Find("CultureSlider").GetComponent<Slider>();
        unrestSlider = transform.Find("UnrestSlider").GetComponent<Slider>();
        healthcareSlider = transform.Find("HealthcareSlider").GetComponent<Slider>();

        religionSlider = transform.Find("ReligionSlider").GetComponent<Slider>();
        constructionSlider = transform.Find("ConstructionSlider").GetComponent<Slider>();
        warSlider = transform.Find("WarSlider").GetComponent<Slider>();
        commerceSlider = transform.Find("CommerceSlider").GetComponent<Slider>();
        technologySlider = transform.Find("CommerceSlider").GetComponent<Slider>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                foodSlider.interactable = true;
                sanitationSlider.interactable = true;
                cultureSlider.interactable = true;
                unrestSlider.interactable = true;
                healthcareSlider.interactable = true;

                religionSlider.interactable = true;
                constructionSlider.interactable = true;
                warSlider.interactable = true;
                commerceSlider.interactable = true;
                technologySlider.interactable = true;
            }
            else
            {
                foodSlider.interactable = false;
                sanitationSlider.interactable = false;
                cultureSlider.interactable = false;
                unrestSlider.interactable = false;
                healthcareSlider.interactable = false;

                religionSlider.interactable = false;
                constructionSlider.interactable = false;
                warSlider.interactable = false;
                commerceSlider.interactable = false;
                technologySlider.interactable = false;

                foodSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.FOOD];
                sanitationSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.SANITATION];
                cultureSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.CULTURE];
                unrestSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.UNREST];
                healthcareSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE];

                religionSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.RELIGION];
                constructionSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION];
                warSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.WAR];
                commerceSlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.COMMERCE];
                technologySlider.value = WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.TECHNOLOGY];
            }
        }
    }

    public void changeInfFood()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.FOOD] = (int)foodSlider.value;
    }

    public void changeInfSanitation()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.SANITATION] = (int)sanitationSlider.value;
    }

    public void changeInfCulture()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.CULTURE] = (int)cultureSlider.value;
    }

    public void changeInfUnrest()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.UNREST] = (int)unrestSlider.value;
    }

    public void changeInfHealthcare()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.HEALTHCARE] = (int)healthcareSlider.value;
    }



    public void changeInfReligion()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.RELIGION] = (int)religionSlider.value;
    }

    public void changeInfConstruction()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.CONSTRUCTION] = (int)constructionSlider.value;
    }

    public void changeInfWar()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.WAR] = (int)warSlider.value;
    }

    public void changeInfCommerce()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.COMMERCE] = (int)commerceSlider.value;
    }

    public void changeInfTechnology()
    {
        WSUI_Controller.selectedTile.infrastructureLevels[(int)InfrastructureType.TECHNOLOGY] = (int)technologySlider.value;
    }
}
