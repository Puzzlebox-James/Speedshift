using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    public void OnNewGameButton()
    {
        SceneManager.LoadScene(firstSceneName);
    }
    
    public void OnLevelSelectButton()
    {
        
    }
    
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
