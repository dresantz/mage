using UnityEngine;

public class FillShake : MonoBehaviour
{
    public RectTransform targetImage;
    public float swingAngle = 15f;
    public float speed = 2f;

    private float _time;

    void Update()
    {
        if (targetImage == null)
            return;

        // Calcula o �ngulo de balan�o usando uma onda senoidal
        float angle = Mathf.Sin(_time * speed) * swingAngle;

        // Aplica a rota��o ao RectTransform
        targetImage.localRotation = Quaternion.Euler(0, 0, angle);

        // Incrementa o tempo
        _time += Time.deltaTime;
    }
}
