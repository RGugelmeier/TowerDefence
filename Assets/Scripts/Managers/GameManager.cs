using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    //The player's health, money balance, and upgrade points.
    [HideInInspector] public float health, maxHealth, balance;
    [HideInInspector] public int upgradePoints, puncherUpgradeCost, guardUpgradeCost, archerUpgradeCost;
    [HideInInspector] public int puncherLevel, archerLevel, guardLevel;
    //The number of the next level.
    public int nextLevelNumber;
    //Holds all of the units a player has currently created.
    public List<UnitBase> aliveUnits;
    public List<BaseEnemy> aliveEnemies;

    public static Action OnUpdateBal;

    //Reference to the unit manager.
    private UnitManager unitMan;

    private void Awake()
    {
        //Find all game objects with GameMan as a tag. The only game opbj that has this tag is the game manager. This is to prevent more than  one game manager to exist at a time.
        GameObject[] findGameManagers = GameObject.FindGameObjectsWithTag("GameMan");

        if (findGameManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Advertisement.Initialize("4632015", true);

        //Get a reference to the unit manager.
        unitMan = FindObjectOfType<UnitManager>();

        //Set initial levels of units. Puncher is the only one unlocked on the first level so it is level 1. Others are 0;
        puncherLevel = 1;
        archerLevel = guardLevel = 0;

        //Set initial amount of upgrade points and upgrade costs for units.
        upgradePoints = 0;
        puncherUpgradeCost = 100;
        guardUpgradeCost = 200;
        archerUpgradeCost = 200;

        //Set the initial next level to be 1. This changes when the level is finished, before it loads the next level.
        nextLevelNumber = 1;

        //Subscribe events.
        BaseEnemy.OnReachedEnd += CheckDeath;
        BaseEnemy.OnDie += IncreaseMoney;
        UnitBase.OnCreation += DecreaseMoney;
        if (WaveManager.OnLevelEnd == null)
        {
            WaveManager.OnLevelEnd += NextLevel;
        }

        //Set default values.
        maxHealth = 100.0f;
        health = maxHealth;
        balance = 20.0f;
    }

    //Checks if the player's heal;th has dropped to or below zero. If it has, end the game.
    public void CheckDeath(GameObject enemy)
    {
        if(health <= 0.0f)
        {
            Application.Quit();
            Debug.Log("Game Over.");
        }
    }

    //Add money to the player's balance.
    private void IncreaseMoney(GameObject enemy)
    {
        if(enemy.activeInHierarchy)
        {
            balance += enemy.GetComponent<BaseEnemy>().worth;
        }

        if (OnUpdateBal != null)
            OnUpdateBal();
    }

    //Take money from the player's balance.
    private void DecreaseMoney(GameObject unit)
    {
        balance -= unit.GetComponent<UnitBase>().cost;

        if (OnUpdateBal != null)
            OnUpdateBal();
    }

    private void NextLevel(int currentLevel)
    {
        if(this != null)
        {
            StartCoroutine(NextLevelTimer(currentLevel));
            aliveUnits.Clear();
            health = maxHealth;
        }
    }

    IEnumerator NextLevelTimer(int currentLevel)
    {
        yield return new WaitForSeconds(5f);

        nextLevelNumber++;

        if (currentLevel != 0)
        {
            //SceneManager.LoadScene("Level" + (currentLevel + 1));
            
            SceneManager.LoadScene("UnitUpgradeShop");

            if (Advertisement.IsReady("Press_E"))
            {
                Advertisement.Show("Press_E");
            }
        }
        else
        {
            Debug.LogError("The game tried to load a level, but the GameManager does not know what level to load. Check to see if the GameManager knows what the current level is on this level.");
        }
    }

    //Runs the save game function from the SaveOrLoad system.
    public void SaveGame()
    {
        SaveOrLoad.SaveGame(this, unitMan);
    }

    //Calls the load game function from the SaveOrLoad system and stopres them into a variable called data.
    //Then tells each manager the information they need to know from the variable we just loaded data into.
    public void LoadGame()
    {
        //Get game data.
        GameData data = SaveOrLoad.LoadGame();

        //Load data into manager's variables.
        upgradePoints = data.levelUpPoints;

        //Unit level data.
        unitMan.monkLvl = data.monkLevel;
        unitMan.guardLvl = data.guardLevel;
        unitMan.archerLvl = data.archerLevel;
    }
}
