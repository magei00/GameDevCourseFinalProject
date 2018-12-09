﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameControllerScript : MonoBehaviour {


    private int coins=0;
    private int savedCoins = 0;
    public Text coinText;
    public bool dash_unlocked = false;
    public bool gravity_unlocked = false;
    public bool fourth_unlocked = false;


    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
        }
        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<Text>();
        DontDestroyOnLoad(this);

       
    }
    
    //checks whether a certain character (indicated by the character index) is unlocked and returns the appropriate boolean value. 
    public bool IsCharacterUnlocked(int character_index)
    {
        bool value = false;
        switch(character_index)
        {
            case 1:
                value = dash_unlocked;
                break;
 
            case 2:
                value = gravity_unlocked;
                break;
            case 3:
                value = fourth_unlocked;
                break;
            default:
                value = dash_unlocked;
                break;
        }
        return value;
    }

    void OnLevelWasLoaded()
    {
        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<Text>();
        UpdateCoinText();
    }

    // Update is called once per frame
    void Update () {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins Collected: " + coins;
    }

    public void IncrementCoin(int i)
    {
        coins += i;
    }

    public void DecrementCoin(int i)
    {
        coins -= i;
    }

    public void SaveCoins()
    {
        savedCoins = coins;
    }

    public void ResetCoins()
    {
        coins = savedCoins;
    }

    public int GetCoins()
    {
        return coins;
    }
}
