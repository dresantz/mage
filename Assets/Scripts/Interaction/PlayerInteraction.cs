using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    // M�todo chamado automaticamente pelo PlayerInput quando a a��o "Interact" � executada
    public void OnInteract()
    {
        // Verifica se h� um objeto interag�vel e chama seu m�todo de ativa��o
        if (currentInteractable != null)
        {
            currentInteractable.Activation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Armazena a refer�ncia do objeto interag�vel atual
        if (other.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove a refer�ncia ao sair do trigger
        if (other.TryGetComponent(out Interactable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
