using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Instância única

    [Header("UI")]
    public GameObject dialogueBox;  // Caixa de diálogo
    public TextMeshProUGUI dialogueText; // Texto da caixa de diálogo
    public Transform playerPosition; // Posição do player
    public Transform npcPosition;    // Posição do NPC
    public Canvas canvas;  // O canvas da interface (precisa ser definido)

    private DialogueLines currentDialogue; // Referência ao diálogo atual
    private int currentLineIndex = 0; // Linha atual do diálogo

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Garantir que exista apenas uma instância
        }
    }

    // Inicia o diálogo
    public void StartDialogue(DialogueLines dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialogueBox.SetActive(true); // Ativa a caixa de diálogo
        ShowDialogue(); // Mostra o primeiro diálogo
    }

    // Exibe a fala atual e move a caixa de diálogo
    private void ShowDialogue()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Verifica quem está falando e ajusta a posição
            if (speaker == "Player")
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPosition.position);
                dialogueBox.transform.position = screenPos;
            }
            else if (speaker == "NPC")
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(npcPosition.position);
                dialogueBox.transform.position = screenPos;
            }

            currentLineIndex++;
        }
        else
        {
            EndDialogue(); // Finaliza o diálogo
        }
    }

    // Avança para a próxima fala
    public void NextLine()
    {
        ShowDialogue();
    }

    // Finaliza o diálogo
    private void EndDialogue()
    {
        dialogueBox.SetActive(false); // Desativa a caixa de diálogo
    }
}
