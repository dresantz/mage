using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform fillRect;

    public void UpdateHealthBar(HealthController healthController)
    {
        if (fillRect != null)
        {
            // Usa o valor normalizado de remainingHealthPercentage para ajustar a escala vertical (Y)
            fillRect.localScale = new Vector3(1f, healthController.remainingHeatlthPercentage, 1f);
        }
    }
}
