using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1f);
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float slowMultiplier = 0.7f; // Diminui a velocidade em 30%

    private Transform playerTransform;
    private Vector3 originalScale;
    private bool playerOnBed = false;

    private PlayerMovement playerMovementScript;
    private float originalSpeed;

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector3 desiredScale = playerOnBed ? targetScale : originalScale;
            playerTransform.localScale = Vector3.Lerp(
                playerTransform.localScale,
                desiredScale,
                Time.deltaTime * lerpSpeed
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            originalScale = playerTransform.localScale;
            playerOnBed = true;

            playerMovementScript = other.GetComponent<PlayerMovement>();
            if (playerMovementScript != null)
            {
                originalSpeed = playerMovementScript.GetSpeed(); // você precisará expor o getter
                playerMovementScript.SetSpeed(originalSpeed * slowMultiplier);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnBed = false;

            if (playerMovementScript != null)
            {
                playerMovementScript.SetSpeed(originalSpeed);
            }
        }
    }
}
