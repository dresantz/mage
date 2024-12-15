using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image healthBarForegroundImage;


    // Esse par�metro healthController aparece no evento de HealthChange justamente por ser um par�metro
    public void UpdateHealthBar(HealthController healthController)
    {
        // fillAmount preenche a barra com a quantidade equivalente a porcentagem de vida
        // determinada no c�digo do HealthController.
        healthBarForegroundImage.fillAmount = healthController.remainingHeatlthPercentage;
    }
}
