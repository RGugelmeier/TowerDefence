using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    //Holds the current level and wave number.
    [SerializeField] public int currentLevelNum;
    [SerializeField] private int currentWaveNum;

    //holds the current wave object.
    private Wave currentWave;

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

    //Static wave manager instance makes it so only one instance of a wave manager can exist at a time, making it a singleton.
    public static WaveManager waveManInstance;

    //Set action events and other default variables.
    void Start()
    {
        //Singleton setting. This makes it so only one game manager can exist at a time.
        if (waveManInstance == null)
        {
            waveManInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        Wave.OnWaveFinished += EndWave;
        Wave.OnFileNotFound += OnNoMoreWaves;
        SceneManager.sceneLoaded += OnSceneLoaded;

        currentWaveNum = 1;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Contains("Level") &! scene.name.Contains("Selection"))
        {
            preWaveCanvas = GameObject.Find("PrewaveUI").GetComponent<Canvas>();
            AudioManager.audioManInstance.Stop("MenuMusic");
            AudioManager.audioManInstance.Play("PreWaveAmbient");
            currentWaveNum = 1;
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

        //AudioManager.audioManInstance.Play("WaveSongIntro");
        AudioManager.audioManInstance.Play("WaveSongLoop");
    }

    //End current wave.
    public void EndWave() //Make this public when  debugging
    {
        if(preWaveCanvas != null)
        {
            preWaveCanvas.enabled = true;
        }

        Debug.Log("Wave " + currentWaveNum + " completed!");

        currentWaveNum += 1;

        //AudioManager.audioManInstance.Stop("WaveSongIntro");
        AudioManager.audioManInstance.Stop("WaveSongLoop");

        AudioManager.audioManInstance.Play("PreWaveAmbient");
        Destroy(wave_);
    }

    //Called when the wave does not exist. This should mean the level is completed. If not, the txt file path was not proper.
    public void OnNoMoreWaves() ////Make this public when  debugging
    {
        if(OnLevelEnd != null)
            OnLevelEnd(currentLevelNum);

        currentWaveNum = 1;
        currentLevelNum++;
    }

    //Level num getter.
    public int GetCurrentLevel()
    {
        return currentLevelNum;
    }
}
