using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public float health;

    void Awake()
    {
        health = 100.0f;
    }

    void Update()
    {
    }

    public void CheckDeath()
    {
        if(health <= 0.0f)
        {
            Application.Quit();
            Debug.Log("Game Over.");
        }
    }
}
