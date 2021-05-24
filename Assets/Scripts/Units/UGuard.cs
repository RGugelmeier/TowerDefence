using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGuard : UnitBase
{
    //This list will hold the two nodes that this guard will move back and forth between. When placing down the guard, the player will also choose two points that the Guard will patrol.
    public GameObject[] patrolNodes;
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //Patrol(patrolNodes[1], patrolNodes[2]);
    }

    void Patrol(GameObject point1, GameObject point2)
    {

    }
}
