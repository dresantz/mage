using UnityEngine;

public class UpdatePlayerPosition : MonoBehaviour
{
    public Material grassMaterial; // Material da grama
    public Transform player; // Transform do jogador

    void Update()
    {
        if (grassMaterial != null && player != null)
        {
            // Atualiza a posição do jogador no shader (apenas X e Y)
            grassMaterial.SetVector("_PlayerPos", new Vector2(player.position.x, player.position.y));
        }
    }
}