using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemDataSO : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public uint price;
}
