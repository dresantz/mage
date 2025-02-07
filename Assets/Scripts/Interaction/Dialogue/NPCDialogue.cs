using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueLines dialogue; // Referência ao ScriptableObject de Diálogo
    public Transform dialoguePosition; // Posição onde o diálogo será exibido

    public void TriggerDialogue()
    {
        if (dialogue != null)
        {
            DialogueManager.Instance.StartDialogueFromCutscene(dialogue, transform);
        }
    }
}

