using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Offset entre a câmera e o jogador (apenas no plano X e Y)
    public Vector3 offset;

    // Velocidade para suavizar o movimento da câmera
    public float smoothSpeed = 0.125f;

    // Fator multiplicador para o deslocamento da câmera na direção do mouse
    public float displacementMultiplier = 0.15f;

    void LateUpdate()
    {

        if (PauseMenu.IsPaused)
        {
            return;
        }

        // Calcula a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        // Calcula o deslocamento da câmera na direção do mouse
        Vector3 cameraDisplace = (mousePosition - player.position) * displacementMultiplier;

        // Calcula a posição desejada com base no offset e deslocamento (mantendo Z fixo)
        Vector3 desiredPosition = player.position + offset + new Vector3(cameraDisplace.x, cameraDisplace.y, 0);

        // Suaviza o movimento da câmera interpolando entre a posição atual e a desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }
}
