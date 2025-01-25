using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public Animator interactableAnimator; // Refer�ncia ao Animator do interag�vel
    public Renderer objectRenderer; // Renderer do objeto com o shader
    [Range(0.001f, 0.01f)] public float maxThickness = 0.01f; // Valor m�ximo do Thickness
    public float transitionSpeed = 1f; // Velocidade de transi��o do shader

    private Material materialInstance; // Inst�ncia do material
    private bool playerInRange = false; // Verifica se o jogador est� no trigger
    private Coroutine currentCoroutine; // Refer�ncia � corrotina ativa

    void Start()
    {
        // Garante que o objeto tenha uma inst�ncia �nica do material
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
        // Interrompe qualquer corrotina em execu��o para evitar conflitos
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Inicia a corrotina de transi��o
        currentCoroutine = StartCoroutine(ThicknessTransition(startValue, endValue));
    }

    private IEnumerator ThicknessTransition(float startValue, float endValue)
    {
        float elapsedTime = 0f;

        // Transi��o gradual do valor
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
