using System.Collections;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public Sprite vertical;
    public Sprite horizontal;
    public Sprite curent;
    public bool Anti = false;
    public bool isRotated = false;

    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
}
