using SaveLoad.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static diaolg;

public class pickUp : MonoBehaviour
{
    public int itemIndex;
    private GameObject button;
    private GameObject player;
    [SerializeField] private GameObject messagePanel;
    private int i = 0;
    public string[] messages;
    [SerializeField] private bool shouldBeDestried = true;


    private void Start()
    {
        button = transform.GetChild(0).gameObject;
        player = FindObjectOfType<PlayerMove>().gameObject;
        button.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        button.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            button.SetActive(true);
        }
    }


  
    private void Update()
    {
        if(itemIndex!=-9){if (player.GetComponent<playerInventory>().PlayerInventory[itemIndex]){
            Destroy(gameObject);
        }}
        if (button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (messagePanel!=null)
            {
                if (!messagePanel.activeSelf)
                {
                    if (i < messages.Length)
                        diaolg.SummonMessage(messagePanel, messages[i]);
                    i++;
                }
                else
                {
                    diaolg.killMessage(true, messagePanel);
                    if (i<messages.Length)
                    {
                        diaolg.SummonMessage(messagePanel, messages[i]);
                        i++;
                        }
                    else
                    {
                        i = 0;
                        if (shouldBeDestried){Destroy(gameObject);}
            if (itemIndex != -9){player.GetComponent<playerInventory>().PlayerInventory[itemIndex]=true;}
                    }
                }
            }
            else {if (shouldBeDestried){Destroy(gameObject);
            if (itemIndex != -9){player.GetComponent<playerInventory>().PlayerInventory[itemIndex]=true;}}}
        }
    }
}