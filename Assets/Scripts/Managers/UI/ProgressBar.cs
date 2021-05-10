using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    //The health amount.
    [SerializeField] private Image mask;

    //The game manager.
    private GameManager gameMan;

    private void Awake()
    {
        //Get the game manager. This is so we can access the player's health for the health bar.
        gameMan = FindObjectOfType<GameManager>();

        //Subscribe GetCurrentFill to the enemy's OnReachedEnd.
        BaseEnemy.OnReachedEnd += GetCurrentFill;
    }

    void GetCurrentFill(GameObject enemy)
    {
        float fillAmount = gameMan.health / gameMan.maxHealth;
        mask.fillAmount = fillAmount;
    }
}
