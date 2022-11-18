using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Damage;
    public int AttackSpeed;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            int[] param = {Damage, AttackSpeed};
            FindObjectOfType<InventoryUI>().AddItem(sprite, gameObject, param);
        }
    }
    public Sprite sprite;
}
