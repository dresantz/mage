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

            // Verifica se o objeto interag�vel tem o componente NPCDialogue
            NPCDialogue npcDialogue = interactablesInRange[0].GetComponent<NPCDialogue>();

            if (npcDialogue != null && npcDialogue.dialogue != null)
            {
                if (!isInteractingWithNPC)
                {
                    // Inicia o di�logo, passando o di�logo e a posi��o do NPC
                    DialogueManager.Instance.StartDialogue(npcDialogue.dialogue, npcDialogue.dialoguePosition);
                    isInteractingWithNPC = true;
                }
                else
                {
                    // Avan�a para a pr�xima linha do di�logo
                    DialogueManager.Instance.NextLine();
                }
            }
            else
            {
                // Se n�o for um NPC com di�logo, ativa o objeto interag�vel normalmente
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

            // Se o player sair do Collider 2D de um NPC enquanto interage, encerra o di�logo
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
