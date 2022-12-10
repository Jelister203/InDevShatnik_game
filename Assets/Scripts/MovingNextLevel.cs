using SaveLoad.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingNextLevel : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject button;
    public GameObject player;
    private void Start() {
        button.SetActive(false);
        Loader();
    }
    private void OnTriggerExit2D(Collider2D other) {
        button.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject == player){
        button.SetActive(true);}
    }
    private void Update() {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.E)){
            Saver();
            var data = SaveManager.Load<TestSave>(sceneToLoad).saveData;
            if (data.position == new Vector3(999,999,999)){
                SceneManager.LoadSceneAsync(sceneToLoad);
            }
            else {
            
            SceneManager.LoadSceneAsync(data.sceneName);
            }
        }
    }
    public void Saver() {
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
    public void Loader() {
        var data = SaveManager.Load<DefaultSave>(SceneManager.GetActiveScene().name).saveData;
            if (data.sceneName != SceneManager.GetActiveScene().name){
            }
            else {
                FindObjectOfType<PlayerMove>().transform.position = data.position;
                FindObjectOfType<Camera.Scripts.CUMera>().transform.position = data.position;
                if (data.items != null){
                    SignalChecker[] tempShit = FindObjectsOfType<SignalChecker>();
                    for (int i = 0; i < tempShit.Length; i++){
                        if (data.items[i]){
                        tempShit[i].SolvePuzle();}
                    }
                }
                if (data.dialogs != null){
                    for (int i = 0; i < data.dialogs.Length; i++){
                        var aboba = FindObjectsOfType<DialogManager>()[i];
                        foreach (Transform child in aboba.transform){
                        if (child.name == data.dialogs[i]){
                            child.GetComponent<MessageTrigger>().oldOne.GetComponent<MessageTrigger>().isActive = true;}}
                    }
                }
            }
    }
}

