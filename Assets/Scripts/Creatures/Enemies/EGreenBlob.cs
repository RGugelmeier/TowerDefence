using UnityEngine;

public class EGreenBlob : BaseEnemy, IEnemy
{
    //public void ReachedEnd()
    //{
    //    //Deal damage to player's health.
    //    gameMan.health -= damage;
    //
    //    //Raise OnReachedEnd event. This checks if the player's health in the game manager is <= zero. It also updates the health bar in ProgressBar
    //    if (OnReachedEnd != null)
    //        OnReachedEnd(gameObject);
    //
    //    //Destroy self.
    //    gameObject.SetActive(false);
    //}

    //Collision checking.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //When this unit hit's the next node, set it's target to a new node.
        if (other.gameObject.CompareTag("RotateNode"))
        {
            //If the next node is the end of the path, run OnReachedEnd function.
            if (nextNode + 1 == moveToNodes.Length)
            {
                ReachedEnd();
            }
            //Else, set what node to move to next.
            else
            {
                nextNode++;
            }
        }
    }
}
