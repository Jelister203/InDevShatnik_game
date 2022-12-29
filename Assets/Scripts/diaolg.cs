using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;


public class diaolg : MonoBehaviour
{
    public static void SummonMessage(GameObject messagePanel, string message)
    {
        GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause = true;
        messagePanel.SetActive(true);
        messagePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;

    }
    public static void killMessage(bool triggered, GameObject messagePanel)
    {
        if (GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause && triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindObjectOfType<Pause>().GetComponent<Pause>().ispause = false;
                messagePanel.SetActive(false);
                messagePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
            }
        }

    }
}
