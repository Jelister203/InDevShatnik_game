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
        string[] newDialogs;
        Vector3 pos;
        bool[] items;

        SignalChecker[] exItems = FindObjectsOfType<SignalChecker>();
        title = SceneManager.GetActiveScene().name;
        pos = FindObjectOfType<PlayerMove>().transform.position;
        var dialogs = FindObjectsOfType<DialogManager>();
        newDialogs = new string[dialogs.Length];
        items = new bool[exItems.Length];
        for (int i = 0; i < exItems.Length; i++) {
            items[i] = exItems[i].inventory.GetComponent<InventoryManager>().isWorking;
        }
        for (int i = 0; i < dialogs.Length; i++) {
            newDialogs[i] = dialogs[i].finalMessage.name;
        }
        var sceneSave = new DefaultSave{position = pos, sceneName = title, dialogs = newDialogs, items = items};
        var SaveProfile = new SaveProfile<DefaultSave>(SceneManager.GetActiveScene().name, sceneSave);
        SaveManager.Save(SaveProfile);
        var etySave = new TestSave{position = new Vector3(999,999,999), sceneName = SceneManager.GetActiveScene().name};
        var etySaveProf = new SaveProfile<TestSave>("Empty", etySave);
        SaveManager.Save(etySaveProf);
                }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить"))
                {
                var data = SaveManager.Load<DefaultSave>("Empty").saveData;
                SceneManager.LoadScene(data.sceneName);
                }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню")) {
                ispause = false;
                timer = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}