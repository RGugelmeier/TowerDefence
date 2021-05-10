using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Holds the current wave number.
    int currentWaveNum, currentLevelNum;

    //holds the current wave object.
    Wave currentWave;

    //An object that has a wave component on it.
    [SerializeField] GameObject wavePrefab;

    //Reference to the instantiated, active wave.
    GameObject wave_;

    //Enemies that can be spawned. Insert prefabs in editor.
    public GameObject blueBlob, greenBlob, orangeBlob;

    //This canvas holds all pre-wave UI. Activated when no wave is active.
    [SerializeField] Canvas preWaveCanvas;

    //Event that gets called when the level is completed. Used in HUD manager to show the end level UI.
    public static Action OnLevelEnd;

    //Set action events and other default variables.
    void Start()
    {
        Wave.OnWaveFinished += EndWave;
        Wave.OnFileNotFound += OnNoMoreWaves;

        currentLevelNum = 1;
        currentWaveNum = 1;
    }

    //Start a new wave by setting currentWave's level and wave data, then instantiating it.
    public void StartWave()
    {
        preWaveCanvas.enabled = false;

        currentWave = wavePrefab.GetComponent<Wave>();

        currentWave.levelNum = currentLevelNum;
        currentWave.waveNum = currentWaveNum;
        wave_ = Instantiate(wavePrefab);
    }

    //End current wave.
    void EndWave()
    {
        preWaveCanvas.enabled = true;

        Debug.Log("Wave " + currentWaveNum + " completed!");

        currentWaveNum += 1;

        Destroy(wave_);
    }

    //Called when the wave does not exist. This should mean the level is completed. If not, the txt file path was not proper.
    private void OnNoMoreWaves()
    {
        if(OnLevelEnd != null)
            OnLevelEnd();
    }
}
