using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController healthController;
    private SpriteFlash spriteFlash;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        spriteFlash = GetComponent<SpriteFlash>();
    }

    public void StartInvincibility(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        // Uma corrotina, um c�digo executado em m�ltiplos frames
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        healthController.IsInvinceble = true;
        yield return spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes);
        healthController.IsInvinceble = false;
    }
}
