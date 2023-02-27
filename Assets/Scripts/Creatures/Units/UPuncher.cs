using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPuncher : UnitBase
{
    static float BASE_ATTACK_INTERVAL = 0.5f;

    private void Start()
    {
        //If the unit has been upgraded, apply modifications to it's stats.
        if (gameMan.puncherLevel > 1)
        {
            damageMod = gameMan.puncherLevel * 1.25f;
            attackSpeedMod = gameMan.puncherLevel * 1.10f;

            ApplyLevel();
        }
        else if(gameMan.puncherLevel == 10)
        {
            MaxLevel();
        }
    }

    protected void MaxLevel()
    {

    }

    //Apply modifiers from the unit type's level to it's stats.
    private void ApplyLevel()
    {
        if (gameMan.puncherLevel > 1)
        {
            attack.GetComponent<Punch>().damage *= damageMod;
            attackInterval = BASE_ATTACK_INTERVAL * attackSpeedMod;
        }
    }
}
