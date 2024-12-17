using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resistência inicial máxima
    private int currentResistance;

    private void Start()
    {
        currentResistance = maxResistance; // Define a resistência inicial
    }

    // Função para aplicar dano ao objeto
    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resistência pelo dano recebido

        // Verifica se a resistência chegou a zero
        if (currentResistance <= 0)
        {
            DestroyObject();
        }
    }

    // Função para destruir o objeto
    private void DestroyObject()
    {
        Destroy(gameObject); // Remove o objeto da cena
    }
}
