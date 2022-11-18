using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_interact : MonoBehaviour
{
    public GameObject button;
    public GameObject Panel;
    public Sprite sprite2;
    private Sprite sprite1;
    private Vector2 pos1;
    private Vector2 pos2;

    private void Awake() {
        button.SetActive(false);
        sprite1 = gameObject.GetComponent<SpriteRenderer>().sprite;
        pos1 = gameObject.transform.position;
        pos2 = new Vector2(0.3f, 0f);
        Panel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        button.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        button.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) & !Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D)) {
            if (button.activeSelf){
                if (gameObject.GetComponent<SpriteRenderer>().sprite == sprite1){
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                    gameObject.transform.position = pos1 - pos2;
                    Panel.SetActive(true);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().enabled = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Pause>().enabled = false;
                }
                else {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                    gameObject.transform.position = pos1;
                    Panel.SetActive(false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().enabled = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Pause>().enabled = true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)){
            if (button.activeSelf){
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Pause>().enabled = true;
            }
        }
    }
}
