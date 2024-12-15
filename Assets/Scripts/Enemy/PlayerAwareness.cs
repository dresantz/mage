using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    // Set private pois queremos que apenas essa variavel defina as outras.
    public bool AwareOfPlayer {  get; private set; }

    // Saber a direção do jogador
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
        // Para verificar se o player está no alcance
        // Primeiro pegamos o Vector entre o personagem e o inimigo
        // Para isso é só subtrair o transform.position dos dois
        // Assim sabemos a distância do jogador e em qual direção
        Vector2 enemyToPlayerVector = player.position - transform.position;

        // Normalize arredonda valores.
        // A primeira função nos dá a direção do Vector,
        // mas nós queremos apenas direção do jogador, não o valor exato.
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
