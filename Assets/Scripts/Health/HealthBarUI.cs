using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image healthBarForegroundImage;


    // Esse parâmetro healthController aparece no evento de HealthChange justamente por ser um parâmetro
    public void UpdateHealthBar(HealthController healthController)
    {
        // fillAmount preenche a barra com a quantidade equivalente a porcentagem de vida
        // determinada no código do HealthController.
        healthBarForegroundImage.fillAmount = healthController.remainingHeatlthPercentage;
    }
}
