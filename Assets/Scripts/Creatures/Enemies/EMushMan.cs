using UnityEngine;

public class EMushMan : BaseEnemy, IEnemy
{
    //The amount ofd time to stun units by when I reach the end of the path.
    static float STUN_TIME = 2.0f;

    //Do more stuff when a fairy reaches the end.
    public override void ReachedEnd()
    {
        foreach (BaseCreature unit in gameMan.aliveUnits)
        {
            StatusEffectManager.OnStun(STUN_TIME, unit.GetInstanceID());
            Debug.Log(unit.GetInstanceID() + "was stunned by " + name);
        }

        //Stun all units for 2 seconds.
        base.ReachedEnd();
    }
}