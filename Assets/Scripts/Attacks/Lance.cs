
public class Lance : AttackBase, IAttack
{
    //Variable that holds how long the lance will stun for
    static float STUN_TIME = 2.0F;

    //Deal damage to the enemy if it is the target, then raise the stun flag.
    public void OnHit(BaseCreature creatureHit)
    {
        if(creatureHit == target)
        {
            if(this != null)
            {
                StatusEffectManager.OnStun(STUN_TIME, creatureHit.GetInstanceID());
                creatureHit.health -= damage;
                AudioManager.audioManInstance.Play("GuardLance");
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
