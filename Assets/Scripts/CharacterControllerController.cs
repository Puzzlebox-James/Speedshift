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
    
    private BaseCharacterController activeCharacterController;

    private Vector2 wishMove;
    private bool wishJump;

    // Start is called before the first frame update
    void Start()
    {
        basicCharacterController = GetComponent<BasicCharacterController>();
        flightController = GetComponent<FlightController>();
        skatingController = GetComponent<SkatingController>();
        
        activeCharacterController = basicCharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        wishMove.x = Input.GetAxisRaw("Horizontal");
        wishMove.y = Input.GetAxisRaw("Vertical");
        wishJump = Input.GetButtonDown("Jump") || wishJump;
    }

    private void FixedUpdate()
    {
        activeCharacterController.Move(wishMove, wishJump);
        wishJump = false;
    }
}
