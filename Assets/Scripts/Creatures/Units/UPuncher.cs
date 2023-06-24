using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPuncher : UnitBase
{
    public static int MAX_LEVEL = 5;

    private void Start()
    {
        //If the unit has been upgraded, apply modifications to it's stats. The attack's script handles applying the modifier.
        if (gameMan.puncherLevel > 1)
        {
            damageMod = gameMan.puncherLevel * 0.6f;
        }
    }
}
