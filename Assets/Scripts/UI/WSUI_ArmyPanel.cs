using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_ArmyPanel : MonoBehaviour
{
    private InputField armyBonusField = null;
    private InputField defenseField = null;

    void Start()
    {
        armyBonusField = transform.Find("ArmyBonusInputField").GetComponent<InputField>();
        defenseField = transform.Find("DefenseInputField").GetComponent<InputField>();
    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            armyBonusField.interactable = true;
            defenseField.interactable = true;
        }
        else
        {
            armyBonusField.interactable = false;
            defenseField.interactable = false;

            armyBonusField.text = WSUI_Controller.selectedTile.armyBonus.ToString();
            defenseField.text = WSUI_Controller.selectedTile.defenseBonus.ToString();
        }
    }

    public void changeArmyBonus()
    {
        WSUI_Controller.selectedTile.armyBonus = float.Parse(armyBonusField.text);
    }

    public void changeDefense()
    {
        WSUI_Controller.selectedTile.defenseBonus = float.Parse(defenseField.text);
    }

}
