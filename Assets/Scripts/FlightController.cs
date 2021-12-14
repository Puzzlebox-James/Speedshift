using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will inherit from a base controller, presumably a monobehavior
public class FlightController : BaseCharacterController
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private float POWER;

    private Vector3 forwardForce = Vector3.forward;
    
    private Vector3 movementDirection;
    // Update is called once per frame
    void Update()
    {
        // Player Movement Update Calls
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    public override void Move(Vector2 wishMove, bool wishJump)
    {
        playerGameObject.GetComponent<Rigidbody>().AddForce(wishMove * Time.deltaTime * POWER, ForceMode.Impulse);
        
    }
}
