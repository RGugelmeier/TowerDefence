using UnityEngine;

public class GameData : MonoBehaviour
{
    //public int highestLevelCompleted;
    public int levelUpPoints;
    //Each unit's level. If the player does not have the unit unlocked, their level is 0.
    public int guardLevel, monkLevel, archerLevel;

    //Set all game data by getting it from wherever holds it.
    public GameData(GameManager gameMan, UnitManager unitMan)
    {
        levelUpPoints = gameMan.upgradePoints;
        guardLevel = unitMan.guardLvl;
        monkLevel = unitMan.monkLvl;
        archerLevel = unitMan.archerLvl;
    }
}
