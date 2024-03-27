using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public ItemData data;
    public int level;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.Icon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch(data.Type)
        {
            case ItemType.Bible:
                break;
            case ItemType.Bullet_Spd:
                break;
            case ItemType.Bullet_Att:
                break;
            case ItemType.Magnet:
                break;
            case ItemType.Boots:
                break;
            case ItemType.Heal:
                break;
        }
        level++;

        /*if(level == data.Damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }*/
    }
}
