using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueLines : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;  // Nome do orador (NPC ou Player)
        [TextArea(3, 10)] 
        public string dialogueText; // Texto do diálogo
    }

    public DialogueLine[] dialogueLines; // Todas as falas do diálogo
}
