using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSUI_EntityInfoPanel : MonoBehaviour
{
    private Text capitalNameText = null;
    private Image coatImage = null;
    private Image typeImage = null;
    private Text nameText = null;

    public Sprite cultureSprite = null;
    public Sprite religionSprite = null;
    public Sprite governmentSprite = null;

    void Start()
    {
        nameText = transform.Find("EntityNameText").GetComponent<Text>();
        capitalNameText = transform.Find("CapitalNameText").GetComponent<Text>();
        coatImage = transform.Find("EntityImage").GetComponent<Image>();
        typeImage = transform.Find("EntityTypeImage").GetComponent<Image>();
    }

    void Update()
    {
        if (WSUI_Controller.selectedEntity != null)
        {
            capitalNameText.text = WSUI_Controller.selectedEntity.capital.name;
            nameText.text = WSUI_Controller.selectedEntity.name;
            coatImage.sprite = WSUI_Controller.selectedEntity.sprite;
            coatImage.color = WSUI_Controller.selectedEntity.color;

            switch(WSUI_Controller.selectedEntity.type)
            {
                case EntityType.CULTURE: typeImage.sprite = cultureSprite; break;
                case EntityType.RELIGION: typeImage.sprite = religionSprite; break;
                case EntityType.GOVERNMENT: typeImage.sprite = governmentSprite; break;
            }
        }

    }
}
