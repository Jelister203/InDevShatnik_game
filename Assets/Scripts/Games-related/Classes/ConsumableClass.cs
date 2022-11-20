using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public float healthRestored;
    
    public override ItemClass GetItem(){return this; }
    public override ToolClass GetTool(){return null; }
    public override MiscClass GetMisc(){return null; }
    public override ConsumableClass GetConsumable(){return this; }
}
