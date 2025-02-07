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
    private Transform npcTransform; // Transform do NPC (armazenado para refer�ncia)
    private Vector3 currentOffset; // Offset do di�logo
    private DialogueLines currentDialogue; // Di�logo atual
    private int currentLineIndex = 0; // �ndice da linha atual do di�logo

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

    // M�todo para iniciar o di�logo
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

        isDialogueActive = true; // Ativa a flag para permitir intera��o durante a cutscene

        ShowLine();
    }



    // M�todo para exibir a linha atual do di�logo
    private void ShowLine()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Determina a posi��o do di�logo com base no falante
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
        // Atualiza a posi��o da caixa de di�logo enquanto estiver ativa
        if (dialogueBox.activeSelf && currentSpeakerTransform != null)
        {
            UpdateDialogueBoxPosition();
        }
    }

    // M�todo para atualizar a posi��o da caixa de di�logo
    private void UpdateDialogueBoxPosition()
    {
        if (currentSpeakerTransform != null)
        {
            Vector3 worldPosition = currentSpeakerTransform.position + currentOffset;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            dialogueBox.transform.position = screenPosition;
        }
    }

    // M�todo para avan�ar para a pr�xima linha do di�logo
    public void NextLine()
    {
        ShowLine();
    }

    // M�todo para encerrar o di�logo
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