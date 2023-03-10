public class UArcher : UnitBase
{
    static float BASE_ATTACK_INTERVAL = 0.4f;
    static float BASE_ATTACK_RANGE = 2.0f;

    private void Start()
    {
        //If the unit has been upgraded, apply modifications to it's stats.
        if (gameMan.archerLevel > 1)
        {
            attackSpeedMod = gameMan.archerLevel * 1.10f;
            rangeMod = gameMan.archerLevel * 1.5f;

            ApplyLevel();
        }
        else if (gameMan.archerLevel == 10)
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
        if (gameMan.archerLevel > 1)
        {
            attackRange = BASE_ATTACK_RANGE * rangeMod;
            attackInterval = BASE_ATTACK_INTERVAL / attackSpeedMod;
        }
    }

    private void LaunchAttack()
    {

    }
}