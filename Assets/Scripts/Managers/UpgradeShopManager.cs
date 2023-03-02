using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeShopManager : MonoBehaviour
{
    GameManager gameMan;

    [SerializeField]
    private GameObject CurrentPoints, PuncherUpgradeCost, GuardUpgradeCost, ArcherUpgradeCost;
    private TMP_Text CurrentPointsText, PuncherUpgradeCostText, GuardUpgradeCostText, ArcherUpgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
        //Stop all audio.
        AudioManager.audioManInstance.Stop();
        AudioManager.audioManInstance.Play("MenuMusic");

        //Try to find a game manager.
        if(FindObjectOfType<GameManager>())
        {
            gameMan = FindObjectOfType<GameManager>();
        }
        else 
        { 
            Debug.LogError("No GameManager found in current scene!");
        }
        
        //Get the TextMexh components for all text thgat has to change during runtime.
        CurrentPointsText = CurrentPoints.GetComponent<TMP_Text>();
        PuncherUpgradeCostText = PuncherUpgradeCost.GetComponent<TMP_Text>();
        GuardUpgradeCostText = GuardUpgradeCost.GetComponent<TMP_Text>();
        ArcherUpgradeCostText = ArcherUpgradeCost.GetComponent<TMP_Text>();

        //Set initial values for text that changed during runtime.
        CurrentPointsText.text = gameMan.upgradePoints.ToString();
        PuncherUpgradeCostText.text = gameMan.puncherUpgradeCost.ToString();
        GuardUpgradeCostText.text = gameMan.guardUpgradeCost.ToString();
        ArcherUpgradeCostText.text = gameMan.archerUpgradeCost.ToString();
    }

    //Levels up a unit
    public void levelUp(string unitType)
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        //Check what button was pressed and make sure there are enough upgrade points to afford the upgrade...
        //...if there are enough points. Upgrade the unit by leveling up up. It's stats are changed in it's own class. For ex. UPuncher contains what happens when it levels up...
        //...Lastly, update the text to properly display the amopunt of upgrade points left after the upgrade.
        if (unitType == "Puncher" && gameMan.upgradePoints >= gameMan.puncherUpgradeCost)
        {
            gameMan.puncherLevel++;
            gameMan.upgradePoints -= gameMan.puncherUpgradeCost;
            CurrentPointsText.text = gameMan.upgradePoints.ToString();
            AudioManager.audioManInstance.Play("Purchase");
            return;
        }
        else if (unitType == "Guard" && gameMan.upgradePoints >= gameMan.guardUpgradeCost)
        {
            gameMan.guardLevel++;
            gameMan.upgradePoints -= gameMan.guardUpgradeCost;
            CurrentPointsText.text = gameMan.upgradePoints.ToString();
            AudioManager.audioManInstance.Play("Purchase");
            return;
        }
        else if (unitType == "Archer" && gameMan.upgradePoints >= gameMan.archerUpgradeCost)
        {
            gameMan.archerLevel++;
            gameMan.upgradePoints -= gameMan.archerUpgradeCost;
            CurrentPointsText.text = gameMan.upgradePoints.ToString();
            AudioManager.audioManInstance.Play("Purchase");
            return;
        }

        AudioManager.audioManInstance.Play("Error");
    }

    //Continue out of the upgrade shop. Loads the next level.
    public void Continue()
    {
        AudioManager.audioManInstance.Play("ButtonPress");
        SceneManager.LoadScene("Level" + gameMan.nextLevelNumber);
    }
}
