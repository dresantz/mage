using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Offsets")]
    public Vector3 playerDialogueOffset = new Vector3(0, 1.5f, 0);
    public Vector3 npcDialogueOffset = new Vector3(0, 1.5f, 0);

    private Transform currentSpeakerTransform; // Transform do falante atual (jogador ou NPC)
    private Transform npcTransform; // Transform do NPC (armazenado para referência)
    private Vector3 currentOffset; // Offset do diálogo
    private DialogueLines currentDialogue; // Diálogo atual
    private int currentLineIndex = 0; // Índice da linha atual do diálogo

    public bool isDialogueActive { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para iniciar o diálogo
    public void StartDialogue(DialogueLines dialogue, Transform npcTransform)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;
        this.npcTransform = npcTransform;
        dialogueBox.SetActive(true);

        isDialogueActive = true; // Ativa a flag

        ShowLine();
    }


    public void StartDialogueFromCutscene(DialogueLines dialogue, Transform npcTransform)
    {
        if (dialogue == null) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;
        this.npcTransform = npcTransform;
        dialogueBox.SetActive(true);

        isDialogueActive = true; // Ativa a flag para permitir interação durante a cutscene

        ShowLine();
    }



    // Método para exibir a linha atual do diálogo
    private void ShowLine()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Determina a posição do diálogo com base no falante
            if (speaker == "Player")
            {
                // Usa o transform do jogador (assumindo que o jogador tem a tag "Player")
                currentSpeakerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                currentOffset = playerDialogueOffset;
            }
            else if (speaker == "NPC")
            {
                // Usa o transform do NPC que foi armazenado
                currentSpeakerTransform = npcTransform;
                currentOffset = npcDialogueOffset;
            }

            UpdateDialogueBoxPosition();
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        // Atualiza a posição da caixa de diálogo enquanto estiver ativa
        if (dialogueBox.activeSelf && currentSpeakerTransform != null)
        {
            UpdateDialogueBoxPosition();
        }
    }

    // Método para atualizar a posição da caixa de diálogo
    private void UpdateDialogueBoxPosition()
    {
        if (currentSpeakerTransform != null)
        {
            Vector3 worldPosition = currentSpeakerTransform.position + currentOffset;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            dialogueBox.transform.position = screenPosition;
        }
    }

    // Método para avançar para a próxima linha do diálogo
    public void NextLine()
    {
        ShowLine();
    }

    // Método para encerrar o diálogo
    public void EndDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
            currentSpeakerTransform = null;
            npcTransform = null;
            isDialogueActive = false; // Desativa a flag
        }
    }
}