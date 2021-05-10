using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The player's health and money balance.
    [HideInInspector] public float health, maxHealth, balance;
    //Holds all of the units a player has currently created.
    public List<UnitBase> aliveUnits;

    public static Action OnUpdateBal;

    private void Awake()
    {
        //Subscribe events.
        BaseEnemy.OnReachedEnd += CheckDeath;
        BaseEnemy.OnDie += IncreaseMoney;
        UnitBase.OnCreation += DecreaseMoney;

        //Set default values.
        maxHealth = 100.0f;
        health = maxHealth;
        balance = 10.0f;
    }

    //Checks if the player's heal;th has dropped to or below zero. If it has, end the game.
    public void CheckDeath(GameObject enemy)
    {
        if(health <= 0.0f)
        {
            Application.Quit();
            Debug.Log("Game Over.");
        }
    }

    //Add money to the player's balance.
    private void IncreaseMoney(GameObject enemy)
    {
        balance += enemy.GetComponent<BaseEnemy>().worth;

        if (OnUpdateBal != null)
            OnUpdateBal();
    }

    //Take money from the player's balance.
    private void DecreaseMoney(GameObject unit)
    {
        balance -= unit.GetComponent<UnitBase>().cost;

        if (OnUpdateBal != null)
            OnUpdateBal();
    }
}
