using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature : MonoBehaviour
{
    [SerializeField] protected float attackInterval, attackRange;   //Creature's stats.
    public float maxHealth, health;

    [SerializeField]
    protected GameObject attack;                //The attack prefab that the creature will perform.
    protected AttackBase attackObj;             //The attack object that will be instantiated.
    protected GameObject createdAttack;         //The attack that has been created.

    protected float attackTimer;                //Time handle that determines when the enemy can attack.

    [SerializeField]
    protected float maxMoveSpeed, moveSpeed;    

    protected GameManager gameMan;              //Reference to the game manager. Used to check and affect game stats.

    protected AttackPool attackPool;            //Reference to the attack pool that the enemy will use for their attacks.

    [SerializeField]
    protected bool canAttack;                   //Determines if the unit can currently attack.
    public bool canTakeDamage;               //Determines if the creature can currently take damage. Will change for things like an Ent's ability.

    protected CircleCollider2D enemyDetection;  //Enemy collision detection.

    [SerializeField]
    private GameObject FXAnimManager;
    private GameObject FXAnimObj;
    public Animator FXAnimator;

    protected void Awake()
    {
        //Set what functions run when a status effect is applied via StatusEffectManager.
        StatusEffectManager.OnStun += OnStun;
        StatusEffectManager.OnShield += OnShield;

        //Set game manager reference.
        gameMan = FindObjectOfType<GameManager>();
        attackPool = GetComponent<AttackPool>();

        //Initialize attack timer.
        attackTimer = attackInterval;


        FXAnimObj = Instantiate(FXAnimManager, transform);
        FXAnimator = FXAnimObj.GetComponent<Animator>();

        //If the enemy can attack, get it's circle collider component to use as their detection radius.
        if(canAttack)
        {
            enemyDetection = GetComponent<CircleCollider2D>();

            //Set enemy detection range.
            if (enemyDetection)
            {
                enemyDetection.radius = attackRange;
            }
            else
            {
                Debug.LogWarning("A unit called: " + gameObject.name + " has no enemy detection circle collider!");
            }
        }
    }

    //This function stuns the enemy for a time dictated by the time_ variable passed in.
    protected void OnStun(float time_, int enemyID)
    {
        
        //This keeps track of if the character being stunned has ther ability to attack. If so, This will prevent them from attacking until the stun is done.
        bool attackStopped = false;

        if (enemyID == GetInstanceID())
        {
            FXAnimator.SetBool("IsStunned", true);

            StartCoroutine(Stunned(time_));

            //Stunned coroutine. This actually stuns the enemy and unstuns on a timer.
            IEnumerator Stunned(float time_)
            {
                //Perform all changes to the creature here to take affect when they are stunned.
                if(canAttack)
                {
                    attackStopped = true;
                    canAttack = false;
                }
                moveSpeed = 0.0f;

                //Next line pauses the coroutine for a time_.
                yield return new WaitForSeconds(time_);
                //Reset all stats to end the stun.
                if(attackStopped == true)
                {
                    canAttack = true;
                }
                moveSpeed = maxMoveSpeed;

                FXAnimator.SetBool("IsStunned", false);
            }
        }
    }

    //When a creature is shielded, prevent it from taking damage for time_ and then let it take damage again when the shield is gone.
    protected void OnShield(float time_, int enemyID)
    {
        if(enemyID == GetInstanceID())
        {
            FXAnimator.SetBool("IsShielded", true);
            StartCoroutine(Shielded(time_));

            IEnumerator Shielded(float time_)
            {
                canTakeDamage = false;
                yield return new WaitForSeconds(time_);
                canTakeDamage = true;
                FXAnimator.SetBool("IsShielded", false);
            }
        }
    }
}
