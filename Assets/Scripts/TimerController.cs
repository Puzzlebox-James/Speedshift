using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    [SerializeField] private Text timeCounterText;
    

    private TimeSpan timePlaying;
    public bool timerRunning;
    public float elapsedTime;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        elapsedTime = 0f;
        timerRunning = true;
        new Task(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("m':'ss'.'f");
            timeCounterText.text = timePlayingStr;

            yield return null;
        }
    }
}
