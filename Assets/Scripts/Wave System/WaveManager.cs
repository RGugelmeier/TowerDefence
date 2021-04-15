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

    //Set action events and other default variables. Then start the first wave.
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

        currentWaveNum += 1;

        Destroy(wave_);

        StartWave(currentLevelNum, currentWaveNum);
    }

    //Called when the wave does not exist.
    private void OnNoMoreWaves()
    {
        Debug.Log("No more waves. You Win!");
        Application.Quit();
    }
}
