using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatusManager.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground")/* || collision.CompareTag("Wall")*/)
        {
            Destroy(gameObject);
        }
    }
}