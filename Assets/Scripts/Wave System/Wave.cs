using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Wave : MonoBehaviour
{
    WaveManager waveMan;

    List<GameObject> enemiesToSpawn = new List<GameObject>();

    float spawnTimer;

    //Awake is performed as soon as the object becomes valid.
    void Awake()
    {
        //Get reference to the wave manager.
        waveMan = FindObjectOfType<WaveManager>();

        //Read the wave file.
        string[] lines = File.ReadAllLines("G:/UNITY/Projects/TowerDefence/Assets/Scripts/Wave System/Waves/wave1-1.txt");

        //Add each unit type to the list. This wave only has "N" (Normal enemies).
        foreach (string line in lines)
        {
            if (line.Equals("N"))
            {
                enemiesToSpawn.Add(waveMan.blueBlob);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //If there are enemies to spawn.
        if(enemiesToSpawn.Count != 0)
        {
            //Countdown spawn timer.
            spawnTimer -= Time.deltaTime;

            //When spawn timer hits zero, spawn the next enemy in line and clear it fron enemiesToSpawn. Reset spawn timer.
            if(spawnTimer <= 0.0f)
            {
                if(enemiesToSpawn.Count != 0)
                {
                    Instantiate<GameObject>(enemiesToSpawn[enemiesToSpawn.Count - 1]);
                    enemiesToSpawn.RemoveAt(enemiesToSpawn.Count - 1);
                    Debug.Log("Enemy spawned. Enemies left to spawn: " + enemiesToSpawn.Count);

                    spawnTimer = 2;
                }
            }
        }
    }
}
