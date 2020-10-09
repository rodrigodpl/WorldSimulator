using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_TechnologyPanel : MonoBehaviour
{
    private InputField techBonusField = null;
    private Text nameText = null;
    private Slider progressSlider = null;

    void Start()
    {
        techBonusField = transform.Find("TechBonusInputField").GetComponent<InputField>();
        nameText = transform.Find("TechNameText").GetComponent<Text>();
        progressSlider = transform.Find("TechSlider").GetComponent<Slider>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0 && WSUI_Controller.selectedTile.currentTech != null)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                techBonusField.interactable = true;
                progressSlider.interactable = true;
            }
            else
            {
                techBonusField.interactable = false;
                progressSlider.interactable = false;

                techBonusField.text = WSUI_Controller.selectedTile.techBonus.ToString();
                nameText.text = WSUI_Controller.selectedTile.currentTech.name;
                progressSlider.value = WSUI_Controller.selectedTile.storedTechPoints / WSUI_Controller.selectedTile.currentTech.cost;
            }
        }
    }

    public void changeTechBonus()
    {
        WSUI_Controller.selectedTile.techBonus = float.Parse(techBonusField.text);
    }

    public void changeTechProgress()
    {
        WSUI_Controller.selectedTile.storedTechPoints = progressSlider.value * WSUI_Controller.selectedTile.currentTech.cost;
    }

}
