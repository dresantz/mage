using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resistência inicial máxima
    private int currentResistance;

    public GameObject destructionEffect; // Prefab do sistema de partículas para destruição (opcional)
    private Animator animator; // Referência ao Animator (opcional)
    public float deleyDestroy;
    private Collider2D objectCollider; // Referência ao Collider do objeto

    public bool isChangeble;
    public PicaxeSpawner picaxeSpawner;
    private bool picaxeHasSpawn;

    private void Start()
    {
        // Define a resistência inicial
        currentResistance = maxResistance;

        // Tenta obter o componente Animator no objeto
        animator = GetComponent<Animator>();
        objectCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resistência pelo dano recebido

        // Garante que a resistência nunca seja menor que zero
        currentResistance = Mathf.Max(currentResistance, 0);

        // Verifica se a resistência chegou a zero
        if (currentResistance == 0)
        {
            DisableCollider();
            TriggerDestructionEffect(); // Dispara o efeito de destruição, se existir
            TriggerDestructionAnimation(); // Dispara a animação de destruição, se existir
            SpawnObject();

            if (!isChangeble)
            {
                Destroy(gameObject, deleyDestroy); // Destroi o objeto
            }
        }
    }

    private void DisableCollider()
    {
        if (objectCollider != null && !isChangeble)
        {
            objectCollider.enabled = false; // Desativa o Collider2D
        }
    }

    private void TriggerDestructionEffect()
    {
        if (destructionEffect != null) // Verifica se o prefab foi atribuído
        {
            // Instancia o efeito de partículas na posição e rotação do objeto
            GameObject effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);

            // Destroi o clone do efeito de partículas após 10 segundos
            Destroy(effectInstance, 10f);
        }
    }

    private void SpawnObject()
    {
        if (picaxeSpawner != null && !picaxeHasSpawn)
        {
            picaxeSpawner.SpawnPicaxeInAllSpawners(); // Chama a função do script PicaxeSpawner
            picaxeHasSpawn = true;
        }
    }

    private void TriggerDestructionAnimation()
    {
        if (animator != null && animator.HasParameter("isDestroyed"))
        {
            // Ativa o trigger da animação "isDestroyed"
            animator.SetTrigger("isDestroyed");
        }

        if (animator != null && animator.HasParameter("hasChange"))
        {
            // Ativa o trigger da animação "hasChange"
            animator.SetTrigger("hasChange");
        }
    }
}

public static class AnimatorExtensions
{
    // Método de extensão para verificar se o Animator possui um parâmetro específico
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}
