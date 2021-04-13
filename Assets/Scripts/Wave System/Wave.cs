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
        BaseEnemy.OnDie += OnEnemyDeath;

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
                    enemiesToSpawn.Add(waveMan.blueBlob);
                }
                //Green blob
                else if (line.Equals("GB"))
                {
                    enemiesToSpawn.Add(waveMan.greenBlob);
                }
                //Orange blob
                else if (line.Equals("OB"))
                {
                    enemiesToSpawn.Add(waveMan.orangeBlob);
                }
            }
        }
        else
        {
            if (OnFileNotFound != null)
                OnFileNotFound();
        }
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
                    Instantiate<GameObject>(enemiesToSpawn[enemiesToSpawn.Count - 1]);
                    enemiesAlive.Add(enemiesToSpawn[enemiesToSpawn.Count - 1]);
                    enemiesToSpawn.RemoveAt(enemiesToSpawn.Count - 1);
                    Debug.Log("Enemy spawned. Enemies left to spawn: " + enemiesToSpawn.Count);

                    spawnTimer = 2;
                }
            }
        }
    }

    //Called when an enemy dies.
    public void OnEnemyDeath(GameObject deadEnemy)
    {
        enemiesAlive.RemoveAt(enemiesAlive.IndexOf(deadEnemy) + 1);

        if (enemiesAlive.Count <= 0 && enemiesToSpawn.Count == 0)
        {
            if(OnWaveFinished != null)
                OnWaveFinished();
        }
    }
}
