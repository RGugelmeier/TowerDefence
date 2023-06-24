using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    //The player's health, money balance, and upgrade points.
    public float health, maxHealth, balance;
    [HideInInspector] public int upgradePoints, puncherUpgradeCost, guardUpgradeCost, archerUpgradeCost;
    public int puncherLevel, archerLevel, guardLevel;
    //The hioghest level the player has completed.
    public int highestLevelCompleted;
    //The number of the next level.
    public int nextLevelNumber;
    //Holds all of the units a player has currently created.
    public List<UnitBase> aliveUnits;
    public List<BaseEnemy> aliveEnemies;

    public static Action OnUpdateBal;

    //The canvas that is displayed when the player loses all HP
    public Canvas GameOverCanvas;

    //Reference to the unit manager.
    private UnitManager unitMan;

    //Static gameManager instance makes it so only one instance of a game manager can exist at a time, making it a singleton.
    public static GameManager gameManInstance;

    public float timeScale = 1.0f;

    private void Awake()
    {
        //Singleton setting. This makes it so only one game manager can exist at a time.
        if(gameManInstance == null)
        {
            gameManInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //Get a reference to the unit manager.
        unitMan = FindObjectOfType<UnitManager>();

        //Set initial levels of units. Puncher is the only one unlocked on the first level so it is level 1. Others are 0;
        puncherLevel = 1;
        archerLevel = guardLevel = 0;

        //Set initial amount of upgrade points and upgrade costs for units.
        upgradePoints = 0;
        puncherUpgradeCost = 100;
        guardUpgradeCost = 100;
        archerUpgradeCost = 100;

        //Set the initial next level to be 1. This changes when the level is finished, before it loads the next level.
        highestLevelCompleted = 0;
        nextLevelNumber = 1;

        //Subscribe events.
        BaseEnemy.OnReachedEnd += CheckDeath;
        BaseEnemy.OnDie += IncreaseMoney; 
        UnitBase.OnCreation += DecreaseMoney;
        if (WaveManager.OnLevelEnd == null)
        {
            WaveManager.OnLevelEnd += NextLevel;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Set default values.
        maxHealth = 100.0f;
        health = maxHealth;
        balance = 20.0f;
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    //Any time a scene is laoded, check if it is a level. If it is, set the game over canvas to the proper one this that level.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Level") & !scene.name.Contains("Selection"))
        {
            GameOverCanvas = GameObject.Find("LoseUI").GetComponent<Canvas>();
            health = maxHealth;
        }
    }

    //Checks if the player's heal;th has dropped to or below zero. If it has, end the game.
    public void CheckDeath(GameObject enemy)
    {
        if(health <= 0.0f)
        {
            foreach (BaseEnemy enemy_ in aliveEnemies)
            {
                enemy_.SetSpeed(0.0f);
                enemy_.canTakeDamage = false;
            }
            GameOverCanvas.enabled = true;

            StartCoroutine(GameOverTimer());
        }
    }

    IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(5f);

        GameOverCanvas.enabled = false;
        AudioManager.audioManInstance.Stop("WaveSongLoop");
        AudioManager.audioManInstance.Play("MenuMusic");
        SceneManager.LoadScene("LevelSelection");
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

        highestLevelCompleted = currentLevel;

        if (currentLevel != 20)
        {
            nextLevelNumber++;
        }

        if (currentLevel != 0 && currentLevel != 20)
        {
            SceneManager.LoadScene("UnitUpgradeShop");
        }
        else if (currentLevel != 0 && currentLevel == 20)
        {
            SceneManager.LoadScene("LevelSelection");
        }
        else
        {
            Debug.LogError("The game tried to load a level, but the GameManager does not know what level to load. Check to see if the GameManager knows what the current level is on this level.");
        }
    }

    //Runs the save game function from the SaveOrLoad system.
    public void SaveGame()
    {
        SaveOrLoad.SaveGame(gameManInstance);
    }

    //Calls the load game function from the SaveOrLoad system and stopres them into a variable called data.
    //Then tells each manager the information they need to know from the variable we just loaded data into.
    public void LoadGame()
    {
        //Get game data.
        //GameData.SaveData data = SaveOrLoad.LoadGame();
        //
        ////Load data into manager's variables.
        //upgradePoints = data.levelUpPoints;
        //
        ////Unit level data.
        //unitMan.monkLvl = data.puncherLevel;
        //unitMan.guardLvl = data.guardLevel;
        //unitMan.archerLvl = data.archerLevel;
    }
}
