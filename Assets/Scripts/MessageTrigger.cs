using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTrigger : MonoBehaviour
{
    private bool triggered = false;
    public GameObject oldOne;
    public GameObject messagePanel;
    public string message;
    public bool isActive = false;
    private float timer;
    private void OnTriggerEnter2D(Collider2D other) {
        if (oldOne == null) {
            if (other.tag == "Player") {
                triggered = true;
                GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause = true;
                messagePanel.SetActive(true);
                messagePanel.transform.GetChild(0).GetComponent<Text>().text = message;
            }
        }
    }
    private void Update() {
        if (isActive == true) {
            if (oldOne != null)
                oldOne.GetComponent<MessageTrigger>().isActive = true;
            Destroy(gameObject);
        }
        if (GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause && triggered) {
            if (Input.GetKeyDown(KeyCode.E)) {
                GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause = false;
                messagePanel.SetActive(false);
                messagePanel.transform.GetChild(0).GetComponent<Text>().text = "";
                Destroy(gameObject);
            }
        }
    }
}
