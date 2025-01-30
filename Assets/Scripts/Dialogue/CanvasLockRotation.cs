using UnityEngine;

public class CanvasLockRotation : MonoBehaviour
{
    private Quaternion fixedRotation;

    void Start()
    {
        // Salva a rotação inicial do Canvas.
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Mantém a rotação do Canvas fixa.
        transform.rotation = fixedRotation;
    }
}
