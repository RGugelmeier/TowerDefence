using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //Reference to the wave manager.
    WaveManager waveMan;

    //List of enemies to spawn, and list of enemies that have spawned and are still alive.
    public List<GameObject> enemiesToSpawn;// = new List<GameObject>();
    public List<GameObject> enemiesAlive;// = new List<GameObject>();

    private GameObject addedGameObject;
    private GameObject spawnedEnemy;

    //Default to true when initialized. Set to false when the wave has no more enemies left.
    bool isActive;

    //Controlls how often each enemy spawns.
    float spawnTimer;

    //The level this wave is a part of, and what wave number it is. Used in reading in the txt file.
    public int levelNum, waveNum;

    public static Action OnFileNotFound;
    public static Action OnWaveFinished;

    //Awake is performed as soon as the object becomes valid.
    void Awake()
    {
        //Get reference to the wave manager.
        waveMan = FindObjectOfType<WaveManager>();

        if(File.Exists("G:/UNITY/Projects/TowerDefence/Assets/Scripts/Wave System/Waves/wave" + levelNum + "-" + waveNum + ".txt"))
        {
            //Read the wave file.
            string[] lines = File.ReadAllLines("G:/UNITY/Projects/TowerDefence/Assets/Scripts/Wave System/Waves/wave" + levelNum + "-" + waveNum + ".txt");

            //Add each unit type to the list. This wave only has "N" (Normal enemies).
            foreach (string line in lines)
            {
                //Blue blob
                if (line.Equals("BB"))
                {
                    addedGameObject = Instantiate<GameObject>(waveMan.blueBlob);
                    addedGameObject.SetActive(false);
                    enemiesToSpawn.Add(addedGameObject);
                }
                //Green blob
                else if (line.Equals("GB"))
                {
                    addedGameObject = Instantiate<GameObject>(waveMan.greenBlob);
                    addedGameObject.SetActive(false);
                    enemiesToSpawn.Add(addedGameObject);
                }
                //Orange blob
                else if (line.Equals("OB"))
                {
                    addedGameObject = Instantiate<GameObject>(waveMan.orangeBlob);
                    addedGameObject.SetActive(false);
                    enemiesToSpawn.Add(addedGameObject);
                }
            }
        }
        else
        {
            if (OnFileNotFound != null)
                OnFileNotFound();
        }

        BaseEnemy.OnDie += OnEnemyDeath;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //If there are enemies to spawn.
        if(enemiesToSpawn.Count != 0)
        {
            //Countdown spawn timer.
            spawnTimer -= Time.deltaTime;

            //When spawn timer hits zero, spawn the next enemy in line, add it to the alive enemies list, and clear it from enemiesToSpawn. Reset spawn timer.
            if(spawnTimer <= 0.0f)
            {
                if(enemiesToSpawn.Count != 0)
                {
                    spawnedEnemy = enemiesToSpawn[0];
                    spawnedEnemy.SetActive(true);
                    enemiesAlive.Add(spawnedEnemy);
                    enemiesToSpawn.Remove(spawnedEnemy);
                    Debug.Log("Enemy spawned. Enemies left to spawn: " + enemiesToSpawn.Count);

                    spawnTimer = 2;
                }
            }
        }
    }

    //Called when an enemy dies.
    public void OnEnemyDeath(GameObject deadEnemy)
    {
        if (this != null)
        {
            enemiesAlive.RemoveAt(enemiesAlive.IndexOf(deadEnemy) + 1);

            if (enemiesAlive.Count == 0 && enemiesToSpawn.Count == 0)
            {
                if (OnWaveFinished != null)
                    OnWaveFinished();
            }
        }
    }
}
