using UnityEngine;
using System.Collections;

public class FloatingRock : MonoBehaviour
{
    public float forcePower = 10f;
    public float switchDelay = 2f;
    public float damage = 15f;

    private ConstantForce2D constantForce2d;

    void Start()
    {
        constantForce2d = GetComponent<ConstantForce2D>();

        StartCoroutine(SwitchDirectionLoop());
    }

    IEnumerator SwitchDirectionLoop()
    {
        while (true)
        {
            constantForce2d.force = new Vector2(0f, -forcePower);
            yield return new WaitForSeconds(switchDelay);

            constantForce2d.force = new Vector2(0f, forcePower);
            yield return new WaitForSeconds(switchDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStatusManager.instance.TakeDamage(damage);
        }
    }
}