using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour collectableBehaviour;

    public float startTime = 1f; // Tempo inicial antes de começar a piscar
    public float blinkDuration = 3f; // Duração do efeito de piscar antes da destruição
    public float initialBlinkRate = 0.5f; // Taxa inicial de piscar (segundos)
    public float finalBlinkRate = 0.1f; // Taxa final de piscar (segundos)

    private float elapsedTime = 0f;
    private float nextBlinkTime = 0f;
    private bool isVisible = true;
    private Renderer objectRenderer;

    private void Awake()
    {
        collectableBehaviour = GetComponent<ICollectableBehaviour>();
        objectRenderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > startTime)
        {
            float blinkProgress = Mathf.Clamp01((elapsedTime - startTime) / blinkDuration);
            float currentBlinkRate = Mathf.Lerp(initialBlinkRate, finalBlinkRate, blinkProgress);

            if (Time.time >= nextBlinkTime)
            {
                nextBlinkTime = Time.time + currentBlinkRate;
                isVisible = !isVisible;
                objectRenderer.enabled = isVisible;
            }
        }

        if (elapsedTime > startTime + blinkDuration)
        {
            Destroy(gameObject);
        }
    }

    // Destrói o coletável se o personagem colidir com ele
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }




}
