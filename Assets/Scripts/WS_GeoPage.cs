using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WS_GeoPage : MonoBehaviour
{
    private InputField altitudeField     = null;
    private InputField temperatureField  = null;
    private InputField humidityField     = null;
    private InputField habitabilityField = null;

    void Start()
    {
        altitudeField       = transform.GetChild(0).GetComponent<InputField>();
        temperatureField    = transform.GetChild(1).GetComponent<InputField>();
        humidityField       = transform.GetChild(2).GetComponent<InputField>();
        habitabilityField   = transform.GetChild(3).GetComponent<InputField>();

    }

    void Update()
    {
        if(WS_TilePanel.selectedTile != null)
        { 
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                altitudeField.interactable      = true;
                temperatureField.interactable   = true;
                humidityField.interactable      = true;
                habitabilityField.interactable  = true;
            }
            else
            {
                altitudeField.interactable      = false;
                temperatureField.interactable   = false;
                humidityField.interactable      = false;
                habitabilityField.interactable  = false;

                altitudeField.text      = WS_TilePanel.selectedTile.altitude.ToString();
                temperatureField.text   = WS_TilePanel.selectedTile.avgTemperature.ToString();
                humidityField.text      = WS_TilePanel.selectedTile.humidity.ToString();
                habitabilityField.text  = WS_TilePanel.selectedTile.habitability.ToString();
            }
        }
    }

    public void changeAltitude()
    {
        WS_TilePanel.selectedTile.altitude = int.Parse(altitudeField.text);
    }

    public void changeTemperature()
    {
        WS_TilePanel.selectedTile.avgTemperature = int.Parse(temperatureField.text);
    }

    public void changeHumidity()
    {
        WS_TilePanel.selectedTile.humidity = int.Parse(humidityField.text);
    }

    public void changeHabitability()
    {
        WS_TilePanel.selectedTile.habitability = int.Parse(habitabilityField.text);
    }
}
