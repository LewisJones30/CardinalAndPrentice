using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() ///Triggered when the player walks into a coin
    {
        if (this.gameObject.tag == "Coin")
        {
            coinsCollected = coinsCollected + 1; //Grant the player another coin
            Destroy(this.gameObject); //Destroy the coin once it is completed
            UpdateCounter(); //Update the coin counter
        }
    }

    void UpdateCounter()
    {
        string display = "Coins: " + coinsCollected.ToString() + "/15";
        coinCounter.text = display;
        if (coinsCollected >= 15)
        {
            UnlockEndOfStage();
        }

    }

    void UnlockEndOfStage()
    {

    }
}
