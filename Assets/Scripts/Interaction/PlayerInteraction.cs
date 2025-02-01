using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactablesInRange = new List<Interactable>();
    private bool isInteractingWithNPC = false; // Vari�vel para verificar se estamos interagindo com um NPC

    /* No modo Send Messages, os m�todos devem ter os mesmos nomes das a��es definidas no Input Actions.
     * Por exemplo: Se voc� criou uma a��o chamada Interact, o m�todo correspondente deve ser chamado OnInteract.*/

    // M�todo chamado automaticamente pelo PlayerInput quando a a��o "Interact" � executada
    public void OnInteract()
    {
        if (interactablesInRange.Count > 0)
        {
            // Ordena os objetos pela dist�ncia ao jogador
            SortInteractablesByDistance();

            // Verifica se o interag�vel tem um Dialogue associado
            NPCDialogue npcDialogue = interactablesInRange[0].GetComponent<NPCDialogue>();

            if (npcDialogue != null && npcDialogue.dialogue != null)
            {
                // Se n�o estamos interagindo ainda, inicia o di�logo
                if (!isInteractingWithNPC)
                {
                    DialogueManager.Instance.StartDialogue(npcDialogue.dialogue);
                    isInteractingWithNPC = true;
                }
                else
                {
                    // Se j� estamos no meio do di�logo, passa para a pr�xima fala
                    DialogueManager.Instance.NextLine();
                }
            }
            else
            {
                // Ativa o objeto mais pr�ximo (se n�o houver di�logo)
                interactablesInRange[0].Activation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Adiciona o objeto interag�vel � lista
        if (other.TryGetComponent(out Interactable interactable) && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove o objeto interag�vel da lista ao sair do trigger
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactablesInRange.Remove(interactable);
            isInteractingWithNPC = false;  // Reseta a vari�vel quando sai do Collider
        }
    }

    private void SortInteractablesByDistance()
    {
        interactablesInRange.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
    }
}
