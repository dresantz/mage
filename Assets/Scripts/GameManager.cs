using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeToWaitBeforeExit;

    [SerializeField]
    private SceneController sceneController;


    public void OnPlayerDied()
    {
        // nameof serve apenas para passar o nome de um m�todo sem criar uma vari�vel
        Invoke(nameof(GameOver), timeToWaitBeforeExit);
    }

    private void GameOver()
    {
        // sceneController substitui o SceneManager
        sceneController.LoadScene("MainMenu");
    }
}
