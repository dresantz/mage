using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactablesInRange = new List<Interactable>();
    private bool isInteractingWithNPC = false; // Variável para verificar se estamos interagindo com um NPC

    /* No modo Send Messages, os métodos devem ter os mesmos nomes das ações definidas no Input Actions.
     * Por exemplo: Se você criou uma ação chamada Interact, o método correspondente deve ser chamado OnInteract.*/

    // Método chamado automaticamente pelo PlayerInput quando a ação "Interact" é executada
    public void OnInteract()
    {
        if (interactablesInRange.Count > 0)
        {
            // Ordena os objetos pela distância ao jogador
            SortInteractablesByDistance();

            // Verifica se o interagível tem um Dialogue associado
            NPCDialogue npcDialogue = interactablesInRange[0].GetComponent<NPCDialogue>();

            if (npcDialogue != null && npcDialogue.dialogue != null)
            {
                // Se não estamos interagindo ainda, inicia o diálogo
                if (!isInteractingWithNPC)
                {
                    DialogueManager.Instance.StartDialogue(npcDialogue.dialogue);
                    isInteractingWithNPC = true;
                }
                else
                {
                    // Se já estamos no meio do diálogo, passa para a próxima fala
                    DialogueManager.Instance.NextLine();
                }
            }
            else
            {
                // Ativa o objeto mais próximo (se não houver diálogo)
                interactablesInRange[0].Activation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Adiciona o objeto interagível à lista
        if (other.TryGetComponent(out Interactable interactable) && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove o objeto interagível da lista ao sair do trigger
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactablesInRange.Remove(interactable);
            isInteractingWithNPC = false;  // Reseta a variável quando sai do Collider
        }
    }

    private void SortInteractablesByDistance()
    {
        interactablesInRange.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
    }
}
