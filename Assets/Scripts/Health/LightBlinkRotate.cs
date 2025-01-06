using UnityEngine;

public class LightBlinkRotate : MonoBehaviour
{
    // Velocidade de rotação em graus por segundo
    public float rotationSpeed = 100f;

    void Update()
    {
        // Faz o objeto girar em torno do eixo X
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }
}

