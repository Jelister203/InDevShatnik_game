using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            FindObjectOfType<InventoryUI>().AddItem(sprite, gameObject);
        }
    }
    public Sprite sprite;
}
