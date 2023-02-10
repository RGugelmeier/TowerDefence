using UnityEngine;

public class EFairy : BaseEnemy, IEnemy
{
    //The amount of health to give to all other enemies.
    static float HEAL_AMOUNT = 10.0f;

    //Do more stuff when a fairy reaches the end.
    public override void ReachedEnd()
    {
        //Heal each enemy by HEAL_AMOUNT.
        foreach(BaseCreature enemy in gameMan.aliveEnemies)
        {
            enemy.health += HEAL_AMOUNT;

            //If the enemy is overhealed, set it's hp back to it's max hp.
            if(enemy.health > enemy.maxHealth)
            {
                enemy.health = enemy.maxHealth;
            }
        }

        base.ReachedEnd();
    }
}