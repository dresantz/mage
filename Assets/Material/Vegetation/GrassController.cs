using UnityEngine;

public class GrassShaderController : MonoBehaviour
{
    public Transform player;
    private Material grassMaterial;

    void Start()
    {
        grassMaterial = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (grassMaterial != null && player != null)
        {
            Vector2 playerPos = player.position;
            grassMaterial.SetVector("_PlayerPosition", playerPos);
        }
    }
}