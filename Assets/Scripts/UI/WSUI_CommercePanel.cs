using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_CommercePanel : MonoBehaviour
{
    private InputField resFoodField = null;
    private InputField resWarField = null;
    private InputField resConstructionField = null;
    private InputField resUnrestField = null;
    private InputField resProsperityField = null;

    private Toggle resIronToggle = null;
    private Toggle resCopperToggle = null;
    private Toggle resTinToggle = null;
    private Toggle resSilverToggle = null;
    private Toggle resGoldToggle = null;
    private Toggle resCoalToggle = null;

    private Toggle resPasturesToggle = null;
    private Toggle resFishToggle = null;
    private Toggle resSpicesToggle = null;
    private Toggle resHuntToggle = null;
    private Toggle resFursToggle = null;
    private Toggle resOpioidsToggle = null;

    private Toggle resGraniteToggle = null;
    private Toggle resClayToggle = null;
    private Toggle resMarbleToggle = null;
    private Toggle resJadeToggle = null;
    private Toggle resSaltToggle = null;
    private Toggle resWoodToggle = null;

    void Start()
    {
        resFoodField = transform.Find("ResFoodInputField").GetComponent<InputField>();
        resWarField = transform.Find("ResWarInputField").GetComponent<InputField>();
        resConstructionField = transform.Find("ResConstructionInputField").GetComponent<InputField>();
        resUnrestField = transform.Find("ResUnrestInputField").GetComponent<InputField>();
        resProsperityField = transform.Find("ResProsperityInputField").GetComponent<InputField>();

        resIronToggle = transform.Find("ResIronToggle").GetComponent<Toggle>();
        resCopperToggle = transform.Find("ResCopperToggle").GetComponent<Toggle>();
        resTinToggle = transform.Find("ResTinToggle").GetComponent<Toggle>();
        resSilverToggle = transform.Find("ResSilverToggle").GetComponent<Toggle>();
        resGoldToggle = transform.Find("ResGoldToggle").GetComponent<Toggle>();
        resCoalToggle = transform.Find("ResCoalToggle").GetComponent<Toggle>();

        resPasturesToggle = transform.Find("ResPasturesToggle").GetComponent<Toggle>();
        resFishToggle = transform.Find("ResFishToggle").GetComponent<Toggle>();
        resSpicesToggle = transform.Find("ResSpicesToggle").GetComponent<Toggle>();
        resHuntToggle = transform.Find("ResHuntToggle").GetComponent<Toggle>();
        resFursToggle = transform.Find("ResFursToggle").GetComponent<Toggle>();
        resOpioidsToggle = transform.Find("ResOpioidsToggle").GetComponent<Toggle>();

        resGraniteToggle = transform.Find("ResGraniteToggle").GetComponent<Toggle>();
        resClayToggle = transform.Find("ResClayToggle").GetComponent<Toggle>();
        resMarbleToggle = transform.Find("ResMarbleToggle").GetComponent<Toggle>();
        resJadeToggle = transform.Find("ResJadeToggle").GetComponent<Toggle>();
        resSaltToggle = transform.Find("ResSaltToggle").GetComponent<Toggle>();
        resWoodToggle = transform.Find("ResWoodToggle").GetComponent<Toggle>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedTile.population > 0)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                resFoodField.interactable = true;
                resWarField.interactable = true;
                resConstructionField.interactable = true;
                resUnrestField.interactable = true;
                resProsperityField.interactable = true;

                resIronToggle.interactable = true;
                resCopperToggle.interactable = true;
                resTinToggle.interactable = true;
                resSilverToggle.interactable = true;
                resGoldToggle.interactable = true;
                resCoalToggle.interactable = true;

                resPasturesToggle.interactable = true;
                resFishToggle.interactable = true;
                resSpicesToggle.interactable = true;
                resHuntToggle.interactable = true;
                resFursToggle.interactable = true;
                resOpioidsToggle.interactable = true;

                resGraniteToggle.interactable = true;
                resClayToggle.interactable = true;
                resMarbleToggle.interactable = true;
                resJadeToggle.interactable = true;
                resSaltToggle.interactable = true;
                resWoodToggle.interactable = true;
            }
            else
            {
                resFoodField.interactable = false;
                resWarField.interactable = false;
                resConstructionField.interactable = false;
                resUnrestField.interactable = false;
                resProsperityField.interactable = false;

                resIronToggle.interactable = false;
                resCopperToggle.interactable = false;
                resTinToggle.interactable = false;
                resSilverToggle.interactable = false;
                resGoldToggle.interactable = false;
                resCoalToggle.interactable = false;

                resPasturesToggle.interactable = false;
                resFishToggle.interactable = false;
                resSpicesToggle.interactable = false;
                resHuntToggle.interactable = false;
                resFursToggle.interactable = false;
                resOpioidsToggle.interactable = false;

                resGraniteToggle.interactable = false;
                resClayToggle.interactable = false;
                resMarbleToggle.interactable = false;
                resJadeToggle.interactable = false;
                resSaltToggle.interactable = false;
                resWoodToggle.interactable = false;

                resFoodField.text = WSUI_Controller.selectedTile.resFoodBonus.ToString();
                resWarField.text = WSUI_Controller.selectedTile.resWarBonus.ToString();
                resConstructionField.text = WSUI_Controller.selectedTile.resConstructionBonus.ToString();
                resUnrestField.text = WSUI_Controller.selectedTile.resUnrestBonus.ToString();
                resProsperityField.text = WSUI_Controller.selectedTile.resProsperityBonus.ToString();

                resIronToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.IRON].amount > 0;
                resCopperToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COPPER].amount > 0;
                resTinToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.TIN].amount > 0;
                resSilverToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SILVER].amount > 0;
                resGoldToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GOLD].amount > 0;
                resCoalToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COAL].amount > 0;

                resPasturesToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.PASTURES].amount > 0;
                resFishToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FISH].amount > 0;
                resSpicesToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SPICES].amount > 0;
                resHuntToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.HUNT].amount > 0;
                resFursToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FURS].amount > 0;
                resOpioidsToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.OPIOIDS].amount > 0;

                resGraniteToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GRANITE].amount > 0;
                resClayToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.CLAY].amount > 0;
                resMarbleToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.MARBLE].amount > 0;
                resJadeToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.JADE].amount > 0;
                resSaltToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SALT].amount > 0;
                resWoodToggle.isOn = WSUI_Controller.selectedTile.resStacks[(int)ResourceType.WOOD].amount > 0;

            }
        }
    }

    public void changeResFoodBonus()
    {
        WSUI_Controller.selectedTile.resFoodBonus = float.Parse(resFoodField.text);
    }

    public void changeResWarBonus()
    {
        WSUI_Controller.selectedTile.resWarBonus = float.Parse(resWarField.text);
    }

    public void changeResConstructionBonus()
    {
        WSUI_Controller.selectedTile.resConstructionBonus = float.Parse(resConstructionField.text);
    }

    public void changeResUnrestBonus()
    {
        WSUI_Controller.selectedTile.resUnrestBonus = float.Parse(resUnrestField.text);
    }

    public void changeResProsperityBonus()
    {
        WSUI_Controller.selectedTile.resProsperityBonus = float.Parse(resProsperityField.text);
    }



    public void changeResIronBonus()
    {
        if (resIronToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.IRON].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else                    WSUI_Controller.selectedTile.resStacks[(int)ResourceType.IRON].amount = 0;
    }

    public void changeResCopperBonus()
    {
        if (resCopperToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COPPER].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COPPER].amount = 0;
    }

    public void changeResTinBonus()
    {
        if (resTinToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.TIN].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.TIN].amount = 0;
    }

    public void changeResSilverBonus()
    {
        if (resSilverToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SILVER].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SILVER].amount = 0;
    }

    public void changeResGoldBonus()
    {
        if (resGoldToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GOLD].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GOLD].amount = 0;
    }

    public void changeResCoalBonus()
    {
        if (resCoalToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COAL].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.COAL].amount = 0;
    }



    public void changeResPasturesBonus()
    {
        if (resPasturesToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.PASTURES].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.PASTURES].amount = 0;
    }

    public void changeResFishBonus()
    {
        if (resFishToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FISH].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FISH].amount = 0;
    }

    public void changeResSpicesBonus()
    {
        if (resSpicesToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SPICES].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SPICES].amount = 0;
    }

    public void changeResHuntBonus()
    {
        if (resHuntToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.HUNT].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.HUNT].amount = 0;
    }

    public void changeResFurBonus()
    {
        if (resFursToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FURS].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.FURS].amount = 0;
    }

    public void changeResOpioidsBonus()
    {
        if (resOpioidsToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.OPIOIDS].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.OPIOIDS].amount = 0;
    }



    public void changeResGraniteBonus()
    {
        if (resGraniteToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GRANITE].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.GRANITE].amount = 0;
    }

    public void changeResClayBonus()
    {
        if (resClayToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.CLAY].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.CLAY].amount = 0;
    }

    public void changeResMarbleBonus()
    {
        if (resMarbleToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.MARBLE].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.MARBLE].amount = 0;
    }

    public void changeResJadeBonus()
    {
        if (resJadeToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.JADE].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.JADE].amount = 0;
    }

    public void changeResSaltBonus()
    {
        if (resSaltToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SALT].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.SALT].amount = 0;
    }

    public void changeResWoodBonus()
    {
        if (resWoodToggle.isOn) WSUI_Controller.selectedTile.resStacks[(int)ResourceType.WOOD].amount = (int)(WSUI_Controller.selectedTile.population * 0.1f);
        else WSUI_Controller.selectedTile.resStacks[(int)ResourceType.WOOD].amount = 0;
    }
}
