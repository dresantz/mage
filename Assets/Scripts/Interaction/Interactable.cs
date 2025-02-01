using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public Animator interactableAnimator; // Referência ao Animator do interagível
    public Renderer objectRenderer; // Renderer do objeto com o shader
    [Range(0.001f, 0.01f)] public float maxThickness = 0.01f; // Valor máximo do Thickness
    public float transitionSpeed = 1f; // Velocidade de transição do shader
    public bool useRendererEffect = true; // NOVO: Permite ativar/desativar o efeito visual

    private Material materialInstance; // Instância do material
    private bool playerInRange = false; // Verifica se o jogador está no trigger
    private Coroutine currentCoroutine; // Referência à corrotina ativa

    public bool useAnimator = true; // Novo: ativa ou desativa o uso do Animator

    void Start()
    {
        // Se o uso do renderer estiver ativo, inicializa o material
        if (useRendererEffect && objectRenderer != null)
        {
            materialInstance = objectRenderer.material;
            materialInstance.SetFloat("_Tickness", 0f); // Inicializa o shader com Thickness = 0
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (useRendererEffect) StartThicknessEffect(0f, maxThickness);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (useRendererEffect) StartThicknessEffect(maxThickness, 0f);
        }
    }

    public void Activation()
    {
        if (playerInRange)
        {
            if (useAnimator && interactableAnimator != null) // Apenas ativa se estiver permitido
            {
                interactableAnimator.SetTrigger("Activate");
            }
            else
            {
                Debug.Log($"Animação ignorada para {gameObject.name}.");
            }
        }
    }


    private void StartThicknessEffect(float startValue, float endValue)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ThicknessTransition(startValue, endValue));
    }

    private IEnumerator ThicknessTransition(float startValue, float endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            float thicknessValue = Mathf.Lerp(startValue, endValue, elapsedTime);
            materialInstance.SetFloat("_Tickness", thicknessValue);
            yield return null;
        }

        materialInstance.SetFloat("_Tickness", endValue);
    }
}
