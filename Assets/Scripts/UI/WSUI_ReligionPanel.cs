using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_ReligionPanel : MonoBehaviour
{
    private InputField religionBonusField = null;
    private Text nameText = null;
    private Image coatImage = null;

    void Start()
    {
        religionBonusField = transform.Find("ReligousBonusInputField").GetComponent<InputField>();
        nameText = transform.Find("ReligionNameText").GetComponent<Text>();
        coatImage = transform.Find("ReligionImage").GetComponent<Image>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                religionBonusField.interactable = true;
            }
            else
            {
                religionBonusField.interactable = false;

                religionBonusField.text = WSUI_Controller.selectedTile.religionBonus.ToString();
                nameText.text = WSUI_Controller.selectedTile.religion.name;
                coatImage.sprite = WSUI_Controller.selectedTile.religion.sprite;
                coatImage.color = WSUI_Controller.selectedTile.religion.color;
            }
        }
    }

    public void changeRelBonus()
    {
        WSUI_Controller.selectedTile.religionBonus = float.Parse(religionBonusField.text);
    }

}
