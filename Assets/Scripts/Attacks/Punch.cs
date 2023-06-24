using UnityEngine;
using System.Collections;

public class Punch : AttackBase, IAttack
{
    //Add OnHit to listen for an enemy hit event, and set the damage to be proper according to the Puncher's current damage modifier.
    private void Awake()
    {
        if (GameManager.gameManInstance.puncherLevel > 1)
        {
            damage += GetComponentInParent<UPuncher>().damageMod;
        }

        transform.localScale /= 3;
        pool = FindObjectOfType<AttackPool>();
        if (GameManager.gameManInstance.puncherLevel == UPuncher.MAX_LEVEL)
        {
            isMaxLevel = true;
        }

        onCreatureHit_ += OnHit;
    }

    //Deal damage to the enemy if it is the target.
    public void OnHit(BaseCreature creatureHit, int instanceID)
    {
        if(instanceID == GetInstanceID())
        {
            if(creatureHit == target)
            {
                if (this != null)
                {
                    AudioManager.audioManInstance.Play("PuncherPunch");
                    Debug.Log(creatureHit.name + "was hit");
                    creatureHit.health -= damage;
                    //If the puncher is max level, cripple the enemy by halving it's spped permanently.
                    if(isMaxLevel)
                    {
                        creatureHit.SetSpeed(creatureHit.GetMaxSpeed() / 2.0f);
                    }
                    pool.Return(gameObject);
                }
            }
        }
    }

    
}
