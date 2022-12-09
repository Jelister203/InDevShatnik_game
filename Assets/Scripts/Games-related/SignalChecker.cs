using System.Collections.Generic;
using UnityEngine;
public class SignalChecker : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    
    [SerializeField] private GameObject objectToDisable;
    private SlotClass[] items;
    [System.Serializable] public class ACList {
    public ItemClass itemsToAddAC;
    public int item_id;
    public bool isOnState;
    public bool rotation;
    public string Name;
    }
    [System.Serializable] public class SubList {
    public int x;
    public int y;
}


[SerializeField] private ItemClass[] itemsToAdd;
public List<SubList> indexes = new List<SubList>();
public List<ACList> ACs = new List<ACList>();
    public void Updater()
    {
        if (inventory.GetComponent<InventoryManager>().isWorking){
            objectToDisable.GetComponent<SpriteRenderer>().enabled = false;
            objectToDisable.GetComponent<BoxCollider2D>().enabled = false;
        }
        else{
        objectToDisable.GetComponent<SpriteRenderer>().enabled = true;
        objectToDisable.GetComponent<BoxCollider2D>().enabled = true;
        }
        for (int i = 0; i < ACs.Count; i++){
            if (items[ACs[i].item_id].GetItem() != null){
                if (items[ACs[i].item_id].GetItem().itemName == ACs[i].Name && items[ACs[i].item_id].GetItem().isRotated == ACs[i].rotation){
                    ACs[i].isOnState = true;
                }
                else{
                    ACs[i].isOnState = false;
                }
            }
            else{
                ACs[i].isOnState = false;
            }
        }
        if (isEverythingFine()){
            inventory.GetComponent<InventoryManager>().isWorking = true;
        }
        else
            inventory.GetComponent<InventoryManager>().isWorking = false;
    }
    bool isEverythingFine(){
        for (int i = 0; i < ACs.Count; i++){
            bool boob = ACs[i].isOnState;
            if (boob == false)
                return false;
        }
        return true;
    }

    public void Starter() {
        items = inventory.GetComponent<InventoryManager>().items;
        if (itemsToAdd.Length == indexes.Count){
        for (int i = 0; i < itemsToAdd.Length; i++){
            inventory.GetComponent<InventoryManager>().Add(itemsToAdd[i], (indexes[i].x+7*indexes[i].y)-1);
        }}
        else{Debug.Log(itemsToAdd.Length.ToString()+" != "+indexes.Count);}

        for (int i = 0; i < ACs.Count; i++){
            ItemClass item = ACs[i].itemsToAddAC;
            inventory.GetComponent<InventoryManager>().Add(item);
        }
    }
}