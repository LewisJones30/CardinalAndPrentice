using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ControllerScreenControl : MonoBehaviour
{
    public UnityEvent southPress, eastPress, northPress, westPress, startPress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all[0].buttonSouth.isPressed == true)
        {
            southPress.Invoke();
        }
        else if (Gamepad.all[0].buttonEast.isPressed == true)
        {
            eastPress.Invoke();
        }
        else if (Gamepad.all[0].buttonNorth.isPressed == true)
        {
            northPress.Invoke();
        }
        else if (Gamepad.all[0].buttonWest.isPressed == true)
        {
            westPress.Invoke();
        }
        else if (Gamepad.all[0].startButton.isPressed == true)
        {
            startPress.Invoke();
        }
    }
}
