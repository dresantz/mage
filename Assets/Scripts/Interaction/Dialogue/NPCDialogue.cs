using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueLines dialogue; // Refer�ncia ao ScriptableObject de Di�logo
    public Transform dialoguePosition; // Posi��o onde o di�logo ser� exibido

    public void TriggerDialogue()
    {
        if (dialogue != null)
        {
            DialogueManager.Instance.StartDialogueFromCutscene(dialogue, transform);
        }
    }
}

