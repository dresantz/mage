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


    public Collider2D boundaryCollider; // O Collider2D que define os limites da câmera
    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        if (boundaryCollider == null)
        {
            Debug.LogError("Por favor, atribua um Collider2D para definir os limites da câmera.");
            return;
        }

        mainCamera = Camera.main;

        // Calcula os limites do Collider2D
        Bounds bounds = boundaryCollider.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;

        // Calcula o tamanho da câmera com base no tamanho da viewport
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * mainCamera.aspect;
    }


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




        if (boundaryCollider == null)
            return;

        // Obtém a posição atual da câmera
        Vector3 cameraPosition = transform.position;

        // Restringe a posição da câmera para dentro dos limites
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        // Aplica a nova posição
        transform.position = cameraPosition;
    }
}
