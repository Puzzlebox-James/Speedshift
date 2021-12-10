using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BasicCharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform cam;

    private bool isGrounded;
    private float horizontal;
    private float vertical;
    
    void Start()
    {
        // technically not needed if we set it in the inspector
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f , vertical).normalized;

        // player moves towards direction the camera is facing
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        direction = transform.TransformDirection(direction) * speed;
        rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);

        if (Input.GetButtonDown("Jump") && isGrounded)
        { 
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        // gliding

        if (Input.GetButton("Fire1") && !isGrounded)
        {
            rb.drag = 5;
        }

        if (!Input.GetButton("Fire1") || isGrounded)
        {
            rb.drag = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }*/
}