using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    BaseEnemy parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collision checking.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //When this unit hit's the next node, set it's target to a new node.
        if (other.gameObject.CompareTag("RotateNode"))
        {
            //If the next node is the end of the path, run OnReachedEnd function.
            if ( parent.nextNode + 1 == parent.moveToNodes.Length)
            {
                parent.ReachedEnd();
            }
            //Else, set what node to move to next.
            else
            {
                parent.nextNode++;
            }
        }
    }
}
