using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The player's health.
    [HideInInspector] public float health;

    void Awake()
    {
        BaseEnemy.OnReachedEnd += CheckDeath;

        health = 1.0f;
    }

    //Checks if the player's heal;th has dropped to or below zero. If it has, end the game.
    public void CheckDeath()
    {
        if(health <= 0.0f)
        {
            Application.Quit();
            Debug.Log("Game Over.");
        }
    }
}
