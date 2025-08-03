using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderDeathScreen : MonoBehaviour
{
    public void LoadMainScene() {
        SceneManager.LoadScene("MainScene");
    }
}
