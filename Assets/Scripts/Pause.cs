using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pause;

    private bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {

                pause.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
            else {
                pause.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }
    }
}
