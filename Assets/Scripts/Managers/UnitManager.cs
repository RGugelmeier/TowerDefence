using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //Holds each unit type's level.
    public int monkLvl, guardLvl, archerLvl;

    //Set each unit to level zero except monk, which the player will start with at level 1.
    //This is called in the game manager's awake or start function to set up the unit levels initially.
    private void Awake()
    {
        //PlayerPrefs.SetInt("monkLevel", 1);
        //PlayerPrefs.SetInt("guardLevel", 0);
        //PlayerPrefs.SetInt("archerLevel", 0);

        monkLvl = 1;
        guardLvl = 0;
        archerLvl = 0;
    }

    //Levels up the unit this function takes in. Called when the level up button is pressed while in the level up window.
    //Each button will need to know what unit to level up by typing in the unit's name in the button's properties window.
    //If the unit does not exist, throw an error.
    public void LevelUp(string unitToLevelUp)
    {
        if(unitToLevelUp == "monk")
        {
            //PlayerPrefs.SetInt("monkLevel", PlayerPrefs.GetInt("monkLevel") + 1);
            monkLvl++;
        }
        else if(unitToLevelUp == "guard")
        {
            //PlayerPrefs.SetInt("monkLevel", PlayerPrefs.GetInt("monkLevel") + 1);
            guardLvl++;
        }
        else if(unitToLevelUp == "archer")
        {
            //PlayerPrefs.SetInt("monkLevel", PlayerPrefs.GetInt("monkLevel") + 1);
            archerLvl++;
        }
        else
        {
            Debug.LogError("A unit by the type '" + unitToLevelUp + "' does not exist. Check to make sure it is spelled correctly in the UnitManager and the level up button for this unit.");
        }
    }
}
