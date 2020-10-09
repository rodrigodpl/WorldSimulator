using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_TerrainGenerator : MonoBehaviour
{
    Toggle lockPoles = null;
    List<Slider> slider = new List<Slider>();

    GameObject toolbar = null;

    public void Start()
    {
        toolbar = GameObject.Find("Toolbar");
        toolbar.SetActive(false);
    }

    public void Load()
    {
        WS_WorldGenerator.lockPoles = transform.GetChild(0).GetComponent<Toggle>().isOn;

        WS_WorldGenerator.shallowGenerators = (int)transform.GetChild(1).GetComponent<Slider>().value;
        WS_WorldGenerator.continentalGenerators = (int)transform.GetChild(2).GetComponent<Slider>().value;
        WS_WorldGenerator.alpineGenerators = (int)transform.GetChild(3).GetComponent<Slider>().value;

        WS_WorldGenerator.landmassPercentage = transform.GetChild(4).GetComponent<Slider>().value;
        WS_WorldGenerator.altitudeRandomizer = transform.GetChild(5).GetComponent<Slider>().value;

        WS_WorldGenerator.altitudeSmoothing = (int)transform.GetChild(6).GetComponent<Slider>().value;
        WS_WorldGenerator.temperatureSmoothing = (int)transform.GetChild(7).GetComponent<Slider>().value;
        WS_WorldGenerator.humiditySmoothing = (int)transform.GetChild(8).GetComponent<Slider>().value;
        WS_WorldGenerator.erosionSmoothing = (int)transform.GetChild(9).GetComponent<Slider>().value;

        WS_WorldGenerator.shallowRestriction = transform.GetChild(10).GetComponent<Slider>().value;
        WS_WorldGenerator.alpineRestriction = transform.GetChild(11).GetComponent<Slider>().value;

        WS_WorldGenerator.maxTemperatureMod = transform.GetChild(12).GetComponent<Slider>().value;
        WS_WorldGenerator.temperatureModNum = transform.GetChild(13).GetComponent<Slider>().value;

        WS_WorldGenerator.startingLandTemp = transform.GetChild(14).GetComponent<Slider>().value;
        WS_WorldGenerator.startingWaterTemp = transform.GetChild(15).GetComponent<Slider>().value;
        WS_WorldGenerator.latitudeLandTemp = transform.GetChild(16).GetComponent<Slider>().value;
        WS_WorldGenerator.latitudeWaterTemp = transform.GetChild(17).GetComponent<Slider>().value;
        WS_WorldGenerator.altitudeTempLoss = transform.GetChild(18).GetComponent<Slider>().value;

        WS_WorldGenerator.baseHumidity = transform.GetChild(19).GetComponent<Slider>().value;
        WS_WorldGenerator.altitudeHumLoss = transform.GetChild(20).GetComponent<Slider>().value;
        WS_WorldGenerator.temperatureHumLoss = transform.GetChild(21).GetComponent<Slider>().value;

        WS_WorldGenerator.altitudePressLoss = transform.GetChild(22).GetComponent<Slider>().value;
        WS_WorldGenerator.tempPressLoss = transform.GetChild(23).GetComponent<Slider>().value;
        WS_WorldGenerator.humidPressLoss = transform.GetChild(24).GetComponent<Slider>().value;
        WS_WorldGenerator.waterAltPressLoss = transform.GetChild(25).GetComponent<Slider>().value;

        WS_WorldGenerator.erosionMultiplier = transform.GetChild(26).GetComponent<Slider>().value;
        WS_WorldGenerator.erosionAltMult = transform.GetChild(27).GetComponent<Slider>().value;
        WS_WorldGenerator.erosionHumMult = transform.GetChild(28).GetComponent<Slider>().value;

        WS_WorldGenerator.maxRiverNum = transform.GetChild(29).GetComponent<Slider>().value;
        WS_WorldGenerator.minRiverNum = transform.GetChild(30).GetComponent<Slider>().value;
        WS_WorldGenerator.baseRiverImpulse = transform.GetChild(31).GetComponent<Slider>().value;
        WS_WorldGenerator.baseRiverStrength = transform.GetChild(32).GetComponent<Slider>().value;
        WS_WorldGenerator.riverHumEffect = transform.GetChild(33).GetComponent<Slider>().value;
        WS_WorldGenerator.riverAltEffect = transform.GetChild(34).GetComponent<Slider>().value;

        WS_WorldGenerator.minHabitability = transform.GetChild(35).GetComponent<Slider>().value;
        WS_WorldGenerator.baseHabitability = transform.GetChild(36).GetComponent<Slider>().value;
        WS_WorldGenerator.habAltMultiplier = transform.GetChild(37).GetComponent<Slider>().value;
        WS_WorldGenerator.habTempMultiplier = transform.GetChild(38).GetComponent<Slider>().value;
        WS_WorldGenerator.habHumMultiplier = transform.GetChild(39).GetComponent<Slider>().value;
        WS_WorldGenerator.habWaterMultiplier = transform.GetChild(40).GetComponent<Slider>().value;

        GameObject.Find("GameController").GetComponent<WS_World>().InitWorld();
        toolbar.SetActive(true);
        GameObject.Find("TerrainPanel").SetActive(false);
    }
}
