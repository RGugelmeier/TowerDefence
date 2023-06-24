using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Load load or new game selection screen
    public void PlayButton()
    {
        SceneManager.LoadScene("LoadOrNew");
        AudioManager.audioManInstance.Play("ButtonPress");
    }

    //Load the level selected and set the next level to be loaded as the correct next level.
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
        AudioManager.audioManInstance.Play("ButtonPress");
        GameManager.gameManInstance.nextLevelNumber = levelNum;
        WaveManager.waveManInstance.currentLevelNum = levelNum;
        GameManager.gameManInstance.balance = 20.0f;
    }

    //Save the game and close the game.
    public void QuitGame()
    {
        SaveOrLoad.SaveGame(GameManager.gameManInstance);
        AudioManager.audioManInstance.Play("ButtonPress");
        Application.Quit();
    }

    //Go back to the previous screen. The scene name is in the game object of each back button
    public void BackButton(string sceneName)
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        SceneManager.LoadScene(sceneName);
    }

    //Load the game data and go to let the player select a level to start at.
    public void LoadGame()
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        if(File.Exists(Application.persistentDataPath + "/gameData.xml"))
        {
            SaveOrLoad.LoadGame();
            SceneManager.LoadScene("LevelSelection");
        }
    }

    //Check if a game has already been saved. If so, ask the player if they are sure they want to delete it and start new...
    //...If not, start a new game as usual.
    public void NewGame()
    {
        AudioManager.audioManInstance.Play("ButtonPress");

        if (File.Exists(Application.persistentDataPath + "/gameData.xml"))
        {
            GameObject.Find("ConfirmationScreen").GetComponent<Canvas>().enabled = true;

            GameManager.gameManInstance.upgradePoints = 0;
            GameManager.gameManInstance.highestLevelCompleted = 0;
            GameManager.gameManInstance.guardLevel = 0;
            GameManager.gameManInstance.puncherLevel = 1;
            GameManager.gameManInstance.archerLevel = 0;
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    //Delete the old save file and start the game
    public void YesNewGame()
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        File.Delete(Application.persistentDataPath + "/gameData.xml");
        SceneManager.LoadScene("Level1");
    }

    //Close the window that asks the player if they would like to start a new game, and nothing else.
    public void NoNewGame()
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        GameObject.Find("ConfirmationScreen").GetComponent<Canvas>().enabled = false;
    }

    //Open the controls screen from the main menu
    public void CheckControls()
    {
        GameObject.Find("ControlsScreen").GetComponent<Canvas>().enabled = true;
    }

    //Close the controls screen
    public void CloseControlsScreen()
    {
        if(GameObject.Find("ControlsScreen").GetComponent<Canvas>().enabled)
        {
            GameObject.Find("ControlsScreen").GetComponent<Canvas>().enabled = false;
        }
    }
}
