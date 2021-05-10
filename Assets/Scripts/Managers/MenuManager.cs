using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
    }
}
