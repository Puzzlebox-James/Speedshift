using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BasicCharacterController : BaseCharacterController
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;
    private float horizontal;
    private float vertical;
    
    void Start()
    {
        // technically not needed if we set it in the inspector
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector2 wishMove, bool wishJump)
    {
        // Check groundedness
        Vector3 groundRayOrigin = transform.position;
        groundRayOrigin.y += 1;

        float rayDistance = 1.0f + Mathf.Min(0.0f, rb.velocity.y * Time.fixedDeltaTime);
        
        if (Physics.Raycast(groundRayOrigin, Vector3.down, rayDistance, groundMask))
        {
            isGrounded = true;
        }
        
        Vector3 direction = new Vector3(wishMove.x,0f , wishMove.y).normalized;

        // player moves towards direction the camera is facing
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        direction = transform.TransformDirection(direction) * speed;
        rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);

        if (wishJump && isGrounded)
        { 
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        // gliding
        // TODO, reimplement this using CharacterControllerController inputs
        
        if (Input.GetButton("Fire1") && !isGrounded)
        {
            rb.drag = 5;
        }

        if (!Input.GetButton("Fire1") || isGrounded)
        {
            rb.drag = 0;
        }
    }
}
