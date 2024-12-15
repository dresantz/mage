using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float damageAmount;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var healtController = collision.gameObject.GetComponent<HealthController>();

            // Manda para a função TakeDamage o damageAmount
            healtController.TakeDamage(damageAmount);
        }
    }
}
