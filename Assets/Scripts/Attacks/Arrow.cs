public class Arrow : AttackBase, IAttack
{
    public void OnHit(BaseEnemy enemyHit)
    {
        if(enemyHit == target)
        {
            if(this != null)
            {
                enemyHit.health -= damage;
                pool.Return(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        pool = FindObjectOfType<AttackPool>();
        onEnemyHit_ += OnHit;
    }
}
