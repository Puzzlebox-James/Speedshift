using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float introTimerLength;

    private Coroutine updateTimeCoroutine;

    public delegate void OnIntroTimerStart();

    public OnIntroTimerStart introTimerStartDelegate;
    
    public delegate void OnLevelStarted();

    public OnLevelStarted levelStartedDelegate;
    
    public delegate void OnLevelEnded();

    public OnLevelEnded levelEndedDelegate;
    
    private bool _levelStarted = false;
    public bool LevelStarted
    {
        get
        {
            return _levelStarted;
        }
        
        private set
        {
            if (_levelStarted == false && value == true)
            {
                _levelStarted = value;
                levelStartedDelegate?.Invoke();
                updateTimeCoroutine = StartCoroutine(UpdateTime());
            }
            
            if (_levelStarted == true && value == false)
            {
                _levelStarted = value;
                levelEndedDelegate?.Invoke();
                StopCoroutine(updateTimeCoroutine);
            }
        }
    }

    public float LevelTimer
    {
        get;
        private set;
    }

    IEnumerator UpdateTime()
    {
        while (_levelStarted)
        {
            LevelTimer += Time.deltaTime; 
            yield return null;
        }
    }

    IEnumerator StartLevel()
    {
        introTimerStartDelegate?.Invoke();

        yield return new WaitForSeconds(introTimerLength);
        LevelStarted = true;
    }

    void Awake()
    {
        ServiceLocator.Instance.RegisterLevelManager(this);
    }
    
    void Start()
    {
        LevelTimer = 0.0f;
        StartCoroutine(StartLevel());
    }

    public void FinishLevel()
    {
        LevelStarted = false;
    }
}
