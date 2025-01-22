using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactablesInRange = new List<Interactable>();

    /* No modo Send Messages, os m�todos devem ter os mesmos nomes das a��es definidas no Input Actions.
     * Por exemplo: Se voc� criou uma a��o chamada Interact, o m�todo correspondente deve ser chamado OnInteract.*/

    // M�todo chamado automaticamente pelo PlayerInput quando a a��o "Interact" � executada
    public void OnInteract()
    {
        if (interactablesInRange.Count > 0)
        {
            // Ordena os objetos pela dist�ncia ao jogador
            SortInteractablesByDistance();

            // Ativa o objeto mais pr�ximo
            interactablesInRange[0].Activation();
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
        }
    }

    private void SortInteractablesByDistance()
    {
        interactablesInRange.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
    }
}
