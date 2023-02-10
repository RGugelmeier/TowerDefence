using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    //The text that holds the player's current balance.
    [SerializeField] private Text errorText;
    [SerializeField] private Canvas endLevelUI;
    private Text balanceVal;

    //The game manager.
    private GameManager gameMan;

    float errorMsgTime;

    //Set default HUD values.
    void Start()
    {
        //Subscribe update bal to the game manager's OnUpdateBal event. This changes the displayed player balance.
        GameManager.OnUpdateBal += UpdateBalance;
        Controller.OnPlayerError += DisplayErrorMessage;
        WaveManager.OnLevelEnd += ShowLevelEnd;

        //Reference to game manager.
        gameMan = FindObjectOfType<GameManager>();

        //Set the initial player balance.
        balanceVal = GameObject.Find("BalanceVal").GetComponent<Text>();
        balanceVal.text = gameMan.balance.ToString();
        endLevelUI.enabled = false;
    }

    private void Update()
    {
        //If an error message is displayed, start ticking timer to turn it off.
        if (errorMsgTime <= 0.0f)
        {
            if(errorText != null)
            {
                errorText.gameObject.SetActive(false);
            }
        }
        else
        {
            errorMsgTime -= Time.deltaTime;
        }
    }

    //Shows the player's proper balance. Updates when the player's balance changes.
    void UpdateBalance()
    {
        if(balanceVal != null)
        {
            balanceVal.text = gameMan.balance.ToString();
        }
    }

    //Change and activate error message.
    void DisplayErrorMessage(string errorMsg)
    {
        errorMsgTime = 3.0f;
        if(errorText != null)
        {
            errorText.text = errorMsg;
            errorText.gameObject.SetActive(true);
        }
    }

    //Enables the end level UI.
    void ShowLevelEnd(int currentLevel)
    {
        if(this != null)
        {
            endLevelUI.enabled = true;
        }
    }
}
