using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : AttackBase, IAttack
{
    //Deal damage to the enemy.
    public void OnHit(BaseEnemy enemyHit)
    {
        enemyHit.health -= damage;
    }

    //Add OnHit to listen for an enemy hit event.
    void Start()
    {
        onEnemyHit_ += OnHit;
    }
}
