using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Slot[] Inventory;
    public bool[] inventory;
    private void Awake()
    {
        Slot[] go = GameObject.FindObjectsOfType<Slot>();
        Inventory = go;
        inventory = new bool[go.Length];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    public void AddItem (Sprite icon, GameObject gameObject)
    {
        int num = Array.IndexOf(inventory, false);
        if (num != -1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        Slot slot = Inventory[num];
        foreach (Transform child in transform)
        {
            GameObject Button = child.GetChild(num).GetChild(0).gameObject;
            Button.transform.GetChild(0).GetComponent<Image>().sprite = icon;
            Button.transform.GetChild(0).GetComponent<Image>().enabled = true;
            inventory[num] = true;
        }
    }
}
