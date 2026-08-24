using UnityEngine;

public class Zombie_Small : Zombie_Big
{
    [SerializeField] private int maxHealth_zombieSmall;
    protected override void Start()
    {
        base.Start();
        currentHealth = maxHealth_zombieSmall;
    }
    void Update()
    {
        CheckPositionX();
        CheckPositionY();
        FindTarget();
    }
}
