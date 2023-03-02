using UnityEngine;

public class ETreant : BaseEnemy, IEnemy
{
    //The amount ofd time to stun units by when I reach the end of the path.
    static float SHIELD_TIME = 2.0f;

    //Do more stuff.
    public override void ReachedEnd()
    {
        foreach(BaseCreature enemy in gameMan.aliveEnemies)
        {
            StatusEffectManager.OnShield(SHIELD_TIME, enemy.GetInstanceID());
        }

        AudioManager.audioManInstance.Play("Shield");

        base.ReachedEnd();
    }
}
