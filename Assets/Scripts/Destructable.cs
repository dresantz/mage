using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resist�ncia inicial m�xima
    private int currentResistance;

    private void Start()
    {
        currentResistance = maxResistance; // Define a resist�ncia inicial
    }

    // Fun��o para aplicar dano ao objeto
    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resist�ncia pelo dano recebido

        // Verifica se a resist�ncia chegou a zero
        if (currentResistance <= 0)
        {
            DestroyObject();
        }
    }

    // Fun��o para destruir o objeto
    private void DestroyObject()
    {
        Destroy(gameObject); // Remove o objeto da cena
    }
}
