using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        IsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
