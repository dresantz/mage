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
        // nameof serve apenas para passar o nome de um método sem criar uma variável
        Invoke(nameof(EndGame), timeToWaitBeforeExit);
    }

    private void EndGame()
    {
        // sceneController substitui o SceneManager
        sceneController.LoadScene("MainMenu");
    }
}
