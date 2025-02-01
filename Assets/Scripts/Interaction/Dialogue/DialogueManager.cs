using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Inst�ncia �nica

    [Header("UI")]
    public GameObject dialogueBox;  // Caixa de di�logo
    public TextMeshProUGUI dialogueText; // Texto da caixa de di�logo
    public Transform playerPosition; // Posi��o do player
    public Transform npcPosition;    // Posi��o do NPC
    public Canvas canvas;  // O canvas da interface (precisa ser definido)

    private DialogueLines currentDialogue; // Refer�ncia ao di�logo atual
    private int currentLineIndex = 0; // Linha atual do di�logo

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Garantir que exista apenas uma inst�ncia
        }
    }

    // Inicia o di�logo
    public void StartDialogue(DialogueLines dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialogueBox.SetActive(true); // Ativa a caixa de di�logo
        ShowDialogue(); // Mostra o primeiro di�logo
    }

    private Transform currentSpeakerTransform; // Guarda quem est� falando

    private void ShowDialogue()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Define quem est� falando e ajusta o transform
            if (speaker == "Player")
            {
                currentSpeakerTransform = playerPosition;
            }
            else if (speaker == "NPC")
            {
                currentSpeakerTransform = npcPosition;
            }

            // Atualiza a posi��o inicial imediatamente
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

    // Atualiza constantemente a posi��o da caixa de di�logo
    private void Update()
    {
        if (dialogueBox.activeSelf && currentSpeakerTransform != null)
        {
            dialogueBox.transform.position = Camera.main.WorldToScreenPoint(currentSpeakerTransform.position);
        }
    }


    // Avan�a para a pr�xima fala
    public void NextLine()
    {
        ShowDialogue();
    }

    // Finaliza o di�logo
    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Desativa a caixa de di�logo
    }
}
