using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SaveLoad.Runtime;
using System.Collections.Generic;

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
                guipause = false;
                timer = 0;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Сохранить"))
                {
                    string title;
                    GameObject[] newDialogs;
                    Vector3 pos;
                    SlotClass[][] items;

                    InventoryManager[] exItems = FindObjectsOfType<InventoryManager>();
                    title = SceneManager.GetActiveScene().name;
                    pos = FindObjectOfType<PlayerMove>().transform.position;
                    var dialogs = FindObjectsOfType<DialogManager>();
                    newDialogs = new GameObject[dialogs.Length];
                    items = new SlotClass[exItems.Length][];
                    for (int i = 0; i < exItems.Length; i++) {
                        items[i] = exItems[i].items;
                    }
                    for (int i = 0; i < dialogs.Length; i++) {
                        newDialogs[i] = dialogs[i].gameObject;
                    }
                    var sceneSave = new DefaultSave{position = pos, sceneName = title, dialog = newDialogs, items = items};
                    var SaveProfile = new SaveProfile<DefaultSave>(SceneManager.GetActiveScene().name, sceneSave);
                    SaveManager.Save(SaveProfile);
                }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить"))
                {
                    string title = SceneManager.GetActiveScene().name;
                    var data = SaveManager.Load<DefaultSave>(SceneManager.GetActiveScene().name).saveData;
                    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                    SlotClass[][] items = data.items;
                    SceneManager.LoadSceneAsync(data.sceneName);
                    FindObjectOfType<PlayerMove>().gameObject.transform.position = data.position;
                }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню")) {
                ispause = false;
                timer = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
