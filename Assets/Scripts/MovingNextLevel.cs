using SaveLoad.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using static diaolg;

public class MovingNextLevel : MonoBehaviour
{
    public AudioClip[] soundsopen_AR; 
    public string sceneToLoad;
    public GameObject button;
    public GameObject player;
    public GameObject widget;
    [SerializeField] private GameObject reallyimportantshit;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private string[] messages;
    private int i = 0;
    [SerializeField] int interactionType;
    public int itemIndex = -9;
    [SerializeField] private int[] invBoolIndexes;
    private void Start() {
        button.SetActive(false);
        if (!SceneManager.GetActiveScene().name.Contains("do_not_save"))
        {
            Debug.Log("Scene is OK, loading data...");
            Loader();
            widget.transform.GetChild(0).gameObject.SetActive(true);// это анимация, здесь всё нормально
            Invoke("breakDown", 1f);// не парься)))
        }
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
    private void Update() {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (reallyimportantshit == null)
            {
                if (invBoolIndexes.Length == 0 || boolIndexesCheck())
                {
                    GetComponent<AudioSource>().PlayOneShot(soundsopen_AR[Random.Range(0, soundsopen_AR.Length - 1)]);
                    if (!SceneManager.GetActiveScene().name.Contains("do_not_save"))
                    {
                        Saver();
                    }
                    widget.transform.GetChild(1).gameObject.SetActive(true);// animation
                    Invoke("nextDoor", 1f);// epta

                }
                else{
                    Saver();
                    SceneManager.LoadSceneAsync(sceneToLoad + " 1");
                }
            }
            else
            {
            if (messagePanel!=null)
            {
                if(interactionType == 1){
                if (!messagePanel.activeSelf)
                {
                    diaolg.SummonMessage(messagePanel, messages[i]);
                    i++;
                    if (!(i<messages.Length)){i = 0;if (itemIndex != -9){player.GetComponent<playerInventory>().PlayerInventory[itemIndex]=true;}}
                }
                else
                {
                    diaolg.killMessage(true, messagePanel);
                }
                }
                else{
                if (!messagePanel.activeSelf)
                {
                    if (i < messages.Length)
                        diaolg.SummonMessage(messagePanel, messages[i]);
                    i++;
                }
                else
                {
                    diaolg.killMessage(true, messagePanel);
                    if (i<messages.Length){
                        diaolg.SummonMessage(messagePanel, messages[i]);
                        i++;}
                    else {
                        i = 0;
                        if (itemIndex != -9){player.GetComponent<playerInventory>().PlayerInventory[itemIndex]=true;}
                    }
                }
                }
            }
        }
        }
        
    }
    private bool boolIndexesCheck(){
        for (int i = 0; i < invBoolIndexes.Length; i++){
            if (!player.GetComponent<playerInventory>().PlayerInventory[invBoolIndexes[i]]){return false;}
        }
        return true;
    }
    private void nextDoor(){
        var data = SaveManager.Load<TestSave>(sceneToLoad).saveData;
        if (data.position == new Vector3(999,999,999))
        {
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
        else
        {
            SceneManager.LoadSceneAsync(data.sceneName);
        }
    }
    private void breakDown()
    {
        widget.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Saver() {
        string title;
        string[] newDialogs;
        Vector3 pos;
        bool[] items;
        bool[] inventory;

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
        inventory = new bool[player.GetComponent<playerInventory>().PlayerInventoryLength];
        for (int i = 0; i < player.GetComponent<playerInventory>().PlayerInventoryLength; i++){
        inventory[i] = player.GetComponent<playerInventory>().PlayerInventory[i];}

        var sceneSave = new DefaultSave{position = pos, sceneName = title, dialogs = newDialogs, items = items};
        var SaveProfile = new SaveProfile<DefaultSave>(SceneManager.GetActiveScene().name, sceneSave);
        SaveManager.Save(SaveProfile);
        var etySave = new TestSave{position = new Vector3(999,999,999), sceneName = SceneManager.GetActiveScene().name};
        var etySaveProf = new SaveProfile<TestSave>("Empty", etySave);
        SaveManager.Save(etySaveProf);
        var invSave = new ThingsSave{inventory=inventory};
        var invSaveProf = new SaveProfile<ThingsSave>("playerInventory", invSave);
        SaveManager.Save(invSaveProf);
    }
    public void Loader() {
        var data2 = SaveManager.Load<ThingsSave>("playerInventory").saveData;
            if (data2.inventory != null){
            for (int i = 0; i < player.GetComponent<playerInventory>().PlayerInventoryLength; i++){
            player.GetComponent<playerInventory>().PlayerInventory[i] = data2.inventory[i];}}
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
