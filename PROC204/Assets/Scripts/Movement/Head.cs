using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float slipSpeed = 5f;

    //STATES
    float heightOffset;

    //CACHE REFERENCES
    CharacterController charController;

    private void Awake()
    {
        charController = GetComponentInParent<CharacterController>();
        heightOffset = charController.height / 2f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (transform.parent == other.transform) return; //Prevents detecting own transform     

        var charPhysics = other.gameObject.GetComponent<CharacterPhysics>();
        if (charPhysics == null) return;

        float otherHeight = other.gameObject.transform.position.y;
        float headHeight = transform.parent.position.y + heightOffset;

        if (otherHeight < headHeight) return; //Other character's feet must be above this character's haed to apply head slippage

        charPhysics.SlipMove(slipSpeed + charController.velocity.x);
    }

    //Stops adding slip force when other characters leaves head zone
    private void OnTriggerExit(Collider other)
    {
        var charPhysics = other.gameObject.GetComponent<CharacterPhysics>();
        if (charPhysics == null) return;

        charPhysics.FinishSlip();
    }
}
