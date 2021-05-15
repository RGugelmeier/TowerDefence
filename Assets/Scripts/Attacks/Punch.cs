﻿public class Punch : AttackBase, IAttack
{
    //Deal damage to the enemy if it is the target.
    public void OnHit(BaseEnemy enemyHit)
    {
        if(enemyHit == target)
        {
            if (this != null)
            {
                enemyHit.health -= damage;
                Destroy(gameObject);
            }
        }
    }

    //Add OnHit to listen for an enemy hit event.
    void Start()
    {
        onEnemyHit_ += OnHit;
    }
}
