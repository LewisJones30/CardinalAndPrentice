using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCreator : MonoBehaviour
{

    string characterName;

    // Start is called before the first frame update
    void Awake()
    {
        Controller p1 = FindObjectOfType<CardinalController>();

        if (p1 == null)
        {
            gameObject.AddComponent<CardinalController>();
            characterName = "Cardinal";
        }
        else
        {
            gameObject.AddComponent<PrenticeController>();
            characterName = "Prentice";
        }

        GetComponent<PlayerInput>().defaultActionMap = characterName;
        var setup = FindObjectOfType<ControllerSetup>();
        if (setup != null) setup.SetStatus(characterName, ControllerStatus.Connected);

        Destroy(this);
    }
}
