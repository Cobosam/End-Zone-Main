using UnityEngine;

public class Zombie_Axe : Zombie_Big
{
    [SerializeField]private int maxHealth_zombieAxe;
    protected override void Start()
    {
        base.Start();
        currentHealth = maxHealth_zombieAxe;
    }
    void Update()
    {
        CheckPositionX();
        CheckPositionY();
        FindTarget();
    }
}