using System.Collections;
using UnityEngine;

public class Lance : AttackBase, IAttack
{
    //Variable that holds how long the lance will stun for
    public float stunTime = 0.2F;
    //Variable that holds how long the lance will slow for
    static float SLOW_TIME = 2.0F;
    //Variable that holds how much the lance will slow an enemy after they have been stunned.
    public static float SLOW_MODIFIER = 1.5f;

    // Subscribe to OnHit and apply stat changes according to the fuard's modifiers.
    void Awake()
    {
        if (GameManager.gameManInstance.guardLevel > 1)
        {
            stunTime += GetComponentInParent<UGuard>().stunMod;
            damage += GetComponentInParent<UGuard>().damageMod;
        }

        if (GameManager.gameManInstance.guardLevel == UGuard.MAX_LEVEL)
        {
            isMaxLevel = true;
        }
        pool = FindObjectOfType<AttackPool>();
        onCreatureHit_ += OnHit;
    }

    //Deal damage to the enemy if it is the target, then raise the stun flag. If the lance is maxLevel, this will also wait for the stun to be done and then slow the unit.
    public void OnHit(BaseCreature creatureHit, int instanceID)
    {
        if(instanceID == GetInstanceID())
        {
            if(creatureHit == target)
            {
                int enemyInstanceID = creatureHit.GetInstanceID();
                if(this != null)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                    creatureHit.health -= damage;
                    AudioManager.audioManInstance.Play("GuardLance");
                    if(isMaxLevel)
                    {
                        StatusEffectManager.OnSlow(stunTime + SLOW_TIME, enemyInstanceID);
                        GetComponent<SpriteRenderer>().enabled = false;
                    }
                    StatusEffectManager.OnStun(stunTime, enemyInstanceID);
                    pool.Return(gameObject);
                }
            }
        }
    }
}
