using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    // 이름
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    // 타입
    [SerializeField] private ItemType type;
    public ItemType Type { get { return type; } }

    // 아이콘
    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    // 제목
    [SerializeField] private string title;
    public string Title { get { return title; } }

    // 아이템 설명
    [SerializeField] private string desc;
    public string Desc { get { return desc; } }
}
