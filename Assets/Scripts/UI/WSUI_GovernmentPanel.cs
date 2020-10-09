using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_GovernmentPanel : MonoBehaviour
{
    private InputField unrestField = null;
    private InputField unrestCulField = null;
    private InputField unrestRelField = null;
    private Image coatImage = null;
    private Text nameText = null;

    void Start()
    {
        unrestField = transform.Find("UnrestInputField").GetComponent<InputField>();
        unrestCulField = transform.Find("UnrestCultureInputField").GetComponent<InputField>();
        unrestRelField = transform.Find("UnrestReligiousInputField").GetComponent<InputField>();
        nameText = transform.Find("GovernmentNameText").GetComponent<Text>();
        coatImage = transform.Find("GovernmentImage").GetComponent<Image>();
    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                unrestField.interactable = true;
                unrestCulField.interactable = true;
                unrestRelField.interactable = true;
            }
            else
            {
                unrestField.interactable = false;
                unrestCulField.interactable = false;
                unrestRelField.interactable = false;

                unrestField.text = WSUI_Controller.selectedTile.unrest.ToString();
                unrestCulField.text = WSUI_Controller.selectedTile.unrestCultural.ToString();
                unrestRelField.text = WSUI_Controller.selectedTile.unrestReligious.ToString();
                nameText.text = WSUI_Controller.selectedTile.government.name;
                coatImage.sprite = WSUI_Controller.selectedTile.government.sprite;
                coatImage.color = WSUI_Controller.selectedTile.government.color;
            }
        }

    }

    public void changeUnrest()
    {
        WSUI_Controller.selectedTile.unrest = float.Parse(unrestField.text);
    }

    public void changeUnrestCul()
    {
        WSUI_Controller.selectedTile.unrestCultural = float.Parse(unrestCulField.text);
    }

    public void changeUnrestRel()
    {
        WSUI_Controller.selectedTile.unrestReligious = float.Parse(unrestRelField.text);
    }

}
