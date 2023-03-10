using System;
using UnityEngine;

//* ABSTRACT UNIT CLASS *//
//This class is what all units will inherit from.
//It includes functionality for initialization, moving, and attacking.

public abstract class UnitBase : BaseCreature
{
    //Unit stats.
    [SerializeField] protected float damageMod, attackSpeedMod, rangeMod;
    [HideInInspector] public float cost;

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
            createdAttack = attackPool.CreateNew(attack, transform.position, gameObject);
            attackObj = createdAttack.GetComponent<AttackBase>();
            attackObj.target = target_;
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

    //Collision handling. Used to check for enemies in range to attack.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isActive)
        {
            //If the unit's circle collider is collided with an enemy and the unit can attack. (For example, if the unit is stunned it cannot attack)
            if (collision.gameObject.GetComponent<BaseEnemy>() != null && canAttack)
            {
                //Set the attack's target.
                if(attackObj)
                {
                    attackObj.target = collision.GetComponent<BaseEnemy>();
                }
                //Attack if the attack timer allows it, then reset the attack timer.
                if (attackTimer >= attackInterval)
                {
                    //Used to hold the positions to use to check what angle the collider hits with.
                    Vector2 myPos, otherPos;
                    myPos = transform.position;
                    otherPos = collision.gameObject.transform.position;
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
                    LaunchAttack(collision.GetComponent<BaseEnemy>());
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
