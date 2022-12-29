using System.IO;
using SaveLoad.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string sceneName;
    public AudioClip music;
    public void Start() {
        sceneName = "PE_Class 1 (do_not_save)";
    }
    /*
    public void menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene("Menu"); }
    }*/
    public void PlayPressed()
    {
        try
        {
            var data = SaveManager.Load<TestSave>("Empty").saveData;
            if (data.sceneName != "Empty"){
                SceneManager.LoadSceneAsync(data.sceneName);
            }
            else{
            SaveManager.Drop();
            SceneManager.LoadScene(sceneName);
            }
        }
            
        catch
        {
            TestPressed();
        }
    }

    public void TestPressed()
    {
        SaveManager.Drop();
        SceneManager.LoadScene(sceneName);
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}