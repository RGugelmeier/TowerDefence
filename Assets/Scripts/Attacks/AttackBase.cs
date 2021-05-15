using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    //Amount of damage to deal.
    //How fast the attack will travel.
    [SerializeField] protected float damage, speed; 
    protected bool hitEnemy = false; //True if the attack hits an enemy.
    public BaseEnemy target; //The target location.

    //Reference the attack's collision box.
    [SerializeField] BoxCollider2D ignoreCollision;
    BoxCollider2D collision;

    public static Action<BaseEnemy> onEnemyHit_;
    public static Action onDamageRecieved_;

    //public delegate void OnEnemyHit(BaseEnemy enemyHit);
    //public static event OnEnemyHit onEnemyHit_;

    //Look at the target and set timeAlive so it can be used properly in update.
    private void Awake()
    {
        //Set the box to ignore.
        collision = GetComponent<BoxCollider2D>();
        ignoreCollision = GameObject.Find("Spot Holder(Clone)").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collision, ignoreCollision, true);
        transform.up = -target.transform.position + transform.position;
    }

    //Move towards the target at a speed determined by the attack's speed. Then check if it has reached it's destination and self destruct if true.
    private void FixedUpdate()
    {
        if(target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Destroy(gameObject);
            //gameObject.SetActive(false); //TODO I need to make an attakc pool so this wont crash the game when an attack hitsa an enemy who has already been hit.
            //Things to test: Give an enemy health to be hit multiple times before dying to see if it crashes...
            //
        }
    }

    //Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyCheck = collision.GetComponent<BaseEnemy>();

        //If I collide with an enemy, set hitEnemy to true. In the attack's class, OnHit will be run when hitEnemy is true.
        if (enemyCheck != null)
        {
            if (enemyCheck.gameObject.activeInHierarchy)
            {
                if (onEnemyHit_ != null)
                {
                    onEnemyHit_(enemyCheck);
                }
                if(onDamageRecieved_ != null)
                {
                    onDamageRecieved_();
                }
            }
        }
    }
}
