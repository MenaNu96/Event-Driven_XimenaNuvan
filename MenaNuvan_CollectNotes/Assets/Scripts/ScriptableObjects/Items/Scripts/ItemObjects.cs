using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItempType
{
    Notes
}
public abstract class ItemObjects : ScriptableObject
{
    public GameObject prefab;
    public ItempType type;
    [TextArea(15,20)]
    public string description;
}

