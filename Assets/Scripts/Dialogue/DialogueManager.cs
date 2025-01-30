using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string dialogueText; // Texto do di�logo.
    public List<DialogueChoice> choices; // Op��es de escolha do jogador.
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText; // Texto da escolha.
    public int nextNodeID; // ID do pr�ximo n�.
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueNode> nodes; // Lista de n�s do di�logo.

    // M�todo para encontrar um n� pelo ID.
    public DialogueNode GetNodeByID(int id)
    {
        if (id < 0 || id >= nodes.Count)
        {
            Debug.LogError($"Node ID {id} is out of range!");
            return null;
        }
        return nodes[id];
    }
}


public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue; // Di�logo ativo.
    public GameObject dialogueUI; // UI do texto.
    public TMPro.TextMeshProUGUI dialogueText; // Texto do di�logo.

    private DialogueNode currentNode;

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentNode = dialogue.GetNodeByID(0); // Sempre come�a no primeiro n� (ID 0).
        dialogueUI.SetActive(true);
        DisplayCurrentNode();
    }

    public void DisplayCurrentNode()
    {
        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentNode.dialogueText;
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        // Aqui voc� implementa o sistema de exibi��o das escolhas, similar ao anterior.
        // Agora use o ID do pr�ximo n� para navegar.
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        if (choiceIndex < 0 || choiceIndex >= currentNode.choices.Count)
        {
            Debug.LogError("Invalid choice index!");
            return;
        }

        // Usa o ID do pr�ximo n� para buscar o pr�ximo n�.
        int nextNodeID = currentNode.choices[choiceIndex].nextNodeID;
        currentNode = currentDialogue.GetNodeByID(nextNodeID);
        DisplayCurrentNode();
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
    }
}
