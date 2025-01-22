using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator interactableAnimator; // Referência ao Animator do baú
    private bool playerInRange = false; // Verifica se o jogador está no trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Certifique-se de que o jogador tem a tag "Player"
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void Activation()
    {
        if (playerInRange && interactableAnimator != null)
        {
            interactableAnimator.SetTrigger("Activate");
        }
    }
}