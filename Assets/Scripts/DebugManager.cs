using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    //Script that has function s that make debugging easier. Uncomment all when debugging.
    //MAKE SURE THIS IS NOT IN ANY SCENE BEFORE PACKAGING

    //private void Start()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    //
    //private void Update()
    //{
    //    //PgUp will skip the wave
    //    if(Input.GetKeyUp(KeyCode.PageUp))
    //    {
    //        EnemyPool enemyPoolRef = FindObjectOfType<EnemyPool>();
    //        foreach(BaseEnemy enemy in GameManager.gameManInstance.aliveEnemies)
    //        {
    //            enemyPoolRef.Return(enemy.gameObject);
    //        }
    //        WaveManager.waveManInstance.EndWave();
    //    }
    //    //PgDwn will skip the level
    //    else if(Input.GetKeyUp(KeyCode.PageDown))
    //    {
    //        EnemyPool enemyPoolRef = FindObjectOfType<EnemyPool>();
    //        foreach (BaseEnemy enemy in GameManager.gameManInstance.aliveEnemies)
    //        {
    //            enemyPoolRef.Return(enemy.gameObject);
    //        }
    //        WaveManager.waveManInstance.OnNoMoreWaves();
    //    }
    //    else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Equals))
    //    {
    //        GameManager.gameManInstance.timeScale += 5.0f;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Minus))
    //    {
    //        GameManager.gameManInstance.timeScale -= 0.1f;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Backslash))
    //    {
    //        GameManager.gameManInstance.timeScale = 1.0f;
    //    }
    //    else if(Input.GetKeyDown(KeyCode.Equals))
    //    {
    //        GameManager.gameManInstance.upgradePoints = 999999;
    //    }
    //}
}
