public class Arrow : AttackBase, IAttack
{
    public void OnHit(BaseCreature creatureHit)
    {
        if(creatureHit == target)
        {
            if(this != null)
            {
                creatureHit.health -= damage;
                pool.Return(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        pool = FindObjectOfType<AttackPool>();
        onCreatureHit_ += OnHit;
    }
}
