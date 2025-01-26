using UnityEngine;

public class Serthalyn : MonoBehaviour
{
    public Transform player; // O jogador a ser seguido pela fada
    public float followSpeed = 2f; // Velocidade de movimento da fada
    public float rotationSpeed = 5f; // Velocidade de ajuste da rota��o da fada
    public Vector2 offset = new Vector2(1f, 1f); // Offset inicial em rela��o ao jogador
    public float floatAmplitude = 0.5f; // Amplitude do movimento flutuante
    public float floatFrequency = 2f; // Frequ�ncia do movimento flutuante

    public float minRandomTime = 2f; // Tempo m�nimo entre mudan�as de offset
    public float maxRandomTime = 5f; // Tempo m�ximo entre mudan�as de offset

    private Vector2[] possibleOffsets = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    private float nextChangeTime = 0f;

    private Transform currentTarget; // O alvo atual (Player ou Enemy)

    void Start()
    {
        // Inicializa o pr�ximo tempo de mudan�a do offset
        SetNextChangeTime();
    }

    void Update()
    {
        if (player == null) return;

        // Calcula a posi��o desejada com o offset atual
        Vector3 desiredPosition = player.position + new Vector3(offset.x, offset.y, 0);

        // Adiciona o movimento flutuante na posi��o Y
        float floatEffect = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        desiredPosition.y += floatEffect;

        // Calcula a dist�ncia atual entre o player e a fada
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se a fada estiver com um inimigo como alvo, faz ela rotacionar em dire��o ao inimigo
        if (currentTarget != null)
        {
            // Faz a fada olhar na dire��o do inimigo suavemente
            Vector3 directionToEnemy = currentTarget.position - transform.position;
            directionToEnemy.z = 0; // Garantir que a rota��o � apenas em 2D
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToEnemy);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Caso contr�rio, a fada volta a olhar para o player
            Quaternion targetRotation = player.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move a fada suavemente para a posi��o desejada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Verifica se � hora de mudar o offset
        if (Time.time >= nextChangeTime)
        {
            ChangeOffset();
        }
    }

    void ChangeOffset()
    {
        // Escolhe um novo offset aleatoriamente
        offset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];

        // Define o pr�ximo tempo de mudan�a
        SetNextChangeTime();
    }

    void SetNextChangeTime()
    {
        nextChangeTime = Time.time + Random.Range(minRandomTime, maxRandomTime);
    }

    // Detec��o de colis�o com Enemy usando o componente Enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto tem o script "Enemy" (n�o mais pela tag)
        if (other.GetComponent<Enemy>() != null)
        {
            currentTarget = other.transform; // Define o inimigo como o alvo
        }
    }

    // Quando o inimigo sai ou � destru�do
    void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto tem o script "Enemy"
        if (other.GetComponent<Enemy>() != null)
        {
            currentTarget = null; // Remove o inimigo como o alvo
        }
    }

    void OnDrawGizmos()
    {
        // Apenas para visualizar o offset e a dist�ncia m�nima no editor (opcional)
        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(player.position, player.position + new Vector3(offset.x, offset.y, 0));
        }
    }
}
