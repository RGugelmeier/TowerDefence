using System;
using System.Collections;
using UnityEngine;

//* ABSTRACT ENEMY CLASS *//
//This class is what all enemies will inherit from.
//It includes functionality for initialization and moving.

public abstract class BaseEnemy : BaseCreature
{
    [SerializeField]
    protected float damage;    //The unit's stats.
    public float worth;

    //The enemy's id. This is used by the pool to keep track of which enemy to reactivate when reactivate is called. Otherwise it resets each enemy,
    //spawning them all at the start of the path and resetting their hp.
    public int id;

    public int nextNode;                 //The next node that the unit has to move to. Changed whenever it reaches it's current value.

    public GameObject[] moveToNodes;     //Array of nodes that the unit will have to move to.

    public static Action<GameObject> OnDie; //Event raised when I die.
    public static Action<GameObject> OnReachedEnd; //Event raised when I reach the end of the path.

    protected UnitPool unitPool;  //The object pool that is used to create new units. We need that here so enemies can return units to the pool if need be.

    new private void Awake()
    {
        base.Awake();

        //Get a reference to the unit pool.
        unitPool = FindObjectOfType<UnitPool>();

        moveSpeed = maxMoveSpeed;

        //Get all positions that the enemies will need to move to.
        moveToNodes = GameObject.FindGameObjectsWithTag("RotateNode");
        //Set initial position.
        transform.Translate(moveToNodes[0].transform.position);
        //Set the first node to start the enemy moving.
        nextNode = 0;        

        AttackBase.onDamageRecieved_ += HealthCheck;
        ObjectPool.OnActivate += OnReactivate;
        
    }

    //This function will be called whenever the enemy is reactivated to be used again by the EnemyPool.
    public void OnReactivate(GameObject obj)
    {
        if(obj != null)
        {
            if(this != null)
            {
                if(obj.GetInstanceID() == gameObject.GetInstanceID())
                {
                    //Reset the health and move speed to be full.
                    health = maxHealth;
                    moveSpeed = maxMoveSpeed;
                    //Set initial position.
                    transform.position = moveToNodes[0].transform.position;
                    //Set the first node to start the enemy moving.
                    nextNode = 0;
                }
            }
        }
    }

    private void Update()
    {
        //Updates the attack timer if an attack has not been readied.
        if(attackTimer < attackInterval)
        {
            attackTimer += Time.deltaTime;
        }
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        //Move to the next node.
        if(moveSpeed > 0.0f)
        {
            MoveToNode(moveToNodes[nextNode]);
        }
    }

    //Move toward the next node. Speed is determined by moveSpeed variable.
    void MoveToNode(GameObject node)
    {
        if(moveSpeed > 0.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, node.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    //Do stuff when an enemy reaches the end of the path. Enemy classes can override this to add their own functionality.
    //For ex. a fairy will stun all units for .15 seconds when it reaches the end.
    public virtual void ReachedEnd()
    {
        //Deal damage to player's health.
        gameMan.health -= damage;
        gameMan.aliveEnemies.Remove(this);

        //Raise OnReachedEnd event. This checks if the player's health in the game manager is <= zero. It also updates the health bar in ProgressBar
        if (OnReachedEnd != null)
            OnReachedEnd(gameObject);

        //Destroy self.
        gameObject.SetActive(false);
    }

    //Called when hit by an attack. Checks health and dies if health dropped below zero.
    public void HealthCheck()
    {
        if(health <= 0.0f)
        {
            if(this != null && gameObject.activeInHierarchy)
            {
                if (OnDie != null)
                {
                    OnDie(gameObject);
                }
            }
        }
    }

    //Instantiate the attack, then reset the attack timer to start counting to another attack being prepared.
    void LaunchAttack(UnitBase target_)
    {
        if (attack != null)
        {
            createdAttack = attackPool.CreateNew(attack, transform.position, gameObject);
            attackObj = createdAttack.GetComponent<AttackBase>();
            attackObj.target = target_;
        }
        else
        {
            Debug.LogWarning("A unit called: " + gameObject.name + " has no attack, but tried to attack an enemy!");
        }
        attackTimer = 0.0f;
    }

    //Collision handling. Used to check for enemies in range to attack.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //If the unit's circle collider is collided with an enemy.
        if (collision.gameObject.GetComponent<UnitBase>() != null && collision.gameObject.GetComponent<UnitBase>().isActive && collision.gameObject.name == "hitBox")
        {
            //Set the attack's target.
            if (attackObj)
            {
                attackObj.target = collision.GetComponent<UnitBase>();
            }
            //Attack if the attack timer allows it, then reset the attack timer.
            if (attackTimer >= attackInterval)
            {
                LaunchAttack(collision.GetComponent<UnitBase>());
            }
        }
    }
}
