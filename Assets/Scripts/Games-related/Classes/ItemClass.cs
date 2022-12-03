using System.Collections;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;
    public Sprite itemIcon2;
    public bool Anti = false;
    public bool isRotated = false;

    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
}
