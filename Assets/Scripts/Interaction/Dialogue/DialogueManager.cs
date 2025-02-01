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

    private Transform currentSpeakerTransform; // Guarda quem está falando

    private void ShowDialogue()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Define quem está falando e ajusta o transform
            if (speaker == "Player")
            {
                currentSpeakerTransform = playerPosition;
            }
            else if (speaker == "NPC")
            {
                currentSpeakerTransform = npcPosition;
            }

            // Atualiza a posição inicial imediatamente
            if (currentSpeakerTransform != null)
            {
                dialogueBox.transform.position = Camera.main.WorldToScreenPoint(currentSpeakerTransform.position);
            }

            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    // Atualiza constantemente a posição da caixa de diálogo
    private void Update()
    {
        if (dialogueBox.activeSelf && currentSpeakerTransform != null)
        {
            dialogueBox.transform.position = Camera.main.WorldToScreenPoint(currentSpeakerTransform.position);
        }
    }


    // Avança para a próxima fala
    public void NextLine()
    {
        ShowDialogue();
    }

    // Finaliza o diálogo
    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Desativa a caixa de diálogo
    }
}
