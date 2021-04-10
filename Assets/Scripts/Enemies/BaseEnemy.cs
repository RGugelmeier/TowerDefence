using UnityEngine;

//* ABSTRACT ENEMY CLASS *//
//This class is what all enemies will inherit from.
//It includes functionality for initialization and moving.

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed, damage, health;    //The unit's stats.

    protected int nextNode;                 //The next node that the unit has to move to. Changed whenever it reaches it's current value.

    protected GameObject[] moveToNodes;     //Array of nodes that the unit will have to move to.
    protected GameManager gameMan;          //Reference to the game manager. Used to check and affect game stats.

    void Awake()
    {
        //Get all positions that the enemies will need to move to.
        moveToNodes = GameObject.FindGameObjectsWithTag("RotateNode");
        //Set initial position.
        transform.Translate(moveToNodes[0].transform.position);
        //Set the first node to start the enemy moving.
        nextNode = 0;
        //Set game manager reference.
        gameMan = FindObjectOfType<GameManager>();
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        //Move to the next node.
        MoveToNode(moveToNodes[nextNode]);
    }

    //Move toward the next node. Speed is determined by moveSpeed variable.
    void MoveToNode(GameObject node)
    {
        transform.position = Vector3.MoveTowards(transform.position, node.transform.position, moveSpeed * Time.deltaTime);
    }
}
