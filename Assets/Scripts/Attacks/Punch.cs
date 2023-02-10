using UnityEngine;

public class Punch : AttackBase, IAttack
{
    //Deal damage to the enemy if it is the target.
    public void OnHit(BaseCreature creatureHit)
    {
        if(creatureHit == target)
        {
            if (this != null)
            {
                Debug.Log(creatureHit.name + "was hit");
                creatureHit.health -= damage;
                pool.Return(gameObject);
            }
        }
    }

    //Add OnHit to listen for an enemy hit event.
    private void Awake()
    {
        transform.localScale /= 3;
        pool = FindObjectOfType<AttackPool>();
        onCreatureHit_ += OnHit;
    }
}
