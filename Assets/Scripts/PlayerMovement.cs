using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float screenBorder;

    private Animator animator;


    private Rigidbody2D Rigidbody2D;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    private Camera _camera;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        // N�o � diferente de um GetComponent
        _camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
        SetAnimation();
    }

    private void SetAnimation()
    {
        // isWalking � true se o input n�o for zero
        bool isWalking = movementInput != Vector2.zero;
        animator.SetBool("isWalking", isWalking);
    }

    private void SetPlayerVelocity()
    {
        //SmoothDamp serve justamente para suavizar a parada
        smoothedMovementInput = Vector2.SmoothDamp(
            smoothedMovementInput,
            movementInput,
            ref movementInputSmoothVelocity,
            0.2f);

        // new Vector2(0f, 0f) * speed;
        // o Smoothed substitui o movementInput
        Rigidbody2D.velocity = smoothedMovementInput * speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        // Converte o transform.position (em rela��o ao mundo) em um position em rela��o a c�mera.
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);


        // Verifica se o pessoar est� al�m da barreira esquerda (menor que 0)
        // Mudados o 0 para screenBorder por causa do bug, metade do player ficava para fora da c�mera.
        // E se ele ainda est� tentando se mover naquela dire��o
        if ((screenPosition.x < screenBorder && Rigidbody2D.velocity.x < 0) ||
            // Essa parte verifica se o personagem est� tentando ir para a direita
                (screenPosition.x > _camera.pixelWidth - screenBorder && Rigidbody2D.velocity.x > 0))
        {
            // Zera a velocidade no eixo X.
            Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        }


        // Verifica se o persnonagem est� indo para cima
        if ((screenPosition.y < screenBorder && Rigidbody2D.velocity.y < 0) ||
        // Essa parte verifica se o personagem est� tentando ir para baixo
        (screenPosition.y > _camera.pixelHeight - screenBorder && Rigidbody2D.velocity.y > 0))
        {
            // Zera a velocidade no eixo Y.
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        // verifica se est� acontecendo um input de movimento
        if (movementInput != Vector2.zero)
        {
            // Essa linha cria um local para se rotacionar.
            // Quaternion � uma variavel espec�fica para guardar rota��es.
            // LookRotation � um m�todo usada para olhar na dire��o desejada.
            // Ela � usada para jogos 3D, por isso usa dois vetores
            // O primeiro vetor � frente e tr�s, mas no 2D n�o usamos frente e tr�s.
            // transform.foward vai travar o frente e tr�s na mesma posi��o.
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);

            // Essa linha faz a rota��o para o local criado na linha anterior.
            // O primeiro pega para onde o personagem est� virado no momento.
            // A segunda vari�vel pega para onde voc� quer virar.
            // A terceira pega a velocidade de rota��o.
            // TdT � usado pois estamos usando f�sica.
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Rigidbody2D.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
