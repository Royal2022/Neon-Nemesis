using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseCanvs.activeSelf)
                Pause();
            else Continue();
        }
    }

    public void Pause()
    {
        PauseCanvs.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void Continue()
    {
        PauseCanvs.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void OnClick_BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }
}
