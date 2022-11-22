using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    private Slot[] InventorySlots;
    private bool[] InventoryIsEmpty;
    private int[] InventoryItemStats;
    private void Awake()
    {
        Slot[] go = GameObject.FindObjectsOfType<Slot>();
        InventorySlots = go;
        InventoryIsEmpty = new bool[go.Length];
        InventoryItemStats = new int[go.Length];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    public void AddItem (Sprite icon, GameObject gameObject, int[] param)
    {
        int num = Array.IndexOf(InventoryIsEmpty, false);
        if (num != -1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        //Slot slot = InventorySlots[num];
        foreach (Transform child in transform)
        {   /*
            GameObject Button = child.GetChild(num).GetChild(0).gameObject;
            Button.transform.GetChild(0).GetComponent<Image>().sprite = icon;
            Button.transform.GetChild(0).GetComponent<Image>().enabled = true;
            inventory[num] = true;
            */
            GameObject slot = child.GetChild(num).GetChild(1).gameObject;
            slot.GetComponent<Image>().sprite = icon;
            slot.GetComponent<Image>().enabled = true;
            InventoryIsEmpty[num] = true;
            //InventoryItemStats[num] = param;
        }
    }
}
