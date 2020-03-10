using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class checkControllers : MonoBehaviour
{
    //Booleans to check if the controllers are ready
     bool controller1Ready = false;
     bool controller2Ready = false;
    public Text controller1Text, controller2Text, startupText, pressStartText;
    void Start()
    {
        //Hide the text until it is enabled within methods.
        pressStartText.enabled = false;
    }
    private void Awake()
    {
        //controllerInput1 = new StartupScreenInput();

        //controllerInput1.checkInput.playerA.performed += ctx => controller1Ready = ctx.ReadValue<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        startupText.text = "Controllers detected: " + Gamepad.all.ToArray().Length; //Get controller count
        if (Gamepad.all.ToArray().Length == 0)
        {
            controller1Text.text = "Controller 1 not detected!";
            controller1Text.color = Color.red;
            controller2Text.text = "Controller 2 not detected!";
            controller2Text.color = Color.red;
            controller1Ready = false; //Reset the controller 1 ready status
            return;
        }

        else
        {
            controller1Text.text = "Press A on Controller 1!";
            controller1Text.color = Color.black;
        }
        if (Gamepad.all[0].buttonSouth.isPressed == true)
        {
            controller1Text.color = Color.green;
            controller1Ready = true;
        }
        if (controller1Ready == true)
        {
            controller1Text.text = "Cardinal is connected!";
        }
        if (Gamepad.all.ToArray().Length < 2)
        {
            controller2Text.text = "Controller 2 not detected!";
            controller2Text.color = Color.red;
            pressStartText.enabled = false;
            controller2Ready = false;
            return;
        }
        else
        {
            controller2Text.text = "Press A on Controller 2!";
        }
        if (Gamepad.all[1].buttonSouth.isPressed == true)
        {
            controller2Text.color = Color.green;
            controller2Ready = true;
            controller2Text.text = "Prenice is connected!";
        }
        else
        {
            controller2Text.color = Color.black;
        }
        if (controller1Ready == true && controller2Ready == true)
        {
            pressStartText.enabled = true;
            startGame();
        }
        else
        {
            pressStartText.enabled = false;
        }

        if (controller2Ready == true)
        {
            controller2Text.text = "Prentice is connected!";
        }
        
    }

    private void PlayerA_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void startGame()
    {
        if (Gamepad.all[0].startButton.isPressed == true || Gamepad.all[1].startButton.isPressed == true)
        {
            //Move to the next scene.
            Debug.Log("Horray!");
            SceneManager.LoadScene("Game Mechanic");

        }
    }
}
