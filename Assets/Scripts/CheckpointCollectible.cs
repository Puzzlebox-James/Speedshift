using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCollectible : TimedCollectible
{
    
    //Testing UI thing to see time and make sure it's all working nice
    [SerializeField] private Text GatherTime;
    private TimeSpan timeCollectedSpan;
    
    protected override void PickUp(GameObject bumpedObject)
    {
        TimeUntilGot(Time.time);
        
        timeCollectedSpan = TimeSpan.FromSeconds(timeCollected);
        string timeCollectedStr = timeCollectedSpan.ToString("m':'ss'.'f");
        GatherTime.text = timeCollectedStr;

        Destroy(bumpedObject);
    }

    private void OnDisable()
    {
        
    }
}
