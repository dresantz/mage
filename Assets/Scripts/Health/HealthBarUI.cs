using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider healthBarForeground;

    // Esse par�metro healthController aparece no evento de HealthChange justamente por ser um par�metro
    public void UpdateHealthBar(HealthController healthController)
    {
        // value preenche a barra com a quantidade equivalente a porcentagem de vida
        healthBarForeground.value = healthController.remainingHeatlthPercentage;
        // remainingHeatlthPercentage � determinada no c�digo do HealthController.
    }
}
