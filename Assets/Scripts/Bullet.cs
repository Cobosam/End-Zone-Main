using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mermiHizi = 20f;
    public int damage = 20;
    private Rigidbody2D rb;
    private Player player;
    [SerializeField]private LayerMask targetLayer;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            damage += player.ekstraMermiGucu;
        }
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * mermiHizi;
        Destroy(gameObject, 1f);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Zombie"))
        {
            IDamageable damageableTarget = collision.GetComponent<IDamageable>();

            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Barricade") || collision.CompareTag("Healing"))
        {
            return;
        }
    }
}
