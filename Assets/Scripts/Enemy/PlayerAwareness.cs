using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    // Set private pois queremos que apenas essa variavel defina as outras.
    public bool AwareOfPlayer {  get; private set; }

    // Saber a dire��o do jogador
    public Vector2 DirectionToPlayer { get; private set; }

    // Distancia que o inimigo vai avistar o jogador
    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;


    private void Awake()
    {
        // Pega o transform do objeto com o script PlayerMovement
        player = FindObjectOfType<PlayerMovement>().transform;
    }


    void Update()
    {
        // Para verificar se o player est� no alcance
        // Primeiro pegamos o Vector entre o personagem e o inimigo
        // Para isso � s� subtrair o transform.position dos dois
        // Assim sabemos a dist�ncia do jogador e em qual dire��o
        Vector2 enemyToPlayerVector = player.position - transform.position;

        // Normalize arredonda valores.
        // A primeira fun��o nos d� a dire��o do Vector,
        // mas n�s queremos apenas dire��o do jogador, n�o o valor exato.
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance )
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
