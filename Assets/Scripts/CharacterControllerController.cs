using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterControllerController : MonoBehaviour
{
    private BasicCharacterController basicCharacterController;
    private FlightController flightController;
    private SkatingController skatingController;
    [SerializeField] private Rigidbody rb;
    
    private BaseCharacterController activeCharacterController;

    private bool MovementEnabled = false;

    private Vector2 wishMove;
    private bool wishJump;

    void EnableMovement()
    {
        MovementEnabled = true;
    }
    
    void DisableMovement()
    {
        MovementEnabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        basicCharacterController = GetComponent<BasicCharacterController>();
        flightController = GetComponent<FlightController>();
        skatingController = GetComponent<SkatingController>();
        
        activeCharacterController = basicCharacterController;

        ServiceLocator.Instance.GetLevelManager().levelStartedDelegate += EnableMovement;
        ServiceLocator.Instance.GetLevelManager().levelEndedDelegate += DisableMovement;
    }

    // Update is called once per frame
    void Update()
    {
        wishMove.x = Input.GetAxisRaw("Horizontal");
        wishMove.y = Input.GetAxisRaw("Vertical");
        wishJump = Input.GetButtonDown("Jump") || wishJump;

        if (Input.GetButton("Switch1"))
        {
            activeCharacterController = basicCharacterController;
            rb.drag = 0;
        }
        if (Input.GetButton("Switch2"))
        {
            activeCharacterController = flightController;
            rb.drag = 5;
        }
        if (Input.GetButton("Switch3"))
        {
            activeCharacterController = skatingController;
        }
    }

    private void FixedUpdate()
    {
        if (MovementEnabled)
        {
            activeCharacterController.Move(wishMove, wishJump);
        }
        wishJump = false;
    }
}
