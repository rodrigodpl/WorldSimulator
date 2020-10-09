using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_DisasterPanel : MonoBehaviour
{
    private InputField disasterDescField = null;
    private Text nameText = null;

    void Start()
    {
        disasterDescField = transform.Find("DisasterInputField").GetComponent<InputField>();
        nameText = transform.Find("DisasterNameText").GetComponent<Text>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed != SimulationSpeed.PAUSED)
            {
                if (WSUI_Controller.selectedTile.disaster != null)
                {
                    disasterDescField.text = WSUI_Controller.selectedTile.disaster.description();
                    nameText.text = WSUI_Controller.selectedTile.disaster.name();
                }
                else
                    nameText.text = "No disasters";
            }
        }
    }

    public void removeDisasterBonus()
    {
        WSUI_Controller.selectedTile.disaster.Reverse(WSUI_Controller.selectedTile);
        WSUI_Controller.selectedTile.disaster = null;
        WSUI_Controller.selectedTile.disasterDuration = 0;
    }

}
