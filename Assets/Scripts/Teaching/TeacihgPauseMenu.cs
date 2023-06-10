using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TeacihgPauseMenu : MonoBehaviour
{
    public GameObject PauseCanvs;

    private void Start()
    {
        Player.NumberOfGrenades = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseCanvs.activeSelf)
                Pause();
            else Continue(null);
        }
    }

    public void Pause()
    {
        PauseCanvs.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void Continue(GameObject button)
    {
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }
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
