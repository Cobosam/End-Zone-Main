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
        rb = GetComponent<Rigidbody2D>();
        // Mermiyi kendi baktýðý yöne (saða) doðru fýrlat. 
        // linearVelocity kullanýyoruz çünkü sen yeni Unity sürümündesin!
        rb.linearVelocity = transform.right * mermiHizi;

        // Ekranda sonsuza kadar uçup oyunu kastýrmamasý için mermiyi 1 saniye sonra yok et
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
        else if (collision.CompareTag("Ground") || collision.CompareTag("Barricade"))
        {
            return;
        }
    }
}
