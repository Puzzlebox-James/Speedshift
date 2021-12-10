using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatingController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxSimpleVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform cam;

    private bool isGrounded;
    private float horizontal;
    private float vertical;
    private bool jumped;


    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumped = true;
        }
        
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(horizontal,0f , vertical).normalized;

        // player moves towards direction the camera is facing
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        direction = transform.TransformDirection(direction) * speed;
        
        if(!(rb.velocity.magnitude > maxSimpleVelocity))
            rb.AddForce(new Vector3(direction.x, 0, direction.z), ForceMode.Impulse);

        if (jumped)
        { 
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumped = false;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
}
