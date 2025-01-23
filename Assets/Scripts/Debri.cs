using UnityEngine;

public class Debri : MonoBehaviour
{
    public float rotationSpeed = 150f; // Velocidade m�xima de rota��o ao interagir

    private bool isRotating = false; // Controla se o objeto est� girando
    private float rotationTimer = 0f; // Tempo de rota��o restante
    public float rotationDuration = 0.5f; // Dura��o da rota��o em segundos

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto � o player
        if (collision.CompareTag("Player"))
        {
            StartRotation();
        }
    }

    private void Update()
    {
        // Se o objeto est� girando, aplica a rota��o
        if (isRotating)
        {
            rotationTimer -= Time.deltaTime;

            // Aplica uma rota��o aleat�ria
            float randomRotation = Random.Range(-rotationSpeed, rotationSpeed);
            transform.Rotate(0, 0, randomRotation * Time.deltaTime);

            // Para a rota��o quando o tempo acabar
            if (rotationTimer <= 0)
            {
                isRotating = false;
            }
        }
    }

    private void StartRotation()
    {
        // Inicia a rota��o por um per�odo definido
        isRotating = true;
        rotationTimer = rotationDuration;
    }
}
