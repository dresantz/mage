using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMovableEntity
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

    private Vector2 moveDirection;
    private Vector2 mousePosition;

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
        RotateInDirectionOfMouse();
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


    // Get e Set speed s�o usados pelo BedTrigger para acessar a a velocidade do player sem alterar sua privacidade.
    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public Transform GetTransform()
    {
        return transform;
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

    private void RotateInDirectionOfMouse()
    {
        // Obt�m a posi��o do mouse no mundo
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        // Calcula a dire��o do mouse em rela��o ao player
        Vector2 directionToMouse = (Vector2)(mouseWorldPosition - transform.position);

        // Calcula o �ngulo necess�rio para olhar na dire��o do mouse
        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;

        // Define a rota��o do Rigidbody2D
        Rigidbody2D.rotation = targetAngle;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
