using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public Animator interactableAnimator; // Referência ao Animator do interagível
    public Renderer objectRenderer; // Renderer do objeto com o shader
    [Range(0.001f, 0.01f)] public float maxThickness = 0.01f; // Valor máximo do Thickness
    public float transitionSpeed = 1f; // Velocidade de transição do shader

    private Material materialInstance; // Instância do material
    private bool playerInRange = false; // Verifica se o jogador está no trigger
    private Coroutine currentCoroutine; // Referência à corrotina ativa

    void Start()
    {
        // Garante que o objeto tenha uma instância única do material
        materialInstance = objectRenderer.material;
        materialInstance.SetFloat("_Tickness", 0f); // Inicializa o shader com Thickness = 0
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Certifique-se de que o jogador tem a tag "Player"
        {
            playerInRange = true;
            StartThicknessEffect(0f, maxThickness); // Aumenta o efeito visual
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            StartThicknessEffect(maxThickness, 0f); // Reduz o efeito visual
        }
    }

    public void Activation()
    {
        if (playerInRange && interactableAnimator != null)
        {
            interactableAnimator.SetTrigger("Activate");
        }
    }

    private void StartThicknessEffect(float startValue, float endValue)
    {
        // Interrompe qualquer corrotina em execução para evitar conflitos
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Inicia a corrotina de transição
        currentCoroutine = StartCoroutine(ThicknessTransition(startValue, endValue));
    }

    private IEnumerator ThicknessTransition(float startValue, float endValue)
    {
        float elapsedTime = 0f;

        // Transição gradual do valor
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            float thicknessValue = Mathf.Lerp(startValue, endValue, elapsedTime);
            materialInstance.SetFloat("_Tickness", thicknessValue);
            yield return null;
        }

        // Garante que o valor final seja o esperado
        materialInstance.SetFloat("_Tickness", endValue);
    }
}
