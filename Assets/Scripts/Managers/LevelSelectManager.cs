using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelSelectButtons;
    
    //Set only levels that have been reached to have active buttons to load that level. This prevents players from playing levels they have not beaten before.
    void Start()
    {
        for(int i = 0; i <= GameManager.gameManInstance.highestLevelCompleted ; i++)
        {
            levelSelectButtons[i].SetActive(true);
        }
    }
}
