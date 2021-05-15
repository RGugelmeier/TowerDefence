using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ObjectPool : MonoBehaviour
{
    //Event that will reset the game object to it's default values so it can be used again.
    public static Action<GameObject> OnActivate;
    //The specific instance of the obj that is being created.
    private GameObject createdObj;

    //The pool. This will hold all created object that are ready to use.
    [SerializeField]
    public List<GameObject> pool;

    //This holds all types of prefabs that this pool will be able to store. To add a prefab, do so in the Unity editor.
    [SerializeField]
    protected List<GameObject> objects;

    //This either creates a new object or uses one that was already in the pool.
    public void CreateNew(GameObject objToCreate)
    {
        //Go through each object currently in the obj pool...
        foreach(GameObject obj in pool)
        {
            //... and if there is one not currently in use...
            if(!obj.activeInHierarchy && obj.name == objToCreate.name + "(Clone)")
            {
                //...raise the OnActivate event. This will set the defaults of the object, done in their own class...
                if(OnActivate != null)
                {
                    //OnActivate(obj.GetComponent<BaseEnemy>().id);
                    OnActivate(obj);
                }
                //...then use it.
                obj.SetActive(true);
                return;
            }   
        }
        //...and if all of them are in use...
        //create a new one.
        //Each object that can be part of a pool must have their default values set in their awake, that way they are instantiated in the proper space with the proper values.
        createdObj = Instantiate(objToCreate);
        pool.Add(createdObj);
    }

    //This will return an object back to the pool so it can be used again.
    public void Return(GameObject objToReturn)
    {
        //If the object passed in is currently in use...
        if(objToReturn.activeInHierarchy)
        {
            //...set it to be inactive.
            objToReturn.SetActive(false);
        }
    }
}
