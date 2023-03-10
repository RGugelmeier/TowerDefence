using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelSelectButtons;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= GameManager.gameManInstance.highestLevelCompleted; i++)
        {
            levelSelectButtons[i].SetActive(true);
        }
    }
}
