using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactablesInRange = new List<Interactable>();

    /* No modo Send Messages, os métodos devem ter os mesmos nomes das ações definidas no Input Actions.
     * Por exemplo: Se você criou uma ação chamada Interact, o método correspondente deve ser chamado OnInteract.*/

    // Método chamado automaticamente pelo PlayerInput quando a ação "Interact" é executada
    public void OnInteract()
    {
        if (interactablesInRange.Count > 0)
        {
            // Ordena os objetos pela distância ao jogador
            SortInteractablesByDistance();

            // Ativa o objeto mais próximo
            interactablesInRange[0].Activation();
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
        }
    }

    private void SortInteractablesByDistance()
    {
        interactablesInRange.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
    }
}
