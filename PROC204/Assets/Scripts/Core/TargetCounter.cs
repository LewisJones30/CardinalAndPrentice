using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCounter : MonoBehaviour
{

    [SerializeField] GameObject TargetGate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;
        if (childCount == 1)
        {
            TargetGate.GetComponent<Animator>().SetTrigger("openGate");
            Destroy(this.gameObject); //Prevents it from running trigger multiple times
        }
    }
}
