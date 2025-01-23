using UnityEngine;

public class Debri : MonoBehaviour
{
    public float rotationSpeed = 150f; // Velocidade máxima de rotação ao interagir

    private bool isRotating = false; // Controla se o objeto está girando
    private float rotationTimer = 0f; // Tempo de rotação restante
    public float rotationDuration = 0.5f; // Duração da rotação em segundos

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto é o player
        if (collision.CompareTag("Player"))
        {
            StartRotation();
        }
    }

    private void Update()
    {
        // Se o objeto está girando, aplica a rotação
        if (isRotating)
        {
            rotationTimer -= Time.deltaTime;

            // Aplica uma rotação aleatória
            float randomRotation = Random.Range(-rotationSpeed, rotationSpeed);
            transform.Rotate(0, 0, randomRotation * Time.deltaTime);

            // Para a rotação quando o tempo acabar
            if (rotationTimer <= 0)
            {
                isRotating = false;
            }
        }
    }

    private void StartRotation()
    {
        // Inicia a rotação por um período definido
        isRotating = true;
        rotationTimer = rotationDuration;
    }
}
