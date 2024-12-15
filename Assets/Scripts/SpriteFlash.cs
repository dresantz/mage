using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Usa InChildre pois os gráficos estão num child do player
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void StartFlash(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
    }

    public IEnumerator FlashCoroutine (float flashDuration, Color flashColor, int numberOffFlashes)
    {
        Color startColor = spriteRenderer.color;
        float elapsedFlashTime = 0;
        float elapsedFlashPercentage = 0;

        while (elapsedFlashTime < flashDuration)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPercentage = elapsedFlashTime / flashDuration;

            if (elapsedFlashPercentage > 1)
            {
                elapsedFlashPercentage = 1;
            }

            float pingPongPercentage = Mathf.PingPong(elapsedFlashPercentage * 2 * numberOffFlashes, 1);
            spriteRenderer.color = Color.Lerp(startColor, flashColor, pingPongPercentage);

            yield return null;
        }
    }
}
