using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    //The health amount.
    [SerializeField] private Image mask;

    //The game manager.
    //private GameManager gameMan;

    private void Awake()
    {
        //Get the game manager. This is so we can access the player's health for the health bar.
        //gameMan = FindObjectOfType<GameManager>();

        //Subscribe GetCurrentFill to the enemy's OnReachedEnd.
        BaseEnemy.OnReachedEnd += GetCurrentFill;
    }

    void GetCurrentFill(GameObject enemy)
    {
        //mask and this are null. Find out why. Maybe GameManager needs to get a new reference to this each new scene, as it is a new health bar each level?
        if(this.mask != null)
        {
            float fillAmount = GameManager.gameManInstance.health / GameManager.gameManInstance.maxHealth;
            mask.fillAmount = fillAmount;
        }
    }
}
