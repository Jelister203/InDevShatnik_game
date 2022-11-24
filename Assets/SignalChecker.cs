using UnityEngine;

public class SignalChecker : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    void Update()
    {
        if (inventory.GetComponent<InventoryManager>().isWorking){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else{
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
