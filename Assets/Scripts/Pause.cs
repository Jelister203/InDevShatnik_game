using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public float timer;
    public bool ispause;
    public bool guipause;
    void Update() {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispause == false) {
            ispause = true;
            guipause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispause == true) {
            ispause = false;
            guipause = false;
        }
        if (ispause == true) {
            timer = 0;
        }
        else if (ispause == false) {
            timer = 1f;
        }
    }
    public void OnGUI() {
        if (guipause == true) {
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить")) {
                ispause = false;
                timer = 0;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Сохранить"))
                { }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить"))
                { }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню")) {
                ispause = false;
                timer = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
