using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float visionRange = 5f;
    public float shootingCooldown = 2f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 5f;
    public float contactDamage = 10f;

    private Transform targetPoint;
    private float shootTimer;

    void Start()
    {
        targetPoint = pointB;
        player = GameObject.FindGameObjectWithTag("Player");
        shootTimer = 0f;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= visionRange)
        {
            FollowPlayer();

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                ShootAtPlayer();
                shootTimer = shootingCooldown;
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }

        if ((targetPoint.position.x - transform.position.x > 0f && transform.localScale.x < 0f) ||
            (targetPoint.position.x - transform.position.x < 0f && transform.localScale.x > 0f))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }


    void FollowPlayer()
    {
        if ((player.transform.position.x - transform.position.x > 0f && transform.localScale.x < 0f) ||
            (player.transform.position.x - transform.position.x < 0f && transform.localScale.x > 0f))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        Vector2 direction = (player.transform.position - shootPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStatusManager.instance.TakeDamage(contactDamage);
        }
    }
}