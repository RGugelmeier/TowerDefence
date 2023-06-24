public class UArcher : UnitBase
{
    static float BASE_ATTACK_INTERVAL = 1.0f;
    static float BASE_ATTACK_RANGE = 2.0f;
    public static int MAX_LEVEL = 5;

    private void Start()
    {
        //If the unit has been upgraded, apply modifications to it's stats.
        if (gameMan.archerLevel > 1)
        {
            attackSpeedMod = gameMan.archerLevel * 0.05f;

            ApplyLevel();
        }
        else if (gameMan.archerLevel == 10)
        {
            MaxLevel();
        }
    }

    //Apply modifiers from the unit type's level to it's stats.
    private void ApplyLevel()
    {
        if (gameMan.archerLevel > 1 && isActive)
        {
            attackRange = BASE_ATTACK_RANGE + (0.1f * gameMan.archerLevel);
            attackInterval = BASE_ATTACK_INTERVAL - attackSpeedMod;
        }
    }
}