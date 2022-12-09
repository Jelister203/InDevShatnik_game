using SaveLoad.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingNextLevel : MonoBehaviour
{
    public Scene sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == FindObjectOfType<PlayerMove>()){
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync(sceneToLoad.buildIndex);
            
        }
    }
}
