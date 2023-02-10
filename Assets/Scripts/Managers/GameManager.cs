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
    [HideInInspector] public int upgradePoints;
    //Holds all of the units a player has currently created.
    public List<UnitBase> aliveUnits;
    public List<BaseEnemy> aliveEnemies;

    public static Action OnUpdateBal;

    //Reference to the unit manager.
    private UnitManager unitMan;

    private void Awake()
    {
        Advertisement.Initialize("4632015", true);

        //Get a reference to the unit manager.
        unitMan = FindObjectOfType<UnitManager>();

        //Set initial amount of upgrade points.
        upgradePoints = 0;

        //Subscribe events.
        BaseEnemy.OnReachedEnd += CheckDeath;
        BaseEnemy.OnDie += IncreaseMoney;
        UnitBase.OnCreation += DecreaseMoney;
        WaveManager.OnLevelEnd += NextLevel;

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

        if (currentLevel != 0)
        {
            SceneManager.LoadScene("Level" + (currentLevel + 1));

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
