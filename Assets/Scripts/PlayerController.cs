using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpPower = 15f;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioManager audioManager;

    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;
    public GameObject deathPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = FindFirstObjectByType<AudioManager>();

        alive = true;

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    private void Update()
    {
        Restart();
        if (alive)
        {
            Run();
            Jump();
            Attack();
            UsePotions();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);

        if (other.CompareTag("Enemy") && alive)
        {
            anim.SetTrigger("hurt");
            PlayerStatusManager.instance.TakeDamage(10f);

            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);

            audioManager.PlayHurt();
        }
    }

    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            audioManager.PlayWalk();

            direction = -1;
            moveVelocity = Vector3.left;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;

            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            audioManager.PlayWalk();

            direction = 1;
            moveVelocity = Vector3.right;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;

            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);

            audioManager.PlayJump();
        }
        if (!isJumping)
        {
            return;
        }

        rb.linearVelocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");

            audioManager.PlayHit();
        }
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
            Time.timeScale = 1f;
            if (deathPanel != null)
            {
                deathPanel.SetActive(false);
            }

            audioManager.PlayWalk();
        }
    }

    void UsePotions()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerStatusManager.instance.currentHP < PlayerStatusManager.instance.maxHP)
            {
                if (InventoryManager.instance.UseItem(ItemType.HP_POTION))
                {
                    anim.SetTrigger("attack");

                    audioManager.PlayPotion();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerStatusManager.instance.currentMana < PlayerStatusManager.instance.maxMana)
            {
                if (InventoryManager.instance.UseItem(ItemType.MANA_POTION))
                {
                    anim.SetTrigger("attack");

                    audioManager.PlayManaPotion();
                }
            }
        }
    }

    public void HandleDeath()
    {
        if (alive)
        {
            alive = false;
            anim.SetTrigger("die");

            audioManager.PlayDie();

            if (deathPanel != null)
            {
                deathPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}