using UnityEngine;

public class Serthalyn : MonoBehaviour
{
    public Transform player; // O jogador a ser seguido pela fada
    public float followSpeed = 2f; // Velocidade de movimento da fada
    public float rotationSpeed = 5f; // Velocidade de ajuste da rotação da fada
    public Vector2 offset = new Vector2(1f, 1f); // Offset inicial em relação ao jogador
    public float floatAmplitude = 0.5f; // Amplitude do movimento flutuante
    public float floatFrequency = 2f; // Frequência do movimento flutuante

    public float minRandomTime = 2f; // Tempo mínimo entre mudanças de offset
    public float maxRandomTime = 5f; // Tempo máximo entre mudanças de offset

    private Vector2[] possibleOffsets = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    private float nextChangeTime = 0f;

    private Transform currentTarget; // O alvo atual (Player ou Enemy)

    void Start()
    {
        // Inicializa o próximo tempo de mudança do offset
        SetNextChangeTime();
    }

    void Update()
    {
        if (player == null) return;

        // Calcula a posição desejada com o offset atual
        Vector3 desiredPosition = player.position + new Vector3(offset.x, offset.y, 0);

        // Adiciona o movimento flutuante na posição Y
        float floatEffect = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        desiredPosition.y += floatEffect;

        // Calcula a distância atual entre o player e a fada
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se a fada estiver com um inimigo como alvo, faz ela rotacionar em direção ao inimigo
        if (currentTarget != null)
        {
            // Faz a fada olhar na direção do inimigo suavemente
            Vector3 directionToEnemy = currentTarget.position - transform.position;
            directionToEnemy.z = 0; // Garantir que a rotação é apenas em 2D
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToEnemy);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Caso contrário, a fada volta a olhar para o player
            Quaternion targetRotation = player.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move a fada suavemente para a posição desejada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Verifica se é hora de mudar o offset
        if (Time.time >= nextChangeTime)
        {
            ChangeOffset();
        }
    }

    void ChangeOffset()
    {
        // Escolhe um novo offset aleatoriamente
        offset = possibleOffsets[Random.Range(0, possibleOffsets.Length)];

        // Define o próximo tempo de mudança
        SetNextChangeTime();
    }

    void SetNextChangeTime()
    {
        nextChangeTime = Time.time + Random.Range(minRandomTime, maxRandomTime);
    }

    // Detecção de colisão com Enemy usando o componente Enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto tem o script "Enemy" (não mais pela tag)
        if (other.GetComponent<Enemy>() != null)
        {
            currentTarget = other.transform; // Define o inimigo como o alvo
        }
    }

    // Quando o inimigo sai ou é destruído
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
        // Apenas para visualizar o offset e a distância mínima no editor (opcional)
        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(player.position, player.position + new Vector3(offset.x, offset.y, 0));
        }
    }
}
