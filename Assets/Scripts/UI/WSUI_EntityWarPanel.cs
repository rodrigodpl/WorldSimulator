using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_EntityWarPanel : MonoBehaviour
{
    private InputField soldierField = null;
    private InputField baseProfField = null;
    private InputField profField = null;
    private InputField commandField = null;
    private InputField warNumField = null;
    private InputField warscoreField = null;

    void Start()
    {
        soldierField = transform.Find("soldierPoolInputField").GetComponent<InputField>();
        baseProfField = transform.Find("baseProfessionalismInputField").GetComponent<InputField>();
        profField = transform.Find("professionalismInputField").GetComponent<InputField>();
        commandField = transform.Find("commandInputField").GetComponent<InputField>();
        warNumField = transform.Find("warNumInputField").GetComponent<InputField>();
        warscoreField = transform.Find("WarscoreInputField").GetComponent<InputField>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedEntity != null)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                soldierField.interactable = true;
                baseProfField.interactable = true;
                profField.interactable = true;
                commandField.interactable = true;
                warNumField.interactable = true;
                warscoreField.interactable = true;
            }
            else
            {
                soldierField.interactable = false;
                baseProfField.interactable = false;
                profField.interactable = false;
                commandField.interactable = false;
                warNumField.interactable = false;
                warscoreField.interactable = false;

                soldierField.text = ((WS_Government)WSUI_Controller.selectedEntity).soldierPool.ToString();
                baseProfField.text = ((WS_Government)WSUI_Controller.selectedEntity).baseProfessionalism.ToString();
                profField.text = ((WS_Government)WSUI_Controller.selectedEntity).armyProfessionalism.ToString();
                commandField.text = ((WS_Government)WSUI_Controller.selectedEntity).commandPower.ToString();
                warNumField.text = ((WS_Government)WSUI_Controller.selectedEntity).warNum.ToString();
                warscoreField.text = ((WS_Government)WSUI_Controller.selectedEntity).warScore.ToString();
            }
        }
    }

    public void changePopulation()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).soldierPool = int.Parse(soldierField.text);
    }

    public void changeFood()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).baseProfessionalism = float.Parse(baseProfField.text);
    }

    public void changeFoodEfficiency()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).armyProfessionalism = float.Parse(profField.text);
    }

    public void changeSanitation()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).commandPower = float.Parse(commandField.text);
    }

    public void changeHealthcare()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).warNum = int.Parse(warNumField.text);
    }

    public void changeGrowth()
    {
        ((WS_Government)WSUI_Controller.selectedEntity).warScore = float.Parse(warscoreField.text);
    }

}
