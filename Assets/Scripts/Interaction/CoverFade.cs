using UnityEngine;

public class CoverFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f; // Dura��o do fade em segundos
    [SerializeField] private float fadedAlpha = 0.3f; // Transpar�ncia do objeto ao ser "desvanecido"

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            enabled = false;
            return;
        }

        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartFade(fadedAlpha);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartFade(originalColor.a);
        }
    }

    // Ao sair do PlayMode sob uma cobertura o jogo n�o detectava o OnTriggerExit2D
    // O erro foi corrigido criando a verifica��o !gameObject.activeInHierarchy
    private void StartFade(float targetAlpha)
    {
        if (!gameObject.activeInHierarchy)
        {
            return; // N�o inicia corrotina se o objeto estiver inativo
        }

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeToAlpha(targetAlpha));
    }


    private System.Collections.IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = spriteRenderer.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}

