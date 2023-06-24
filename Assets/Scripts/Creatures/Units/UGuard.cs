public class UGuard : UnitBase
{
    public static int MAX_LEVEL = 5;

    //The modifier for how long to stun a hit enemy.
    public float stunMod;

    private void Start()
    {
        //If the unit has been upgraded, apply modifications to it's stats. The attack's script handles applying the modifier.
        if (gameMan.guardLevel > 1)
        {
            damageMod = gameMan.guardLevel * 0.6f;
            stunMod = gameMan.guardLevel * 0.1f;
        }
    }
}