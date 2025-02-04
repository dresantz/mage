using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactablesInRange = new List<Interactable>();
    private bool isInteractingWithNPC = false; // Verifica se estamos interagindo com um NPC

    public void OnInteract()
    {
        if (interactablesInRange.Count > 0)
        {
            SortInteractablesByDistance();

            // Verifica se o objeto interagível tem o componente NPCDialogue
            NPCDialogue npcDialogue = interactablesInRange[0].GetComponent<NPCDialogue>();

            if (npcDialogue != null && npcDialogue.dialogue != null)
            {
                if (!isInteractingWithNPC)
                {
                    // Inicia o diálogo, passando o diálogo e a posição do NPC
                    DialogueManager.Instance.StartDialogue(npcDialogue.dialogue, npcDialogue.dialoguePosition);
                    isInteractingWithNPC = true;
                }
                else
                {
                    // Avança para a próxima linha do diálogo
                    DialogueManager.Instance.NextLine();
                }
            }
            else
            {
                // Se não for um NPC com diálogo, ativa o objeto interagível normalmente
                interactablesInRange[0].Activation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable) && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactablesInRange.Remove(interactable);

            // Se o player sair do Collider 2D de um NPC enquanto interage, encerra o diálogo
            if (isInteractingWithNPC)
            {
                DialogueManager.Instance.EndDialogue();
                isInteractingWithNPC = false;
            }
        }
    }

    private void SortInteractablesByDistance()
    {
        interactablesInRange.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
    }
}
