using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Speaker Positions")]
    public Transform playerPosition;
    public Transform npcPosition;

    [Header("Dialogue Offsets")]
    public Vector3 playerDialogueOffset = new Vector3(0, 1.5f, 0);
    public Vector3 npcDialogueOffset = new Vector3(0, 1.5f, 0);

    private Transform currentSpeakerTransform;
    private Vector3 currentOffset;
    private DialogueLines currentDialogue;
    private int currentLineIndex = 0;

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

    public void StartDialogue(DialogueLines dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialogueBox.SetActive(true);
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            string speaker = currentDialogue.dialogueLines[currentLineIndex].speakerName;
            string text = currentDialogue.dialogueLines[currentLineIndex].dialogueText;
            dialogueText.text = text;

            // Determina automaticamente a posição do diálogo
            if (speaker == "Player")
            {
                currentSpeakerTransform = playerPosition;
                currentOffset = playerDialogueOffset;
            }
            else if (speaker == "NPC")
            {
                currentSpeakerTransform = npcPosition;
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
        if (dialogueBox.activeSelf && currentSpeakerTransform != null)
        {
            UpdateDialogueBoxPosition();
        }
    }

    private void UpdateDialogueBoxPosition()
    {
        if (currentSpeakerTransform != null)
        {
            Vector3 worldPosition = currentSpeakerTransform.position + currentOffset;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            dialogueBox.transform.position = screenPosition;
        }
    }

    public void NextLine()
    {
        ShowDialogue();
    }

    public void EndDialogue()
    {
        if (dialogueBox != null) // Verifica se o objeto ainda existe
        {
            dialogueBox.SetActive(false);
        }
    }
}
