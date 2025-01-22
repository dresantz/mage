using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    // Método chamado automaticamente pelo PlayerInput quando a ação "Interact" é executada
    public void OnInteract()
    {
        // Verifica se há um objeto interagível e chama seu método de ativação
        if (currentInteractable != null)
        {
            currentInteractable.Activation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Armazena a referência do objeto interagível atual
        if (other.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove a referência ao sair do trigger
        if (other.TryGetComponent(out Interactable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
