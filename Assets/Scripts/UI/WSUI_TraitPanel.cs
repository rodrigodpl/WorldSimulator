using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_TraitPanel : MonoBehaviour
{
    List<Text> traitNames = new List<Text>();
    List<Text> traitDescs = new List<Text>(); 

    void Start()
    {
        traitNames.Add(transform.GetChild(0).GetComponent<Text>());
        traitNames.Add(transform.GetChild(1).GetComponent<Text>());
        traitNames.Add(transform.GetChild(2).GetComponent<Text>());
        traitNames.Add(transform.GetChild(3).GetComponent<Text>());

        traitDescs.Add(transform.GetChild(4).GetComponent<Text>());
        traitDescs.Add(transform.GetChild(5).GetComponent<Text>());
        traitDescs.Add(transform.GetChild(6).GetComponent<Text>());
        traitDescs.Add(transform.GetChild(7).GetComponent<Text>());
    }

    void Update()
    {
        if (WSUI_Controller.selectedEntity != null)
        {
            for(int i = 0; i < 4; i++)
            {
                if(i < WSUI_Controller.selectedEntity.traits.Count)
                {
                    traitNames[i].text = WSUI_Controller.selectedEntity.traits[i].traitName();
                    traitDescs[i].text = WSUI_Controller.selectedEntity.traits[i].traitDesc();
                }
                else
                {
                    traitNames[i].text = "";
                    traitDescs[i].text = "";
                }
            }
        }
    }

}
