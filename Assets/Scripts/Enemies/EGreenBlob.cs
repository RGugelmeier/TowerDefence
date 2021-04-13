using UnityEngine;

public class EGreenBlob : BaseEnemy, IEnemy
{
    public void OnReachedEnd()
    {
        //Deal damage to player's health.
        gameMan.health -= damage;
        //Check if health is <= zero.
        gameMan.CheckDeath();

        //Destroy self.
        Destroy(gameObject);
    }

    //Collision checking.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //When this unit hit's the next node, set it's target to a new node.
        if (other.gameObject.CompareTag("RotateNode"))
        {
            //If the next node is the end of the path, run OnReachedEnd function.
            if (nextNode + 1 == moveToNodes.Length)
            {
                OnReachedEnd();
            }
            //Else, set what node to move to next.
            else
            {
                nextNode++;
            }
        }
    }
}
