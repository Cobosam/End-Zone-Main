using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Zombie_Big : MonoBehaviour, IDamageable
{
    protected Player player;
    protected Barricade barricade;
    protected Rigidbody2D rigidbody2;
    protected CapsuleCollider2D capsuleCollider2;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;

    [Header("Saldýrý Sesleri")]
    [SerializeField] protected AudioClip playerHitSound;
    [SerializeField] protected AudioClip barricadeHitSound;

    [Header("Zombi Ayarlarý")]
    [SerializeField] protected int damage = 50;
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float colorTime = 0.3f;
    [SerializeField] protected float waitTime = 1f;
    [SerializeField] protected float deathTime = 0.8f;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth_zombieBig = 100;
    [SerializeField] Transform attackPoint;

    protected int deathCount = 0;
    protected Color originalColor;
    protected Animator anim2;
    protected bool canMove = true;

    protected virtual void Start()
    {
        currentHealth = maxHealth_zombieBig;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        capsuleCollider2 = GetComponent<CapsuleCollider2D>();
        anim2 = GetComponentInChildren<Animator>();
        player = FindAnyObjectByType<Player>();
        barricade = FindAnyObjectByType<Barricade>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if(player.isAlive==false)
        {
            this.enabled = false;
            capsuleCollider2.enabled = false;
            anim2.SetBool("isAttacking", false);
            return;
        }

        bool isTargetInSight = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer) != null;

        if (isTargetInSight)
        {
            Attack();
        }
        else
        {
            if (canMove)
            {
                anim2.SetBool("isAttacking", false);
                CheckPositionY();
                CheckPositionX();
            }
            else
            {
                return;
            }
        } 
    }

    protected void CheckPositionX()
    {
        if (player == null) return;

        Vector3 currentScale = transform.localScale;
        if (player.isAlive)
        {
            if (player.transform.position.x > transform.position.x)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
                transform.localScale = currentScale;
                rigidbody2.linearVelocity = new Vector2(moveSpeed, rigidbody2.linearVelocity.y);
                anim2.SetFloat("xVelocity", Mathf.Abs(rigidbody2.linearVelocity.x));
            }
            else if (player.transform.position.x < transform.position.x)
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
                transform.localScale = currentScale;
                rigidbody2.linearVelocity = new Vector2(-moveSpeed, rigidbody2.linearVelocity.y);
                anim2.SetFloat("xVelocity", Mathf.Abs(rigidbody2.linearVelocity.x));
            }
            else
            {
                rigidbody2.linearVelocity = new Vector2(0, rigidbody2.linearVelocity.y);
            }
        }
        else
        {
            return;
        }

    }

    protected void CheckPositionY()
    {
        if (player == null) return;

        if (player.isAlive)
        {
            if (player.transform.position.y > transform.position.y)
            {
                rigidbody2.linearVelocity = new Vector2(rigidbody2.linearVelocity.x, moveSpeed);
            }
            else if (player.transform.position.y < transform.position.y)
            {
                rigidbody2.linearVelocity = new Vector2(rigidbody2.linearVelocity.x, -moveSpeed);
            }
            else
            {
                rigidbody2.linearVelocity = new Vector2(rigidbody2.linearVelocity.x, 0);
            }
        }
        else
        {
            return;
        }
        
    }

    protected void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    protected void FindTarget()
    {
        Collider2D targetInSight = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);
        if (targetInSight != null)
        {
            Attack();
        }
    }

    protected void Attack()
    {
        rigidbody2.linearVelocity = new Vector2(0, 0);
        anim2.SetFloat("xVelocity", 0f);
        anim2.SetBool("isAttacking", true);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(RedStand());
        currentHealth -= damage;
        Debug.Log(gameObject.name + " hasar aldý! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        LevelManager levelManager = FindAnyObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.ZombiOlduruldu();
        }
        deathCount++;
        StartCoroutine(DeathBody());
    }

    protected IEnumerator RedStand()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(colorTime);
        spriteRenderer.color = originalColor;
    }

    protected IEnumerator DeathBody()
    {
        this.enabled = false;
        capsuleCollider2.enabled = false;
        rigidbody2.linearVelocity = new Vector2(0, 0);
        anim2.SetTrigger("isDeath");
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    public virtual void DealDamage()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayer);

        foreach (Collider2D target in hitTargets)
        {
            IDamageable damageableTarget = target.GetComponent<IDamageable>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(damage);
            }
        }
    }

    public virtual void EnableMovement()
    {
        canMove = true;
    }

    public virtual void DisableMovement()
    {
        canMove = false;
    }

    public virtual void DamageSound()
    {
        // Menzildeki hedefi bul
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);

        if (hit != null)
        {
            // Vurduðu objenin etiketine göre sesi çal
            if (hit.CompareTag("Player") && playerHitSound != null)
            {
                audioSource.PlayOneShot(playerHitSound);
            }
            else if (hit.CompareTag("Barricade") && barricadeHitSound != null)
            {
                audioSource.PlayOneShot(barricadeHitSound);
            }
        }
    }
}
