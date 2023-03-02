using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("LoadOrNew");
        AudioManager.audioManInstance.Play("ButtonPress");
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
        AudioManager.audioManInstance.Play("ButtonPress");
    }

    public void QuitGame()
    {
        //Save game here before quitting
        AudioManager.audioManInstance.Play("ButtonPress");
        Application.Quit();
    }

    public void BackButton(string sceneName)
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGame()
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        //Load game code idk.
    }

    public void NewGame()
    {
        //SceneManager.LoadScene("TutorialLevel");
        AudioManager.audioManInstance.Play("ButtonPress");
        SceneManager.LoadScene("Level1");
    }
}
