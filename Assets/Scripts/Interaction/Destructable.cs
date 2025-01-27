using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxResistance = 20; // Resist�ncia inicial m�xima
    private int currentResistance;

    public GameObject destructionEffect; // Prefab do sistema de part�culas para destrui��o (opcional)
    private Animator animator; // Refer�ncia ao Animator (opcional)
    public float deleyDestroy;
    private Collider2D objectCollider; // Refer�ncia ao Collider do objeto

    public bool isChangeble;
    public PicaxeSpawner picaxeSpawner;
    private bool picaxeHasSpawn;

    private void Start()
    {
        // Define a resist�ncia inicial
        currentResistance = maxResistance;

        // Tenta obter o componente Animator no objeto
        animator = GetComponent<Animator>();
        objectCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentResistance -= damage; // Reduz a resist�ncia pelo dano recebido

        // Garante que a resist�ncia nunca seja menor que zero
        currentResistance = Mathf.Max(currentResistance, 0);

        // Verifica se a resist�ncia chegou a zero
        if (currentResistance == 0)
        {
            DisableCollider();
            TriggerDestructionEffect(); // Dispara o efeito de destrui��o, se existir
            TriggerDestructionAnimation(); // Dispara a anima��o de destrui��o, se existir
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
        if (destructionEffect != null) // Verifica se o prefab foi atribu�do
        {
            // Instancia o efeito de part�culas na posi��o e rota��o do objeto
            GameObject effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);

            // Destroi o clone do efeito de part�culas ap�s 10 segundos
            Destroy(effectInstance, 10f);
        }
    }

    private void SpawnObject()
    {
        if (picaxeSpawner != null && !picaxeHasSpawn)
        {
            picaxeSpawner.SpawnPicaxeInAllSpawners(); // Chama a fun��o do script PicaxeSpawner
            picaxeHasSpawn = true;
        }
    }

    private void TriggerDestructionAnimation()
    {
        if (animator != null && animator.HasParameter("isDestroyed"))
        {
            // Ativa o trigger da anima��o "isDestroyed"
            animator.SetTrigger("isDestroyed");
        }

        if (animator != null && animator.HasParameter("hasChange"))
        {
            // Ativa o trigger da anima��o "hasChange"
            animator.SetTrigger("hasChange");
        }
    }
}

public static class AnimatorExtensions
{
    // M�todo de extens�o para verificar se o Animator possui um par�metro espec�fico
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
