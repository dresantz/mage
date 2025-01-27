using UnityEngine;

public class GrassWaveController : MonoBehaviour
{
    public Material grassMaterial;
    public Transform player;

    void Update()
    {
        if (grassMaterial && player)
        {
            grassMaterial.SetVector("_PlayerPos", player.position);
        }
    }
}
