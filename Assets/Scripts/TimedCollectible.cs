using System;
using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;

public class TimedCollectible : Collectible
{
    private float lastHit; // Thing to make sure the player with their multiple colliders (?) don't pick up the same thing twice.

    private float initializationTime;
    

    protected float timeCollected;

    private void Start()
    {
        initializationTime = Time.time;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // Theoretically anything can pick up this collectible. Do logic here if we want to check to make only certain things able to pick it up
        // Also the player might have multiple colliders, so make sure to only pickup the thing once
        if (Time.time - lastHit < .1) // MAGIC NUMBER ALERT WEEWOO
            return;
        lastHit = Time.time;
        
        PickUp(this.gameObject);
    }

    protected override void PickUp(GameObject bumpedObject)
    {
        Destroy(bumpedObject);
        TimeUntilGot(Time.time);
    }

    protected void TimeUntilGot(float timegot)
    {
        timeCollected = timegot - initializationTime;
    }

}