using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resistência inicial máxima
    private int currentResistance;

    public GameObject destructionEffect; // Prefab do sistema de partículas para destruição

    private void Start()
    {
        // Define a resistência inicial
        currentResistance = maxResistance;
    }

    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resistência pelo dano recebido

        // Garante que a resistência nunca seja menor que zero
        currentResistance = Mathf.Max(currentResistance, 0);

        // Verifica se a resistência chegou a zero
        if (currentResistance == 0)
        {
            Destroy(gameObject); // Destroi o objeto
            TriggerDestructionEffect(); // Dispara o efeito de destruição
        }
    }

    private void TriggerDestructionEffect()
    {
        // Instancia o efeito de partículas na posição e rotação do objeto
        GameObject effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);

        // Destroi o clone do efeito de partículas após 10 segundos
        Destroy(effectInstance, 10f);
    }
}

