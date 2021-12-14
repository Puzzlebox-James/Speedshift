using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator _instance;
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            GameObject instanceObject = new GameObject("Service Locator");
            DontDestroyOnLoad(instanceObject);
            _instance = instanceObject.AddComponent<ServiceLocator>();
            return _instance;
        }
    }

    private LevelManager _currentLevelManager;

    public LevelManager GetLevelManager()
    {
        return _currentLevelManager;
    }

    public void RegisterLevelManager(LevelManager newLevelManager)
    {
        _currentLevelManager = newLevelManager;
    }
}
