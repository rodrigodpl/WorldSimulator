using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_CulturePanel : MonoBehaviour
{
    private InputField cultureBonusField = null;
    private Text nameText = null;
    private Image coatImage = null;

    void Start()
    {
        cultureBonusField = transform.Find("CultureBonusInputField").GetComponent<InputField>();
        nameText = transform.Find("CultureNameText").GetComponent<Text>();
        coatImage = transform.Find("CultureImage").GetComponent<Image>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                cultureBonusField.interactable = true;
            }
            else
            {
                cultureBonusField.interactable = false;

                cultureBonusField.text = WSUI_Controller.selectedTile.cultureBonus.ToString();
                nameText.text = WSUI_Controller.selectedTile.culture.name;
                coatImage.sprite = WSUI_Controller.selectedTile.culture.sprite;
                coatImage.color = WSUI_Controller.selectedTile.culture.color;
            }
        }
    }

    public void changeCulBonus()
    {
        WSUI_Controller.selectedTile.cultureBonus = float.Parse(cultureBonusField.text);
    }


}
