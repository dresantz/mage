using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);

        // Usa-se o yield return tamb�m para chamar uma outra corrotina
        yield return FadeCoroutine(startColor, targetColor, duration);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);

        // No FadeOut � preciso ativar o gameObject antes de chamar a pr�xima corrotina
        gameObject.SetActive(true);

        // Usa-se o yield return tamb�m para chamar uma outra corrotina
        yield return FadeCoroutine(startColor, targetColor, duration);
    }


    // Corrotina se extende por m�ltiplos frames
    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedTime < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            sceneFadeImage.color = Color.Lerp(startColor, targetColor,elapsedPercentage);

            // yield return null faz com que a corrotina aguarde at� o pr�ximo frame
            yield return null;

            // No pr�ximo frame, somamos mais tempo ao elapsedTime
            elapsedTime += Time.deltaTime;
        }
    }
}
