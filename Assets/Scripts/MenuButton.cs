using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnClick_Play()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClick_MultiplayerPlay() 
    {
        SceneManager.LoadScene(4);
    }
    public void OnClick_BackMenu()
    {
        SceneManager.LoadScene(0);
    }


    /*LocalPlayGames*/
    public void OnClick_Continue()
    {
        SceneManager.LoadScene(3);
    }    
    
    
    public void OnClick_Quit()
    {
        Application.Quit();
    }

}
