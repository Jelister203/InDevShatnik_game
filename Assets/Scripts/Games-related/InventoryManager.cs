using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject movingItemCursor;
    [SerializeField] private GameObject slotHolder;
    public SlotClass[] items;

    private GameObject[] slots;
    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;
    private bool isMovingItem;
    public bool isWorking = false;
    public GameObject Game;

    private void Start() {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            items[i] = new SlotClass();
        }
        for (int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        
        Game.GetComponent<SignalChecker>().Starter();
        RefreshUI();
    }
    private void Update() {
        Game.GetComponent<SignalChecker>().Updater();
        movingItemCursor.SetActive(isMovingItem);
        movingItemCursor.transform.position = Input.mousePosition;
        if (isMovingItem){
        movingItemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }
        if (Input.GetMouseButtonDown(0)){
            if(isMovingItem)
                EndItemMove();
            else
                BeginItemMove();
        }
        if (Input.GetMouseButtonDown(1)){
            if(isMovingItem){}
            else{
                try {
                originalSlot = GetClosestSlot();
                Sprite temp = originalSlot.GetItem().itemIcon;
                originalSlot.GetItem().itemIcon = originalSlot.GetItem().itemIcon2;
                originalSlot.GetItem().itemIcon2 = temp;
                originalSlot.GetItem().isRotated = !originalSlot.GetItem().isRotated;
                RefreshUI();
                }
                catch{}
            }
        }
    }
    #region Invenotry Utils
    public void RefreshUI(){
        for (int i = 0; i < slots.Length; i++){
            try {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }
    public bool Add(ItemClass item){
        SlotClass slot = Contains(item);
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    items[i] = new SlotClass(item);
                    break;
                }
            }
        
        RefreshUI();
        return true;
    }
    public bool Add(ItemClass item, int i){
        if (items[i].GetItem() == null){
            items[i] = new SlotClass(item);
        }
        RefreshUI();
        return true;
    }
    public bool Remove(ItemClass item){
        SlotClass temp = Contains(item);
        if (temp != null){
                int slotToRemoveIndex = 0;
                for (int i = 0; i < items.Length; i++){
                    if (items[i].GetItem() == item){
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                items[slotToRemoveIndex].Clear();
            
        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }
    public SlotClass Contains(ItemClass item){
        for ( int i = 0; i < items.Length; i++)
        {
             if (items[i].GetItem() == item)
                return items[i];
        }
        return null;
    }
    #endregion Inventory Utils

    #region Moving Stuff
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false;
        
        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem());
            movingSlot.Clear();
        }
        else{
        if (originalSlot.GetItem() != null)
        {
            if (!originalSlot.GetItem().Anti)
            {
            tempSlot = new SlotClass(originalSlot);
            originalSlot.AddItem(movingSlot.GetItem());
            movingSlot.AddItem(tempSlot.GetItem());
            RefreshUI();
            return true;
            }
            else
                return false;
        }
        
        else
        {
            originalSlot.AddItem(movingSlot.GetItem());
            movingSlot.Clear();
        }
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }

    private SlotClass GetClosestSlot(){
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
            {
                if (items[i].GetItem() == null)
                    return items[i];
                else if (!items[i].GetItem().Anti)
                    return items[i];
            }
        }
        return null;
    }
    #endregion Moving Stuff
}
