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
        // Não é diferente de um GetComponent
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
        // isWalking é true se o input não for zero
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


    // Get e Set speed são usados pelo BedTrigger para acessar a a velocidade do player sem alterar sua privacidade.
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
        // Converte o transform.position (em relação ao mundo) em um position em relação a câmera.
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);


        // Verifica se o pessoar está além da barreira esquerda (menor que 0)
        // Mudados o 0 para screenBorder por causa do bug, metade do player ficava para fora da câmera.
        // E se ele ainda está tentando se mover naquela direção
        if ((screenPosition.x < screenBorder && Rigidbody2D.velocity.x < 0) ||
            // Essa parte verifica se o personagem está tentando ir para a direita
                (screenPosition.x > _camera.pixelWidth - screenBorder && Rigidbody2D.velocity.x > 0))
        {
            // Zera a velocidade no eixo X.
            Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        }


        // Verifica se o persnonagem está indo para cima
        if ((screenPosition.y < screenBorder && Rigidbody2D.velocity.y < 0) ||
        // Essa parte verifica se o personagem está tentando ir para baixo
        (screenPosition.y > _camera.pixelHeight - screenBorder && Rigidbody2D.velocity.y > 0))
        {
            // Zera a velocidade no eixo Y.
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfMouse()
    {
        // Obtém a posição do mouse no mundo
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        // Calcula a direção do mouse em relação ao player
        Vector2 directionToMouse = (Vector2)(mouseWorldPosition - transform.position);

        // Calcula o ângulo necessário para olhar na direção do mouse
        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;

        // Define a rotação do Rigidbody2D
        Rigidbody2D.rotation = targetAngle;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
