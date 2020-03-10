using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrenticeController : Controller
{
    RangedWeapon rangedWeapon;
    Shield shield;

    Vector2 aimInput;
    private float horizontalRotation = 0.0f;
    private float verticalRotation = 0.0f;
    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Prentice Controller";
    }

    public void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    public void OnFire()
    {
        rangedWeapon.Fire();


        //bool fire = callbackContext.ReadValue<bool>();

        //callbackContext.phase;

    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 2");
        if (character == null) return;

        transform.parent = character.transform;
        rangedWeapon = character.GetComponent<RangedWeapon>();
        shield = character.GetComponent<Shield>();
    }

    private void Update()
    {
        //if (!CanControl()) return;

            //rangedWeapon.Aim(aimInput);
            //shield.Protect(aimInput);
        if (Gamepad.all[0].buttonSouth.isPressed == true) //A button on Xbox Controller, X on PS4
        {
            Debug.Log("Fired!!");
            rangedWeapon.Fire();
            
        }
        else if (Gamepad.all[1].buttonEast.isPressed == true) //B button on Xbox controller, Circle on PS4
        {
            //Code to be added
        }
        else if (Gamepad.all[1].buttonNorth.isPressed == true) //Y button on Xbox controller, Triangle on PS4
        {
            //Code to be added
        }
        else if (Gamepad.all[1].buttonWest.isPressed == true) //X button on Xbox controller
        {
            //Code to be added
        }
        else if (Gamepad.all[1].leftStick.IsPressed()) //Shield controller, left joystick on Xbox/PS4
        {
            horizontalRotation = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Vertical") < 0) //Ensures shield does not go through floor, potentially could work both ways?
            {
                verticalRotation = 0;
            }
            else
            {
                verticalRotation = Input.GetAxis("Vertical"); 
            }
            shield.Protect(new Vector2(horizontalRotation, verticalRotation)); //Invoke shield spawner
        }


    }
}
