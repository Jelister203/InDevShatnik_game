using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene("Menu"); }
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("GYM");
    }

    public void TestPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
