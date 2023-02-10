using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("LoadOrNew");
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
    }

    public void QuitGame()
    {
        //Save game here before quitting

        Application.Quit();
    }

    public void BackButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGame()
    {
        //Load game code idk.
    }

    public void NewGame()
    {
        //SceneManager.LoadScene("TutorialLevel");
        SceneManager.LoadScene("Level1");
    }
}
