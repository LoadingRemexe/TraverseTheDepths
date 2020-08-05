using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen = null;
    bool paused = false;
    void Start()
    {
        pauseScreen.SetActive(paused);
        Time.timeScale = (paused) ? 0 : 1;
        Cursor.lockState = (paused) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseScreen)
            {
                paused = !paused;
                pauseScreen.SetActive(paused);
                Time.timeScale = (paused) ? 0 : 1;
                Cursor.lockState = (paused) ? CursorLockMode.None: CursorLockMode.Locked;
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        FindObjectOfType<LoadingScreen>().Show(SceneManager.LoadSceneAsync(sceneName));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
