using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmission : MonoBehaviour
{
    private int _emissionMultiplayer = Shader.PropertyToID("_EmissionMultiplayer");
    private Material _mat;
    private float _incrementer;

    // Start is called before the first frame update
    private void Awake()
    {
        _mat = GetComponent<ParticleSystemRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _incrementer += Time.deltaTime *20;

        // SetColor se fosse Color
        _mat.SetFloat(_emissionMultiplayer, _incrementer);
    }
}
