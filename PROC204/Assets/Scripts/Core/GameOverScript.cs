using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverScript : MonoBehaviour
{
    GameObject gameOverObject;
    Canvas gameOverCanvas;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverObject = GameObject.Find("GameOverCanvas");
        gameOverCanvas = gameOverObject.GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            if (Gamepad.all[0].buttonSouth.isPressed == true)
            {
                //Restart the game
                gameOver = false;
            }
        }
        else
        {

        }
    }

    public void onGameOver()
    {
        gameOverCanvas.enabled = true;
        gameOver = true;
        //Code to stop background physics

    }
}
