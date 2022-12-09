using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject finalMessage;
    private void FixedUpdate() {
        for (int i = 0; i < gameObject.transform.childCount; i++){
            var boy = gameObject.transform.GetChild(i);
            if (boy.GetComponent<MessageTrigger>().oldOne = null){
                finalMessage = boy.gameObject;
            }
        }
    }
}
