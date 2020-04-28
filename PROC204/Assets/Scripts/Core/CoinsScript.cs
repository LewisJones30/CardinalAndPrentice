using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinsScript : MonoBehaviour
{
    public int coinsCollected = 0;
    Text coinCounter;
    bool level1 = false;
    [SerializeField] GameObject CoinGateUnlock;
    void Start()
    {
        coinCounter = GameObject.Find("Coins Text").GetComponent<Text>();
        if (this.gameObject.name == "coins")
        {
            PlayerPrefs.SetInt("LevelEndUnlocked", 0); //Reset unlock
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            level1 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) ///Triggered when the player walks into a coin
    {
        if (other.gameObject.GetComponent<PrenticeProjectile>() != null)
        {
            CoinsScript parentScript = GameObject.Find("coins").GetComponentInParent<CoinsScript>();
            parentScript.UpdateCounter();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.transform.parent.name == "Cardinal")
        {
            CoinsScript parentScript = GameObject.Find("coins").GetComponentInParent<CoinsScript>();
            parentScript.UpdateCounter();
            Destroy(this.gameObject);
        }
    }

    void UpdateCounter()
    {
        string display;
        coinsCollected = coinsCollected + 1;
        if (level1 == true)
        {
             display = "Coins: " + coinsCollected.ToString() + "/15";
        }
        else //Level 2 display
        {
             display = "Coins: " + coinsCollected.ToString() + "/25";
        }
        coinCounter.text = display;
        if (coinsCollected >= 15 && level1 == true) //Level 1 check
        {
            UnlockEndOfStage();
            GameObject NotEnoughCoins = GameObject.Find("NotEnoughCoins");
            Text text = NotEnoughCoins.GetComponent<Text>();
            text.enabled = false;
        }
        else if (coinsCollected >= 25)
        {
            UnlockEndOfStage();
            GameObject NotEnoughCoins = GameObject.Find("NotEnoughCoins");
            Text text = NotEnoughCoins.GetComponent<Text>();
            text.enabled = false;
        }

    }

    void UnlockEndOfStage()
    {
        if (level1 == true)
        {
            PlayerPrefs.SetInt("LevelEndUnlocked", 1); //Unlock end of level 1
            CoinGateUnlock.GetComponent<Animator>().SetTrigger("openGate");
        }
        else
        {
            PlayerPrefs.SetInt("Level2EndUnlocked", 1); //Unlock end of level 2
        }

    }
}
