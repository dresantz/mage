using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Biblioteca de Eventos (namespace)
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealt;
    [SerializeField]
    private float maximumHealth;

    public float remainingHeatlthPercentage
    {
        // get e set s�o acessors, eles pegam info de campos privados.
        // get pega (l� info) e retorna, e o set estabelece, geralmente com um "=".
        // geralmente usados em propriedade publics como remainingHealthPercentage.
        get
        {
            return currentHealt / maximumHealth;
        }
    }

    // Vamos usar para diminuir a frequ�ncia de dano do inimigo
    public bool IsInvinceble { get; set; }

    // Cria no Inspector um evento que acontecer� se o Invoke for ativado.
    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    // Ligado direto ao c�digo HealthBarUI
    public UnityEvent OnHealthChanged;

    // Sofrendo dano
    public void TakeDamage(float damageAmount)
    {
        if (currentHealt == 0)
        {
            // O return pula esse if se for zero e passa para o pr�ximo
            // que � verififcar se est� abaixo de zero
            return;
        }

        if (IsInvinceble == true)
        {
            return;
        }

        // Subtrai o dano da vida atual
        currentHealt -= damageAmount;

        OnHealthChanged.Invoke();

        // Se estiver abaixo de zero, setamos em zero.
        if (currentHealt < 0)
        {
            currentHealt = 0;
        }

        if (currentHealt == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (currentHealt == maximumHealth)
        {
            return;
        }
        currentHealt += amountToAdd;

       OnHealthChanged.Invoke();

        if (currentHealt > maximumHealth)
        {
            currentHealt = maximumHealth;
        }

    }
}
