using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    //Colocamos -30 para que o inimigo consiga sair 30 Pixels e voltar depois.
    [SerializeField]
    private float screenBorder;
    
    // Sobre o CircleCast
    [SerializeField]
    private float obstacleCheckCircleRadius;

    [SerializeField]
    private float obstacleCheckDistance;

    [SerializeField]
    private LayerMask obstacleLayerMask;

    private RaycastHit2D[] obstacleCollisions;

    // Usado para evitar que mais de um obst�culo fa�a com que o Enemy fique preso
    private float obstacleAvoidanceCooldown;

    private Vector2 obstacleAvoidanceTargetDirection;

    private Rigidbody2D Rigidbody2D;

    private PlayerAwareness playerAwareness;

    private Vector2 targetDirection;

    private float changeDirectionCooldown;

    private Camera _camera;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
        targetDirection = transform.up;
        _camera = Camera.main;

        // [] guarda o resultado desse n�mero de colis�es 
        obstacleCollisions = new RaycastHit2D[10];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();

        // Precisa ser chamado logo depois de encontrar o jogador
        HandleObstacles();
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange()
    {
        // Acredito que seja negativo pois � um CoolDown
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            // O segundo par�metro � a dire��o que aponta o eixo, no caso, o eixo foward aponta para a tela
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            // Rotaciona para a posi��o determinada pelos de cima
            targetDirection = rotation * targetDirection;


            changeDirectionCooldown = Random.Range(1f, 4f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen()
    {
        // Converte o transform.position (em rela��o ao mundo) em um position em rela��o a c�mera.
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);


        // Substitui o Rigidbody2D.velocity.x por targetPosition
        // Essa parte verifica se o personagem est� tentando ir para a esquerda
        if ((screenPosition.x < screenBorder && targetDirection.x < 0) ||
                // Essa parte verifica se o personagem est� tentando ir para a direita
                (screenPosition.x > _camera.pixelWidth - screenBorder && targetDirection.x > 0))
        {
            // Em vez de zerar a velocidade no eixo X, a gente reverte colocando "-".
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y);
        }


        // Verifica se o persnonagem est� indo para cima
        if ((screenPosition.y < screenBorder && targetDirection.y < 0) ||
        // Essa parte verifica se o personagem est� tentando ir para baixo
        (screenPosition.y > _camera.pixelHeight - screenBorder && targetDirection.y > 0))
        {
            // Reverte a velocidade no eixo Y.
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y);
        }
    }

    private void HandleObstacles()
    {
        obstacleAvoidanceCooldown -= Time.deltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(
            transform.position, 
            obstacleCheckCircleRadius, 
            transform.up, 
            contactFilter, 
            obstacleCollisions, 
            obstacleCheckDistance);

        for (int index = 0; index < numberOfCollisions; index++)
        {
            var obstacleCollision = obstacleCollisions[index];

            if (obstacleCollision.collider.gameObject == gameObject)
            {
                continue; //  para o pr�ximo alvo no loop
            }

            if (obstacleAvoidanceCooldown <= 0)
            {
                obstacleAvoidanceTargetDirection = obstacleCollision.normal;
                obstacleAvoidanceCooldown = 0.3f;
            }

            // Cria uma rota��o m�xima para o inimigo n�o virar na dire��o oposta do obst�culo
            // substituimos o obstacleCollision.normal por 
            var targetRotation = Quaternion.LookRotation(transform.forward, obstacleAvoidanceTargetDirection);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // normal faz o Enemy virar na dire��o oposta da colis�o
            // targetDirection = obstacleCollision.normal;
            targetDirection = rotation * Vector2.up;

            break; // sai do loop
        }
    }

    private void RotateTowardsTarget()
    {

        //Semelhante ao que foi feito com o jogador.
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Rigidbody2D.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        Rigidbody2D.velocity = transform.up * speed;
    }
}
