using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class checkControllers : MonoBehaviour
{
    //Booleans to check if the controllers are ready
     bool controller1Ready = false;
     bool controller2Ready = false;
    [SerializeField]
    private Button startButton;
    public Text controller1Text, controller2Text, startupText;
    StartupScreenInput controllerInput1;
    void Start()
    {

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
        }

        if (Gamepad.all.ToArray().Length < 2)
        {
            controller2Text.text = "Controller 2 not detected!";
            controller2Text.color = Color.red;
            startButton.interactable = false;
            return;
        }
        else
        {
            controller2Text.text = "Press A on Controller 2!";
        }
        if (Gamepad.all.ToArray().Length == 2)
        {
            startButton.interactable = true;
        }
        if (Gamepad.all[1].buttonSouth.isPressed == true)
        {
            controller2Text.color = Color.green;
        }
        else
        {
            controller2Text.color = Color.black;
        }
    }

    private void PlayerA_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnPlayerA()
    {
        if (controller1Ready == true)
        {
            controller1Text.color = Color.green;
            controller1Ready = false;
        }
        else
        {
            controller1Text.color = Color.black;
            controller1Ready = true;
        }

    }
}
