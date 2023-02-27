using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    //Holds the current level and wave number.
    [SerializeField] private int currentLevelNum;
    private int currentWaveNum;

    //holds the current wave object.
    Wave currentWave;

    //An object that has a wave component on it.
    [SerializeField] GameObject wavePrefab;

    //Reference to the instantiated, active wave.
    GameObject wave_;

    //Enemies that can be spawned. Insert prefabs in editor.
    public GameObject blueBlob, greenBlob, orangeBlob, mushMan, treant, wisp, fairy;

    //This canvas holds all pre-wave UI. Activated when no wave is active.
    [SerializeField] Canvas preWaveCanvas;

    //Event that gets called when the level is completed. Used in HUD manager to show the end level UI.
    public static Action<int> OnLevelEnd;

    //Set action events and other default variables.
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Wave.OnWaveFinished += EndWave;
        Wave.OnFileNotFound += OnNoMoreWaves;
        SceneManager.sceneLoaded += OnSceneLoaded;

        currentWaveNum = 1;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Contains("Level"))
        {
            preWaveCanvas = GameObject.Find("PrewaveUI").GetComponent<Canvas>();
        }
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
        if(preWaveCanvas != null)
        {
            preWaveCanvas.enabled = true;
        }

        Debug.Log("Wave " + currentWaveNum + " completed!");

        currentWaveNum += 1;

        Destroy(wave_);
    }

    //Called when the wave does not exist. This should mean the level is completed. If not, the txt file path was not proper.
    private void OnNoMoreWaves()
    {
        if(OnLevelEnd != null)
            OnLevelEnd(currentLevelNum);

        //currentLevelNum++;
    }

    //Level num getter.
    public int GetCurrentLevel()
    {
        return currentLevelNum;
    }
}
