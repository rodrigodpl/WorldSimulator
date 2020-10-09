using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_Controller : MonoBehaviour
{
    [HideInInspector] public WS_World world = null;
    [HideInInspector] public GameObject tilePanel = null;
    [HideInInspector] public GameObject entityPanel = null;

    [HideInInspector] public static WS_Tile selectedTile = null;
    [HideInInspector] public static WS_Entity selectedEntity = null;
    [HideInInspector] public Text townName = null;
    [HideInInspector] public Image biomeImage = null;

    public Sprite iconPolar = null;
    public Sprite iconTundra = null;
    public Sprite iconBorealForest = null;

    public Sprite iconAlpine = null;
    public Sprite iconAlpineShrubland = null;
    public Sprite iconAlpineForest = null;

    public Sprite iconTemperateGrassland = null;
    public Sprite iconTemperateForest = null;
    public Sprite iconTemperateShrubland = null;
    public Sprite iconWetlands = null;

    public Sprite iconTropicalJungle = null;
    public Sprite iconTropicalGrassland = null;
    public Sprite iconSavannah = null;

    public Sprite iconTemperateDesert = null;
    public Sprite iconAridDesert = null;

    public Sprite iconWater = null;

    void Start()
    {
        world = GameObject.Find("GameController").GetComponent<WS_World>();
        tilePanel = transform.Find("TilePanel").gameObject;
        entityPanel = GameObject.Find("EntityPanel").gameObject;

        townName = tilePanel.transform.Find("TownNameText").GetComponent<Text>();
        biomeImage = tilePanel.transform.Find("BiomeImage").GetComponent<Image>();
        tilePanel.SetActive(false);
        entityPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!tilePanel.activeSelf)
        {
            if (Input.GetMouseButton(0) && world.output != null)
            {
                float mouseRatioX = Input.mousePosition.x / Screen.width;
                float mouseRatioY = Input.mousePosition.y / Screen.height;
                Vector2 tilePos = new Vector2((mouseRatioX * world.output.width) / (world.hexTex.width * 0.84f), (mouseRatioY * world.output.height) / (world.hexTex.height * 0.75f));

                selectedTile = world.GetTile(new Vector2Int(Mathf.FloorToInt(tilePos.x), Mathf.FloorToInt(tilePos.y)));

                if (selectedTile != null)
                {
                    if (!selectedTile.seaBody)
                    {
                        tilePanel.SetActive(true);

                        switch (selectedTile.biome)
                        {
                            case Biome.POLAR: biomeImage.sprite = iconPolar; break;
                            case Biome.TUNDRA: biomeImage.sprite = iconTundra; break;
                            case Biome.BOREAL_FOREST: biomeImage.sprite = iconBorealForest; break;
                            case Biome.ALPINE: biomeImage.sprite = iconAlpine; break;
                            case Biome.ALPINE_SHRUBLAND: biomeImage.sprite = iconAlpineShrubland; break;
                            case Biome.ALPINE_FOREST: biomeImage.sprite = iconAlpineForest; break;
                            case Biome.TEMPERATE_SHRUBLAND: biomeImage.sprite = iconTemperateShrubland; break;
                            case Biome.TEMPERATE_GRASSLAND: biomeImage.sprite = iconTemperateGrassland; break;
                            case Biome.TEMPERATE_FOREST: biomeImage.sprite = iconTemperateForest; break;
                            case Biome.WETLANDS: biomeImage.sprite = iconWetlands; break;
                            case Biome.SAVANNAH: biomeImage.sprite = iconSavannah; break;
                            case Biome.TEMPERATE_DESERT: biomeImage.sprite = iconTemperateDesert; break;
                            case Biome.TROPICAL_GRASSLAND: biomeImage.sprite = iconTropicalGrassland; break;
                            case Biome.TROPICAL_JUNGLE: biomeImage.sprite = iconTropicalJungle; break;
                            case Biome.ARID_DESERT: biomeImage.sprite = iconAridDesert; break;
                            case Biome.WATER: biomeImage.sprite = iconWater; break;
                        }
                        return;
                    }
                }

                tilePanel.SetActive(false);
            }
        }
        else
            townName.text = selectedTile.name;
    }

    public void Pause()
    {
        GameObject.Find("TimePanel").GetComponent<WS_TimeController>().SetSpeed((int)SimulationSpeed.PAUSED);
    }

    public void CloseTilePanel()
    {
        tilePanel.SetActive(false);
        selectedEntity = null;
        entityPanel.SetActive(false);
    }

    public void selectCulture()
    {
        if (selectedTile != null)
        {
            if (selectedTile.culture != null)
            {
                selectedEntity = selectedTile.culture;
                entityPanel.SetActive(true);
                entityPanel.transform.Find("ArmyPanel").gameObject.SetActive(false);
            }
        }
    }

    public void selectReligion()
    {
        if (selectedTile != null)
        {
            if (selectedTile.religion != null)
            {
                selectedEntity = selectedTile.religion;
                entityPanel.SetActive(true);
                entityPanel.transform.Find("ArmyPanel").gameObject.SetActive(false);
            }
        }
    }


    public void selectGovernment()
    {
        if (selectedTile != null)
        {
            if (selectedTile.government != null)
            {
                selectedEntity = selectedTile.government;
                entityPanel.SetActive(true);
                entityPanel.transform.Find("ArmyPanel").gameObject.SetActive(true);
            }
        }
    }

}
