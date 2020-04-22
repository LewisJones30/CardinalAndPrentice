﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsScript : MonoBehaviour
{
    public int coinsCollected = 0;
    Text coinCounter;
    void Start()
    {
        coinCounter = GameObject.Find("Coins Text").GetComponent<Text>();
        if (this.gameObject.name == "coins")
        {
            PlayerPrefs.SetInt("LevelEndUnlocked", 0); //Reset unlock
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() ///Triggered when the player walks into a coin
    {
        if (this.gameObject.tag == "Coin")
        {
            CoinsScript parentScript = GameObject.Find("coins").GetComponentInParent<CoinsScript>();
            parentScript.UpdateCounter();
            Destroy(this.gameObject);
        }
    }

    void UpdateCounter()
    {
        coinsCollected = coinsCollected + 1;
        string display = "Coins: " + coinsCollected.ToString() + "/15";
        coinCounter.text = display;
        if (coinsCollected >= 15)
        {
            UnlockEndOfStage();
            GameObject NotEnoughCoins = GameObject.Find("NotEnoughCoins");
            Text text = NotEnoughCoins.GetComponent<Text>();
            text.enabled = false;
        }

    }

    void UnlockEndOfStage()
    {
        PlayerPrefs.SetInt("LevelEndUnlocked", 1);
    }
}
