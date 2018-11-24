using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameControllerScript : MonoBehaviour {


    private int coins=0;
    private int savedCoins = 0;
    public Text coinText;


    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
        }
        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<Text>();
        DontDestroyOnLoad(this);

        
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

    public void SaveCoins()
    {
        savedCoins = coins;
    }

    public void ResetCoins()
    {
        coins = savedCoins;
    }
}
