using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CombatAttack : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float lifeTime = 1f;
    // this attack's damage
    [SerializeField]
    private int attackDamage = 1;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= lifeTime)
            Destroy(gameObject);

        // TODO : check overlap
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        HealthComponent target;
        Debug.Log(collision.TryGetComponent<HealthComponent>(out target));
        if (collision.TryGetComponent<HealthComponent>(out target))
        {
            target.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}