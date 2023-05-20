using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_PauseMenuButton : MonoBehaviour
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
        AudioListener.pause = true;
    }
    public void Continue()
    {
        PauseCanvs.SetActive(false);
        AudioListener.pause = false;
    }
}
