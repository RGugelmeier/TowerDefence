using System;
using System.Collections.Generic;
using UnityEngine;

//* ABSTRACT UNIT CLASS *//
//This class is what all units will inherit from.
//It includes functionality for initialization, moving, and attacking.

public abstract class UnitBase : BaseCreature
{
    //Unit stats.
    [SerializeField] public float damageMod, attackSpeedMod, rangeMod;
    public float cost;

    //List of enemies within attack range.
    public LinkedList<BaseEnemy> potentialTargets = new LinkedList<BaseEnemy>();

    private float animResetTimer;

    //Set to false at first so I cannot attack before I am placed.
    public bool isActive;

    //Animation variables. Used to control which way the unit looks when firing an attack.
    //Quadrants are used to determine where the enemy is in relation to the unit.
    Vector2 lowerQuadrant, rightQuadrant, leftQuadrant;
    Animator anim;

    //Event raised when I am created by the player.
    public static Action<GameObject> OnCreation;

    //Get circle collider reference.
    new private void Awake()
    {
        base.Awake();

        //Set quadrant x and y values.
        rightQuadrant.x = 35.0f; rightQuadrant.y = 125.0f;
        lowerQuadrant.x = 125.01f; lowerQuadrant.y = 215.0f;
        leftQuadrant.x = 215.01f; leftQuadrant.y = 305.0f;

        anim = GetComponent<Animator>();

        damageMod = attackSpeedMod = rangeMod = 1f;

        //Set isActive to false. This will be set to true when the player places one down.
        //isActive = false;

        //Initialize attack timer.
        animResetTimer = 0.5f;

        BaseEnemy.OnDie += OnEnemyDie;
    }

    void Update()
    {
        //Resets animation if there is one playing and half a second has passed since the anim played.
        if(anim.GetBool("isUp") || anim.GetBool("isDown") || anim.GetBool("isRight") || anim.GetBool("isLeft"))
        {
            if(animResetTimer <= 0.0f)
            {
                ResetAnim();
                animResetTimer = 0.5f;
            }
            animResetTimer -= Time.deltaTime;
        }
        //Updates the attack timer if an attack has not been readied.
        if(attackTimer < attackInterval)
        {
            attackTimer += Time.deltaTime;
        }
    }

    //Instantiate the attack, then reset the attack timer to start counting to another attack being prepared.
    void LaunchAttack(BaseEnemy target_)
    {
        if(attack != null)
        {
            //Create a new attack and set it's target
            createdAttack = attackPool.CreateNew(attack, transform.position, gameObject);
            //attackObj = createdAttack.GetComponent<AttackBase>();
            //attackObj.target = target_;
            if(GetComponent<UArcher>()) //Play archer's attack sound when they attack.
            {
                AudioManager.audioManInstance.Play("BowShot");
            }
        }
        else
        {
            Debug.LogWarning("A unit called: " + gameObject.name + " has no attack, but tried to attack an enemy!");
        }
        attackTimer = 0.0f;
    }

    //Set the attack to it's max level form.
    protected void MaxLevel()
    {
        //attackObj.isMaxLevel = true;
    }

    private void OnEnemyDie(GameObject enemy)
    {
        if (potentialTargets.Contains(enemy.GetComponent<BaseEnemy>()))
        {
            potentialTargets.Remove(enemy.GetComponent<BaseEnemy>());
        }
    }

    // Add
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collision is an enemy
        if(collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            //Add the enemy to the potential targets list
            potentialTargets.AddFirst(collision.gameObject.GetComponent<BaseEnemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Check if the collision is an enemy
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            //Remove the enemy to the potential targets list
            potentialTargets.Remove(collision.gameObject.GetComponent<BaseEnemy>());
        }
    }

    //Collision handling. Used to check for enemies in range to attack.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isActive)
        {
            //If the unit's circle collider is collided with an enemy and the unit can attack. (For example, if the unit is stunned it cannot attack)
            if (collision.gameObject.GetComponent<BaseEnemy>() != null && canAttack)
            {
                if (attackTimer >= attackInterval)
                {
                    //Used to hold the positions to use to check what angle the collider hits with.
                    Vector2 myPos, otherPos;
                    myPos = transform.position;
                    //otherPos = collision.gameObject.transform.position;
                    otherPos = potentialTargets.First.Value.gameObject.transform.position;
                    float angleToEnemy = FindDegree(myPos.x - otherPos.x, myPos.y - otherPos.y);

                    //If left
                    if (angleToEnemy  >= rightQuadrant.x && angleToEnemy <= rightQuadrant.y)
                    {
                        anim.SetBool("isRight", false);
                        anim.SetBool("isLeft", true);
                        anim.SetBool("isUp", false);
                        anim.SetBool("isDown", false);
                    }
                    //If up
                    else if( angleToEnemy >= lowerQuadrant.x && angleToEnemy <= lowerQuadrant.y)
                    {
                        anim.SetBool("isRight", false);
                        anim.SetBool("isLeft", false);
                        anim.SetBool("isUp", true);
                        anim.SetBool("isDown", false);
                    }
                    //If right
                    else if (angleToEnemy >= leftQuadrant.x && angleToEnemy <= leftQuadrant.y)
                    {
                        anim.SetBool("isRight", true);
                        anim.SetBool("isLeft", false);
                        anim.SetBool("isUp", false);
                        anim.SetBool("isDown", false);
                    }
                    //If down
                    else
                    {
                        anim.SetBool("isRight", false);
                        anim.SetBool("isLeft", false);
                        anim.SetBool("isUp", false);
                        anim.SetBool("isDown", true);
                    }
                    LaunchAttack(potentialTargets.Last.Value);
                }
            }
        }
    }

    //Resets the animation state to idle.
    void ResetAnim()
    {
        anim.SetBool("isRight", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);
    }

    //Used to check what direction an enemy is from a unit.
    public static float FindDegree(float x, float y)
    {
        float value = (float)((System.Math.Atan2(x, y) / System.Math.PI) * 180f);
        if (value < 0) value += 360f;
        return value;
    }
}
