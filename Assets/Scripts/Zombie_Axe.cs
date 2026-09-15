using UnityEngine;

public class Zombie_Axe : Zombie_Big
{
    [SerializeField]private int maxHealth_zombieAxe;
    protected override void Start()
    {
        base.Start();
        currentHealth = maxHealth_zombieAxe;
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = Random.Range(20, 1000);
        }
    }
    void Update()
    {
        if (player.isAlive == false)
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