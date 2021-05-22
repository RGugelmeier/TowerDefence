using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPool : ObjectPool, IObjectPool
{
    private void Awake()
    {
        pool = new List<GameObject>();
        VerifyPool();
    }

    //Go through each object in the objects pool to see if they are all enemies.
    public bool VerifyPool()
    {
        //Go through each object in objects...
        foreach (GameObject obj in objects)
        {
            //...and if one of them is not an enemy...
            if (!obj.GetComponent<AttackBase>())
            {
                //return false...
                //...otherwise, return true after sending an error message.
                Debug.LogError("The attack pool was not verified properly!");
                return false;
            }
        }
        return true;
    }
}
