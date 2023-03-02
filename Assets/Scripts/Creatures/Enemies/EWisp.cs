using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EWisp : BaseEnemy, IEnemy
{
    static float CURSE_TIME_TO_KILL = 3.0f;

    

    //Do more stuff when a fairy reaches the end.
    public override void ReachedEnd()
    {
        //This code has to be done here ionstead of in parent class due to the BaseCreature being deactivated before the coroutine 'Cursed' can finish.
        //Deal damage to player's health.
        gameMan.health -= damage;

        //Raise OnReachedEnd event. This checks if the player's health in the game manager is <= zero. It also updates the health bar in ProgressBar
        if (OnReachedEnd != null)
            OnReachedEnd(gameObject);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        AudioManager.audioManInstance.Play("Curse");

        //Start coroutine that delays unit killing by an amount of time determined by CURSE_TIME_TO_KILL.
        StartCoroutine(Cursed(CURSE_TIME_TO_KILL));
    }

    IEnumerator Cursed(float time_)
    {
        List<BaseCreature> curseList = new List<BaseCreature>();

        //If there are more than three units created, pick 3 random ones and kill them (Return them to unitPool.
        if (gameMan.aliveUnits.Count > 3)
        {
            for (int i = 0; i < 3; i++)
            {
                curseList.Add(gameMan.aliveUnits[Random.Range(0, gameMan.aliveUnits.Count)]);
                //unitPool.Return(gameMan.aliveUnits[Random.Range(0, gameMan.aliveUnits.Count)].gameObject);
                //gameMan.aliveUnits[Random.Range(0, gameMan.aliveUnits.Count)].health = 0;
            }

            foreach (BaseCreature unit in gameMan.aliveUnits)
            {
                unit.FXAnimator.SetBool("IsCursed", true);
            }
        }
        //If there are less than or equal to 3 units created, kill 'em all.
        else
        {
            foreach (BaseCreature unit in gameMan.aliveUnits)
            {
                //unit.health = 0;
                //unitPool.Return(unit.gameObject);
                curseList.Add(unit);
                unit.FXAnimator.SetBool("IsCursed", true);
            }
        }

        yield return new WaitForSeconds(time_);
        
        //Return all cursed game objects to obj pool
        foreach (BaseCreature unit in curseList)
        {
            unit.FXAnimator.SetBool("IsCursed", false);
            unitPool.Return(unit.gameObject);
        }

        //Destroy self.
        gameObject.SetActive(false);
    }
}