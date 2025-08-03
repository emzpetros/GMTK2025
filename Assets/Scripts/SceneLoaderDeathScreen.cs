using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderDeathScreen : MonoBehaviour
{

    [SerializeField] private string sceneName;
    public void LoadMainScene() {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit() {
        Application.Quit();
    }
}
