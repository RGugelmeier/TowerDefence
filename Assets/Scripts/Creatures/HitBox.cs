using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    BoxCollider2D hitBox;    //Yje collider that will act as the hitbox for the creature.

    // Start is called before the first frame update
    void Start()
    {
         gameObject.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
