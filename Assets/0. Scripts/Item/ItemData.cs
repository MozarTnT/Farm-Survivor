using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    // �̸�
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    // Ÿ��
    [SerializeField] private ItemType type;
    public ItemType Type { get { return type; } }

    // ������
    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    // ����
    [SerializeField] private string title;
    public string Title { get { return title; } }

    // ������ ����
    [SerializeField] private string desc;
    public string Desc { get { return desc; } }



    [Header("# Level Data")]

    [SerializeField] private float[] damages;
    public float[] Damages { get { return damages; } }

    [SerializeField] private int[] counts;
    public int[] Counts { get { return Counts; } }
}
