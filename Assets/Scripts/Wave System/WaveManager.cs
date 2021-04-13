﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Holds the current wave number.
    int currentWaveNum, currentLevelNum;

    //holds the current wave object.
    Wave currentWave;

    [SerializeField] GameObject wavePrefab;
    GameObject wave_;

    //Check that holds if there is an active wave and if all of waves have been completed or not.
    bool hasActiveWave, hasWavesLeft;

    //Enemies that can be spawned. Insert prefabs in editor.
    public GameObject blueBlob, greenBlob, orangeBlob;

    // Start is called before the first frame update
    void Start()
    {
        Wave.OnWaveFinished += EndWave;
        Wave.OnFileNotFound += OnNoMoreWaves;

        currentLevelNum = 1;
        currentWaveNum = 1;
        StartWave(currentLevelNum, currentWaveNum);
    }

    //Start a new wave by setting currentWave's level and wave data, the instantiating it.
    void StartWave(int levelToStartNum, int waveToStartNum)
    {
        currentWave = wavePrefab.GetComponent<Wave>();

        currentWave.levelNum = levelToStartNum;
        currentWave.waveNum = waveToStartNum;
        wave_ = Instantiate(wavePrefab);
    }

    //End current wave.
    void EndWave()
    {
        Debug.Log("Wave " + currentWaveNum + " completed!");

        Destroy(wave_);

        StartWave(currentLevelNum, currentWaveNum + 1);
    }

    //Called when the wave does not exist.
    private void OnNoMoreWaves()
    {
        Debug.Log("No more waves. You Win!");
        Application.Quit();
    }
}
