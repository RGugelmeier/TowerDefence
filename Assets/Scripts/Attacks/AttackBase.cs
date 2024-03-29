﻿using System;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    //Amount of damage to deal.
    //How fast the attack will travel.
    public float damage;
    [SerializeField] protected float speed, maxLiveTime;
    private float liveTime;
    protected bool hitEnemy = false; //True if the attack hits an enemy.
    public bool isMaxLevel;
    public BaseCreature target; //The target location.

    //Reference the attack's collision box.
    [SerializeField] BoxCollider2D ignoreCollision;
    BoxCollider2D collision;

    //This will hold the GameObject of the unit that created this attack.
    GameObject parent;

    //Reference to the attack pool.
    protected AttackPool pool;

    //Events that the attack will raise when it hits an enemy.
    public static Action<BaseCreature, int> onCreatureHit_;
    public static Action onDamageRecieved_;

    //Look at the target and set timeAlive so it can be used properly in update.
    void Start()
    {
        liveTime = maxLiveTime;
        parent = transform.parent.gameObject;
        target = parent.GetComponent<UnitBase>().potentialTargets.First.Value;
        //Set the box to ignore.
        collision = GetComponent<BoxCollider2D>();
        ignoreCollision = GameObject.Find("Spot Holder(Clone)").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collision, ignoreCollision, true);
        transform.up = -target.transform.position + transform.position;

        ObjectPool.OnActivate += OnReactivate;
    }

    //This function will be called whenever the attack is reactivated to be used again by the AttackPool.
    //Reset the position to be back at the unit, and rotate it to look at the target.
    public void OnReactivate(GameObject obj)
    {
        if(this != null)
        {
            if (obj.GetInstanceID() == gameObject.GetInstanceID())
            {
                target = parent.GetComponent<UnitBase>().potentialTargets.First.Value;
                liveTime = maxLiveTime;
                transform.position = parent.transform.position;
                transform.up = -target.transform.position + transform.position;
            }
        }
    }

    //Move towards the target at a speed determined by the attack's speed. Then check if it has reached it's destination and self destruct if true.
    //While the attack is alive, it's "liveTime" will tick down. If it hits zero, it will be returned to the attack pool.
    private void FixedUpdate()
    {
        liveTime -= Time.deltaTime;

        if(liveTime <= 0.0f)
        {
            pool.Return(gameObject);
        }
        else if(target)
        {
            //transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
            transform.position += -transform.up * speed * Time.deltaTime;
            //transform.up = -target.transform.position + transform.position;
        }
    }

    //Collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if this attack collided with an enemy. This prevents the function from running further when colliding with other things like the map.
        if(collision.GetComponent<BaseCreature>())
        {
            BaseCreature creatureCheck = collision.GetComponent<BaseCreature>();

            //If I collide with an enemy, raise flag for onCreatureHit. In the attack's class, OnHit will be run when this flag is raised.
            if (creatureCheck.canTakeDamage)
            {
                if (collision.gameObject.activeInHierarchy)
                {
                    Debug.Log("Enemy hit!");
                    if (onCreatureHit_ != null)
                    {
                        onCreatureHit_(creatureCheck, GetInstanceID());
                    }
                    if(onDamageRecieved_ != null)
                    {
                        onDamageRecieved_();
                    }
                }
            }
        }
    }
}
