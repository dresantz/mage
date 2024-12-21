using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private float timeToWaitBeforeExit;
    [SerializeField]
    private SceneController sceneController;

    public GameObject endGameScreen;
    public TextMeshProUGUI endGameText;
    public string[] endGameMessages;
    private int currentMessageIndex = 0;
    private bool isEndGameScreenActive = false;
    private bool isTyping = false;
    private bool isLastMessageDisplayed = false;
    private bool isEndGameCompleted = false;
    private EnemySpawner[] enemySpawners;
    public float typingSpeed = 0.05f;


    void Start()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }


    void Update()
    {
        bool allSpawnersDestroyed = true;

        foreach (var spawner in enemySpawners)
        {
            if (spawner != null) // Se algum spawner ainda existe, mant�m o estado de espera.
            {
                allSpawnersDestroyed = false;
                break;
            }
        }

        // Ativa a tela de fim de jogo apenas uma vez
        if (allSpawnersDestroyed && !isEndGameScreenActive)
        {
            ActivateEndGameScreen();
            isEndGameScreenActive = true;  // Marca que a tela de fim de jogo foi ativada
        }

        // Quando o jogador pressionar a tecla E, avan�ar para a pr�xima frase
        if (Input.GetKeyDown(KeyCode.E) && isEndGameScreenActive && !isTyping && !isEndGameCompleted)
        {
            ShowNextMessage();
        }
    }


    private void ActivateEndGameScreen()
    {
        if (endGameScreen != null)
        {
            endGameScreen.SetActive(true);
            StopGamePlay();
            // Exibe a primeira mensagem da lista com a corrotina
            StartCoroutine(TypeSentence());
        }
    }


    private void ShowNextMessage()
    {
        // Se j� estamos na �ltima mensagem chama o MainMenu
        if (isLastMessageDisplayed)
        {
            EndAfterLastPhrase();
            return;
        }

        // Limpa o texto anterior antes de iniciar a nova frase
        endGameText.text = "";

        // Exibe a pr�xima mensagem da lista com a corrotina
        StartCoroutine(TypeSentence());
    }


    // Corrotina para escrever a frase uma letra de cada vez
    IEnumerator TypeSentence()
    {
        // Marca que a corrotina est� em execu��o
        isTyping = true;

        // Verifica se h� mensagens na lista
        if (endGameMessages.Length > 0)
        {
            // Itera sobre cada letra da frase atual
            foreach (char letter in endGameMessages[currentMessageIndex].ToCharArray())
            {
                endGameText.text += letter;  // Adiciona a letra ao texto
                yield return new WaitForSeconds(typingSpeed);  // Aguarda a velocidade definida
            }

            // Avan�a para a pr�xima mensagem, se n�o for a �ltima
            if (currentMessageIndex < endGameMessages.Length - 1)
            {
                currentMessageIndex++;  // Apenas aumenta o �ndice se n�o for o �ltimo item da lista
            }
            else
            {
                // Marca que a �ltima mensagem foi exibida
                isLastMessageDisplayed = true;
            }
        }

        // Marca que a corrotina terminou
        isTyping = false;
    }


    // Mata os inimigos e desativa fun��es do player e do pause.
    void StopGamePlay()
    {
        // Encontrar e desativar os c�digos do player
        PlayerMovement mov = FindObjectOfType<PlayerMovement>();
        PlayerShoot shoot = FindObjectOfType<PlayerShoot>();
        mov.enabled = false;
        shoot.enabled = false;

        PauseMenu pause = FindObjectOfType<PauseMenu>();
        pause.enabled = false;

        // Encontrar todos os objetos do tipo Enemy na cena
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        // Iterar sobre todos os inimigos encontrados
        foreach (Enemy enemy in enemies)
        {
            // Acessar o componente HealthController que est� no mesmo objeto do Enemy
            HealthController healthController = enemy.GetComponent<HealthController>();

            // Se o HealthController for encontrado
            if (healthController != null)
            {
                // Invocar o evento OnDied manualmente
                healthController.Die();
            }
        }
    }


    // Essas duas �ltimas fun��es gerenciam a transi��o para o MainMenu
    public void EndAfterLastPhrase()
    {
        // Flag impede que o jogador continue apertando E ap�s a �ltima frase
        // Ele corrige um bug que acontecia se ficasse apertando E.
        isEndGameCompleted = true;
        // nameof serve apenas para passar o nome de um m�todo sem criar uma vari�vel
        Invoke(nameof(GameOver), timeToWaitBeforeExit);
        
    }

    private void GameOver()
    {
        // sceneController substitui o SceneManager
        sceneController.LoadScene("MainMenu");
    }
}
