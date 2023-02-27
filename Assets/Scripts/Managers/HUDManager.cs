using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    //The text that holds the player's current balance.
    [SerializeField] private Text errorText;
    [SerializeField] private Canvas endLevelUI;
    private Text balanceVal;

    //Reference to thge button that starts the next wave.
    [SerializeField] private Button startWaveButton;

    //Get references to needed managers
    private GameManager gameMan;
    private WaveManager waveMan;

    float errorMsgTime;

    //Set default HUD values.
    void Start()
    {
        //Reference to game manager.
        gameMan = FindObjectOfType<GameManager>();
        waveMan = FindObjectOfType<WaveManager>();

        //Set the selection image of each unit to be black to indicate they have not yet been unlocked
        if (gameMan.archerLevel == 0)
        {
            GameObject.Find("Archer").GetComponent<Image>().color = Color.black;
        }
        if (gameMan.guardLevel == 0)
        {
            GameObject.Find("Guard").GetComponent<Image>().color = Color.black;
        }

        //Subscribe update bal to the game manager's OnUpdateBal event. This changes the displayed player balance.
        GameManager.OnUpdateBal += UpdateBalance;
        Controller.OnPlayerError += DisplayErrorMessage;
        WaveManager.OnLevelEnd += ShowLevelEnd;

        //Set the initial player balance.
        balanceVal = GameObject.Find("BalanceVal").GetComponent<Text>();
        balanceVal.text = gameMan.balance.ToString();
        endLevelUI.enabled = false;

        startWaveButton.onClick.AddListener(waveMan.StartWave);
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
