using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatingController : BaseCharacterController
{
    [Header("Scene References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [Header("General Movement Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSimpleVelocity;
    [SerializeField] private float jumpForce;
    [Header("Skating Movement Variables")]
    [SerializeField] private float skateSpeed;
    [Tooltip("MAKE SURE TO HAVE THIS VALUE MATCH THE MAX CURVE LENGTH")]
    [SerializeField] private float skateTimeLength = 1;
    [Tooltip("BETWEEN 0 and skateTimeLength! (x)")]
    [SerializeField] private AnimationCurve skateTimingCurve;
    [Tooltip("The direction offset of skating")]
    [SerializeField] private float skateBoostOffset;


    private bool isGrounded;

    private bool skateCRRunning;
    private bool skated;
    private bool skatedRight;
    private float skateVelocityImpact;
    

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Skate();
        }
    }

    public override void Move(Vector2 wishMove, bool wishJump)
    {
        Vector3 direction = new Vector3(wishMove.x,0f , wishMove.y).normalized;

        // player moves towards direction the camera is facing
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        direction = transform.TransformDirection(direction) * speed;
        
        if(!(rb.velocity.magnitude > maxSimpleVelocity))
            rb.AddForce(new Vector3(direction.x, 0, direction.z), ForceMode.Impulse);

        if (wishJump && isGrounded)
        { 
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (skated)
        {
            if (skatedRight)
            {
                skatedRight = !skatedRight;
                Vector3 skateDirection = new Vector3(skateBoostOffset, 0, skateVelocityImpact);
                skateDirection = transform.TransformDirection(skateDirection);
                rb.AddForce(skateDirection, ForceMode.Impulse);
                skated = false;
            }
            else
            {
                skatedRight = !skatedRight;
                rb.AddForce(new Vector3(direction.x + -skateBoostOffset, 0, direction.z + (skateVelocityImpact * skateSpeed)), ForceMode.Impulse);
                skated = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Skate()
    {
        //check if 'skatecurve' coroutine is running, get the value from it, then kill it.
        //  -if not, then kick it off, and use a 'maxSkateCurve' value.
        //Apply the value as a forward force (can be negative), flip floping each itteration.
        if (!skateCRRunning)
        {
            skated = true;
            new Task(Skating());
            skateVelocityImpact = skateTimingCurve.Evaluate(.6f);
        }
        else
        {
            skated = true;
            StopCoroutine(Skating());
        }
    }

    private IEnumerator Skating()
    {
        skateCRRunning = true;
        
        var startTime = Time.time;
        while (Time.time < startTime + skateTimeLength)
        {
            skateVelocityImpact = skateTimingCurve.Evaluate(Mathf.Lerp(0, skateTimeLength, Mathf.Abs(Time.time - (startTime + skateTimeLength))));
            yield return null;
        }
    }

}
