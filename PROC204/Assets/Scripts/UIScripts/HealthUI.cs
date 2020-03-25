using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{

    public Sprite threeHearts, twoHearts, oneHeart, twoHalfHearts, oneHalfHearts, zeroHalfHearts;
    bool playerDead = false; //Used to detect if the player is dead, for the canvas to appear.
    public Image currentSprite;
    public string currentSpriteName;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<Image>();
        currentSpriteName = currentSprite.sprite.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead == true)
        {
            if (Gamepad.all[0].buttonSouth.isPressed == true)
            {
                SceneManager.LoadScene("Level 1");
                //Restart the scene
            }
            else if (Gamepad.all[0].startButton.isPressed == true)
            { 
                //Transport to the main menu, currently not available as this is not a scene.
            }
        }
    }


    public void TakeHealthUpdate()
    {
        currentSpriteName = currentSprite.sprite.name;

        if (currentSpriteName.ToString() == "threeHearts")
        {
            currentSprite.overrideSprite = twoHalfHearts;
            currentSprite.sprite = twoHalfHearts;
        }
        else if (currentSpriteName.ToString() == "twoHalfHearts")
        {
            currentSprite.overrideSprite = twoHearts;
            currentSprite.sprite = twoHearts;
        }
        else if (currentSpriteName.ToString() == "twoHearts")
        {
            currentSprite.overrideSprite = oneHalfHearts;
            currentSprite.sprite = oneHalfHearts;
        }
        else if (currentSpriteName.ToString() == "oneHalfHearts")
        {
            currentSprite.overrideSprite = oneHeart;
            currentSprite.sprite = oneHeart;
        }
        else if (currentSpriteName.ToString() == "oneHeart")
        {
            currentSprite.overrideSprite = zeroHalfHearts;
            currentSprite.sprite = zeroHalfHearts;
        }
        else
        {
            //Default case
            Debug.Log("Error. Default case triggered in TakeHealthUpdate.");
        }
    }

    public void AddHalfHeart() //This is if the player heals by half a heart.
    {
        if (currentSpriteName.ToString() == "threeHearts")
        {
            //No healing takes place, player is at maximum health.
        }
        else if (currentSpriteName.ToString() == "twoHalfHearts")
        {
            currentSprite.overrideSprite = threeHearts;
            currentSprite.sprite = threeHearts;
        }
        else if (currentSpriteName.ToString() == "twoHearts")
        {
            currentSprite.overrideSprite = twoHalfHearts;
            currentSprite.sprite = twoHalfHearts;
        }
        else if (currentSpriteName.ToString() == "oneHalfHearts")
        {
            currentSprite.overrideSprite = twoHearts;
            currentSprite.sprite = twoHearts;
        }
        else if (currentSpriteName.ToString() == "oneHeart")
        {
            currentSprite.overrideSprite = oneHalfHearts;
            currentSprite.sprite = oneHalfHearts;
        }
        else if (currentSpriteName.ToString() == "zeroHalfHearts")
        {
            currentSprite.overrideSprite = oneHeart;
            currentSprite.sprite = oneHeart;
        }
        else
        {
            //Default case
            Debug.Log("Error. Default case triggered in AddHalfHeart.");
        }
    }
    public void AddOneHeart() //This method is used if the player heals 1 heart. The cap is currently 3 hearts.
    {
        if (currentSpriteName.ToString() == "threeHearts")
        {
            //No healing takes place, player is at maximum health.
        }
        else if (currentSpriteName.ToString() == "twoHalfHearts")
        {
            //Player can only go to three hearts. Therefore this still only adds half a heart.
            currentSprite.overrideSprite = threeHearts;
            currentSprite.sprite = threeHearts;
        }
        else if (currentSpriteName.ToString() == "twoHearts")
        {
            currentSprite.overrideSprite = threeHearts;
            currentSprite.sprite = threeHearts;
        }
        else if (currentSpriteName.ToString() == "oneHalfHearts")
        {
            currentSprite.overrideSprite = twoHalfHearts;
            currentSprite.sprite = twoHalfHearts;
        }
        else if (currentSpriteName.ToString() == "oneHeart")
        {
            currentSprite.overrideSprite = twoHearts;
            currentSprite.sprite = twoHearts;
        }
        else if (currentSpriteName.ToString() == "zeroHalfHearts")
        {
            currentSprite.overrideSprite = oneHalfHearts;
            currentSprite.sprite = oneHalfHearts;
        }
        else
        {
            //Default case
            Debug.Log("Error. Default case triggered in AddHalfHeart.");
        }
    }
    public void gameOver() //Called by health if cardinal dies
    { 
        GameObject gameoverCanvas = GameObject.Find("GameOver Canvas");
        gameoverCanvas.GetComponent<Canvas>().enabled = true; //Set the gameover canvas to be true
        playerDead = true;

    }
}
