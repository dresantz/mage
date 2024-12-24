using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resist�ncia inicial m�xima
    private int currentResistance;

    public GameObject destructionEffect; // Prefab do sistema de part�culas para destrui��o

    private void Start()
    {
        // Define a resist�ncia inicial
        currentResistance = maxResistance;
    }

    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resist�ncia pelo dano recebido

        // Garante que a resist�ncia nunca seja menor que zero
        currentResistance = Mathf.Max(currentResistance, 0);

        // Verifica se a resist�ncia chegou a zero
        if (currentResistance == 0)
        {
            Destroy(gameObject); // Destroi o objeto
            TriggerDestructionEffect(); // Dispara o efeito de destrui��o
        }
    }

    private void TriggerDestructionEffect()
    {
        // Instancia o efeito de part�culas na posi��o e rota��o do objeto
        GameObject effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);

        // Destroi o clone do efeito de part�culas ap�s 10 segundos
        Destroy(effectInstance, 10f);
    }
}

