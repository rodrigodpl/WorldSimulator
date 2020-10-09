using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_EntityVarPanel : MonoBehaviour
{
    private Text text1 = null;
    private Text text2 = null;
    private Text text3 = null;
    private Text text4 = null;
    private Text text5 = null;
    private Text text6 = null;

    private InputField field1 = null;
    private InputField field2 = null;
    private InputField field3 = null;
    private InputField field4 = null;
    private InputField field5 = null;
    private InputField field6 = null;

    void Start()
    {

        text1 = transform.Find("Text1").GetComponent<Text>();
        text2 = transform.Find("Text2").GetComponent<Text>();
        text3 = transform.Find("Text3").GetComponent<Text>();
        text4 = transform.Find("Text4").GetComponent<Text>();
        text5 = transform.Find("Text5").GetComponent<Text>();
        text6 = transform.Find("Text6").GetComponent<Text>();

        field1 = transform.Find("InputField1").GetComponent<InputField>();
        field2 = transform.Find("InputField2").GetComponent<InputField>();
        field3 = transform.Find("InputField3").GetComponent<InputField>();
        field4= transform.Find("InputField4").GetComponent<InputField>();
        field5 = transform.Find("InputField5").GetComponent<InputField>();
        field6 = transform.Find("InputField6").GetComponent<InputField>();

    }

    void Update()
    {
        if (WSUI_Controller.selectedEntity != null)
        {
            if (WS_World.speed == SimulationSpeed.PAUSED)
            {
                field1.interactable = true;
                field2.interactable = true;
                field3.interactable = true;
                field4.interactable = true;
                field5.interactable = true;
                field6.interactable = true;
            }
            else
            {
                field1.interactable = false;
                field2.interactable = false;
                field3.interactable = false;
                field4.interactable = false;
                field5.interactable = false;
                field6.interactable = false;
            }

            switch (WSUI_Controller.selectedEntity.type)
            {
                case EntityType.CULTURE:

                    field1.text = ((WS_Culture)WSUI_Controller.selectedEntity).FoodEfficiency.ToString();
                    field2.text = ((WS_Culture)WSUI_Controller.selectedEntity).expansionism.ToString();
                    field3.text = ((WS_Culture)WSUI_Controller.selectedEntity).healthcare.ToString();
                    field4.text = ((WS_Culture)WSUI_Controller.selectedEntity).influenceBonus.ToString();
                    field5.text = ((WS_Culture)WSUI_Controller.selectedEntity).influenceMul.ToString();
                    field6.text = ((WS_Culture)WSUI_Controller.selectedEntity).syncretism.ToString();


                    text1.text = "FoodEfficiency";
                    text2.text = "Expansionism";
                    text3.text = "Healthcare";
                    text4.text = "InfluenceBonus";
                    text5.text = "InfluenceMul";
                    text6.text = "Syncretism";

                    break;

                case EntityType.RELIGION:

                    field1.text = ((WS_Religion)WSUI_Controller.selectedEntity).corruption.ToString();
                    field2.text = ((WS_Religion)WSUI_Controller.selectedEntity).power.ToString();
                    field3.text = "";
                    field4.text = ((WS_Religion)WSUI_Controller.selectedEntity).influenceBonus.ToString();
                    field5.text = ((WS_Religion)WSUI_Controller.selectedEntity).influenceMul.ToString();
                    field6.text = ((WS_Religion)WSUI_Controller.selectedEntity).syncretism.ToString();


                    text1.text = "Corruption";
                    text2.text = "Power";
                    text3.text = "";
                    text4.text = "InfluenceBonus";
                    text5.text = "InfluenceMul";
                    text6.text = "Syncretism";

                    break;

                case EntityType.GOVERNMENT:

                    field1.text = ((WS_Government)WSUI_Controller.selectedEntity).unrest.ToString();
                    field2.text = ((WS_Government)WSUI_Controller.selectedEntity).unrestCultural.ToString();
                    field3.text = ((WS_Government)WSUI_Controller.selectedEntity).unrestReligious.ToString();
                    field4.text = ((WS_Government)WSUI_Controller.selectedEntity).unrestMul.ToString();
                    field5.text = ((WS_Government)WSUI_Controller.selectedEntity).baseRepression.ToString();
                    field6.text = ((WS_Government)WSUI_Controller.selectedEntity).repression.ToString();


                    text1.text = "Unrest";
                    text2.text = "Unrest Cul.";
                    text3.text = "Unrest Rel.";
                    text4.text = "Unrest Mul.";
                    text5.text = "Base Repression";
                    text6.text = "Repression";

                    break;
            }

               
        }
    }

    public void changeField1()
    {
        switch(WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).FoodEfficiency = float.Parse(field1.text); break;
            case EntityType.RELIGION: ((WS_Religion)WSUI_Controller.selectedEntity).corruption = float.Parse(field1.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).unrest = float.Parse(field1.text); break;
        }
    }

    public void changeField2()
    {
        switch (WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).expansionism = float.Parse(field2.text); break;
            case EntityType.RELIGION: ((WS_Religion)WSUI_Controller.selectedEntity).power = float.Parse(field2.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).unrestCultural = float.Parse(field2.text); break;
        }
    }

    public void changeField3()
    {
        switch (WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).healthcare = float.Parse(field3.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).unrestReligious = float.Parse(field3.text); break;
        }
    }

    public void changeField4()
    {
        switch (WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).influenceBonus = float.Parse(field4.text); break;
            case EntityType.RELIGION: ((WS_Religion)WSUI_Controller.selectedEntity).influenceBonus = float.Parse(field4.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).unrestMul = float.Parse(field4.text); break;
        }
    }

    public void changeField5()
    {
        switch (WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).influenceMul = float.Parse(field5.text); break;
            case EntityType.RELIGION: ((WS_Religion)WSUI_Controller.selectedEntity).influenceMul = float.Parse(field5.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).baseRepression = float.Parse(field5.text); break;
        }
    }

    public void changeField6()
    {
        switch (WSUI_Controller.selectedEntity.type)
        {
            case EntityType.CULTURE: ((WS_Culture)WSUI_Controller.selectedEntity).syncretism = float.Parse(field6.text); break;
            case EntityType.RELIGION: ((WS_Religion)WSUI_Controller.selectedEntity).syncretism = float.Parse(field6.text); break;
            case EntityType.GOVERNMENT: ((WS_Government)WSUI_Controller.selectedEntity).repression = float.Parse(field6.text); break;
        }
    }

}
