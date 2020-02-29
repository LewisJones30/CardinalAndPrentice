using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Controller controller = FindObjectOfType<Controller>();

        if (controller != null) gameObject.AddComponent<CardinalController>();
        else gameObject.AddComponent<PrenticeController>();

        Destroy(this);
    }
}
