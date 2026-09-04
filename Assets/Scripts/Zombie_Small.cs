using UnityEngine;

public class Zombie_Small : Zombie_Big
{
    [SerializeField] private int maxHealth_zombieSmall;
    protected override void Start()
    {
        base.Start();
        currentHealth = maxHealth_zombieSmall;
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = Random.Range(1, 1000);
        }
    }
    void Update()
    {
        if(player.isAlive==false)
        {
            rigidbody2.linearVelocity = new Vector2(0, 0);
            anim2.SetBool("isAttacking", false);
            anim2.SetFloat("xVelocity", 0f);
            this.enabled = false;
            return;
        }
        CheckPositionX();
        CheckPositionY();
        FindTarget();
    }
}
