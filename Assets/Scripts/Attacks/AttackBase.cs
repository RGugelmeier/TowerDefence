using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    //Amount of damage to deal.
    //How fast the attack will travel.
    [SerializeField] protected float damage, speed; 
    protected bool hitEnemy = false; //True if the attack hits an enemy.
    public Transform target; //The target location.

    public static Action<BaseEnemy> onEnemyHit_;
    public static Action onDamageRecieved_;

    //public delegate void OnEnemyHit(BaseEnemy enemyHit);
    //public static event OnEnemyHit onEnemyHit_;

    //Look at the target and set timeAlive so it can be used properly in update.
    private void Awake()
    {
        transform.up = -target.position + transform.position;
    }

    //Move towards the target at a speed determined by the attack's speed. Then check if it has reached it's destination and self destruct if true.
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(transform.position == target.transform.position)
        {
            Destroy(this);
        }
    }

    //Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyCheck = collision.GetComponent<BaseEnemy>();
        //If collides with an enemy, set hitEnemy to true. In the attack's class, OnHit will be run when hitEnemy is true.
        if (enemyCheck != null)
        {
            if(onEnemyHit_ != null)
            {
                onEnemyHit_(enemyCheck);
            }
            if(onDamageRecieved_ != null)
            {
                onDamageRecieved_();
            }
            Destroy(gameObject);
        }
    }
}
