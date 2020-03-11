using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    bool gameOver = false;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }


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
    }

    public void onGameOver()
    {
        gameOverCanvas.enabled = true;
        gameOver = true;
        //Code to stop background physics

    }
}
