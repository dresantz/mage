using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFamage : MonoBehaviour
{
    [SerializeField]
    private float damageAmount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.GetComponent<Enemy>())
        {
            var healtController = collision.gameObject.GetComponent<HealthController>();
            // Manda para a função TakeDamage o damageAmount
            healtController.TakeDamage(damageAmount);
        }
    }
}
