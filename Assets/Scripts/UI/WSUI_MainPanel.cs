using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WSUI_MainPanel : MonoBehaviour
{
    private InputField farmersField = null;
    private InputField buildersField = null;
    private InputField tradersField = null;
    private InputField soldiersField = null;
    private InputField scholarsField = null;

    void Start()
    {
        farmersField = transform.Find("FarmersImputField").GetComponent<InputField>();
        buildersField = transform.Find("BuildersInputField").GetComponent<InputField>();
        tradersField = transform.Find("TradersInputField").GetComponent<InputField>();
        soldiersField = transform.Find("SoldiersInputField").GetComponent<InputField>();
        scholarsField = transform.Find("ScholarsInputField").GetComponent<InputField>();
    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                farmersField.interactable = true;
                buildersField.interactable = true;
                tradersField.interactable = true;
                soldiersField.interactable = true;
                scholarsField.interactable = true;
            }
            else
            {
                farmersField.interactable = false;
                buildersField.interactable = false;
                tradersField.interactable = false;
                soldiersField.interactable = false;
                scholarsField.interactable = false;

                farmersField.text = WSUI_Controller.selectedTile.farmers.ToString();
                buildersField.text = WSUI_Controller.selectedTile.builders.ToString();
                tradersField.text = WSUI_Controller.selectedTile.traders.ToString();
                soldiersField.text = WSUI_Controller.selectedTile.soldiers.ToString();
                scholarsField.text = WSUI_Controller.selectedTile.scholars.ToString();
            }
        }
    }

    public void changeFarmers()
    {
        WSUI_Controller.selectedTile.farmers = int.Parse(farmersField.text);
    }

    public void changeBuilders()
    {
        WSUI_Controller.selectedTile.builders = int.Parse(buildersField.text);
    }

    public void changeTraders()
    {
        WSUI_Controller.selectedTile.traders = int.Parse(tradersField.text);
    }

    public void changeSoldiers()
    {
        WSUI_Controller.selectedTile.soldiers = int.Parse(soldiersField.text);
    }

    public void changeScholars()
    {
        WSUI_Controller.selectedTile.scholars = int.Parse(scholarsField.text);
    }

}
