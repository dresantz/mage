using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform wandOffset;

    [SerializeField]
    private float delayBetweenShoots;


    [SerializeField]
    private float bulletSpeed;

    private bool fireContinuously;
    // Feito para corrigir um bug que se voc� aperta duas vezes e segura na segunda,
    // Ele s� dispara uma vez e para pois o segundo clique veio antes do delay acabar.
    private bool fireSingle;
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireContinuously || fireSingle)
        {
            // Calcula a quantidade de tempo que passou desde o �ltimo disparo
            float timeSinceLastFire = Time.time - lastFireTime;

            // Se o tempo do �ltimo disparo for maior ou igual ao delay, ent�o pode disparar novamente
            if (timeSinceLastFire >= delayBetweenShoots)
            {
                FireBullet();
                // Pega o momento atual do �ltimo disparo
                lastFireTime = Time.time;
                fireSingle = false;
            }

        }
    }

    private void FireBullet()
    {
        // Cria um proj�til na posi��o do Offset
        GameObject bullet = Instantiate(bulletPrefab, wandOffset.position, transform.rotation);
        // Faz o proj�til andar
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // A velocidade do proj�til multiplicada pela dire��o que o personagem est� apontando(up)
        rb.velocity = bulletSpeed * transform.up;
    }

    // InputValue � o novo sistema de Input
    private void OnFire(InputValue inputValue)
    {
        fireContinuously = inputValue.isPressed;

        //Verifica se o bot�o foi apertado
        if (inputValue.isPressed)
        {
            fireSingle = true;
        }
    }
}
