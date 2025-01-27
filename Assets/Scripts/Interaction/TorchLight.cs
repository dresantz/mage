using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class TorchLight : MonoBehaviour
{
    private Light2D torchLight;
    public float intensityMin = 0.8f;
    public float intensityMax = 1.2f;
    public float flickerSpeed = 0.1f;
    private float randomTimeOffset;

    void Start()
    {
        torchLight = GetComponent<Light2D>();
        randomTimeOffset = Random.Range(0f, 100f); // Para que cada tocha tenha uma oscilação única
    }

    void Update()
    {
        // Usando Perlin Noise para criar variação suave e natural
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, randomTimeOffset);
        torchLight.intensity = Mathf.Lerp(intensityMin, intensityMax, noise);
    }
}
