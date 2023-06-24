public class Arrow : AttackBase, IAttack
{
    public void OnHit(BaseCreature creatureHit, int InstanceID)
    {
        //If the archer is max level, do not return the arrow to the attack pool (therefore destroying it).
        //It will return itself once it has been alive for it's max live time.
        //It will also damage all enemies hit, not just the original target.
        if(InstanceID == GetInstanceID())
        {
            if (isMaxLevel)
            {
                if(creatureHit.GetComponent<BaseEnemy>())
                {
                    creatureHit.health -= damage;
                }
            }
            //If the archer is not max level we will get here. This only damages the target and returns the objecxt to the attack pool, therefore deactivating it.
            else if(creatureHit == target)
            {
                if(this != null)
                {
                    creatureHit.health -= damage;
                    pool.Return(gameObject);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.gameManInstance.archerLevel == UArcher.MAX_LEVEL)
        {
            isMaxLevel = true;
        }
        pool = FindObjectOfType<AttackPool>();
        onCreatureHit_ += OnHit;
    }
}
