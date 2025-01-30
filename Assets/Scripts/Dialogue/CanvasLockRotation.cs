using UnityEngine;

public class CanvasLockRotation : MonoBehaviour
{
    private Quaternion fixedRotation;

    void Start()
    {
        // Salva a rota��o inicial do Canvas.
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Mant�m a rota��o do Canvas fixa.
        transform.rotation = fixedRotation;
    }
}
