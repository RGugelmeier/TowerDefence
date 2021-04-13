using UnityEngine;

//* ABSTRACT UNIT CLASS *//
//This class is what all units will inherit from.
//It includes functionality for initialization, moving, and attacking.

public abstract class UnitBase : MonoBehaviour
{
    //Unit stats.
    [SerializeField] protected float moveSpeed, attackInterval, attackRange, cost;

    //The attack prefab that the unit will perform.
    [SerializeField] protected AttackBase attack;

    private float attackTimer;

    protected CircleCollider2D enemyDetection;

    //Get circle collider reference.
    private void Awake()
    {
        enemyDetection = GetComponent<CircleCollider2D>();

        //Initialize attack timer.
        attackTimer = attackInterval;

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

    //Instantiate the attack.
    void LaunchAttack()
    {
        if(attack != null)
        {
            Instantiate(attack, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("A unit called: " + gameObject.name + " has no attack, but tried to attack an enemy!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //If the unit's circle collider is collided with an enemy.
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            //Set the attack's target.
            attack.target = collision.transform;
            //Attack if the attack timer allows it, then reset the attack timer.
            if (attackTimer >= attackInterval)
            {
                
                LaunchAttack();
                attackTimer = 0.0f;
            }
            //If you cannot attack, change timer.
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
    }
}
