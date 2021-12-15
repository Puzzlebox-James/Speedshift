using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GameObject IntroTimer;
    [SerializeField] private Text TimerText;
    
    void OnEnable()
    {
        ServiceLocator.Instance.GetLevelManager().introTimerStartDelegate += LevelStarted;
    }

    void LevelStarted()
    {
        IntroTimer.GetComponent<Animator>().SetTrigger("IntroTimerStarted");
        IntroTimer.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        TimerText.text = TimeSpan.FromSeconds(ServiceLocator.Instance.GetLevelManager().LevelTimer).ToString(@"mm\:ss\:ff");
    }
}
