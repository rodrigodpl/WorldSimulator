using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_GeographyPanel : MonoBehaviour
{
    private InputField altitudeField = null;
    private InputField temperatureField = null;
    private InputField humidityField = null;
    private InputField habitabilityField = null;
    private Toggle riverToggle = null;

    void Start()
    {
        altitudeField = transform.Find("AltitudeInputField").GetComponent<InputField>();
        temperatureField = transform.Find("TemperatureInputField").GetComponent<InputField>();
        humidityField = transform.Find("HumidityInputField").GetComponent<InputField>();
        habitabilityField = transform.Find("HabitabilityInputField").GetComponent<InputField>();
        riverToggle = transform.Find("RiverToggle").GetComponent<Toggle>();

    }

    void Update()
    {
        if (WS_World.speed == SimulationSpeed.PAUSED)
        {
            altitudeField.interactable = true;
            temperatureField.interactable = true;
            humidityField.interactable = true;
            habitabilityField.interactable = true;
            riverToggle.interactable = true;
        }
        else
        {
            altitudeField.interactable = false;
            temperatureField.interactable = false;
            humidityField.interactable = false;
            habitabilityField.interactable = false;
            riverToggle.interactable = false;

            altitudeField.text = WSUI_Controller.selectedTile.altitude.ToString();
            temperatureField.text = WSUI_Controller.selectedTile.avgTemperature.ToString();
            humidityField.text = WSUI_Controller.selectedTile.humidity.ToString();
            habitabilityField.text = WSUI_Controller.selectedTile.habitability.ToString();
            riverToggle.isOn = WSUI_Controller.selectedTile.riverStrength > 0.0f;
        }

    }

    public void changeAltitude()
    {
        WSUI_Controller.selectedTile.altitude = float.Parse(altitudeField.text);
    }

    public void changeTemperature()
    {
        WSUI_Controller.selectedTile.avgTemperature = float.Parse(temperatureField.text);
    }

    public void changeHumidity()
    {
        WSUI_Controller.selectedTile.humidity = float.Parse(humidityField.text);
    }

    public void changeHabitability()
    {
        WSUI_Controller.selectedTile.habitability = float.Parse(habitabilityField.text);
    }

    public void changeRiver()
    {
        if(riverToggle.isOn)
            WSUI_Controller.selectedTile.riverStrength = 1.0f;
        else
            WSUI_Controller.selectedTile.riverStrength = 0.0f;
    }
}
