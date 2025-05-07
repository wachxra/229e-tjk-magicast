using UnityEngine;

public class PendulumDamage : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStatusManager.instance.TakeDamage(damage);
        }
    }
}