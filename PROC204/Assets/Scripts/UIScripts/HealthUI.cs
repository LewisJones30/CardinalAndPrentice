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
    // Start is called before the first frame update
    void Start()
    {
        
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
        Image currentSprite = GetComponent<Image>();
        var currentSpriteName = currentSprite.sprite.name;
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
            currentSprite.enabled = false;
        }
    }
    public void gameOver() //Called by health if cardinal dies
    { 
        GameObject gameoverCanvas = GameObject.Find("GameOver Canvas");
        gameoverCanvas.GetComponent<Canvas>().enabled = true; //Set the gameover canvas to be true
        playerDead = true;

    }
}
