using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    public int SceneID;

    public Slider LoadingValue;
    public GameObject WarningCanvas;

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("SaveMyScene") == 3)
        {
            SceneID = 3;
            StaticGameInfo.ButtonContinueWasPressed = true;
            Loading();
        }
        else if (PlayerPrefs.GetInt("SaveMyScene") == 7)
        {
            SceneID = 7;
            StaticGameInfo.ButtonContinueWasPressed = true;
            Loading();
        }

    }
    public void Loading()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadAsync(SceneID));
        StartCoroutine(LoadingSlider());
    }



    public void NewGame()
    {
        if (PlayerPrefs.HasKey("MainSave"))
        {
            WarningCanvas.SetActive(true);
        }
        else
        {
            PlayerPrefs.DeleteKey("MainSave");
            gameObject.SetActive(true);
            StartCoroutine(LoadAsync(6));
            StartCoroutine(LoadingSlider());
        }
    }
    public void Cancel()
    {
        WarningCanvas.SetActive(false);
    }
    public void CreatNewGame()
    {
        gameObject.SetActive(true);
        WarningCanvas.SetActive(false);
        PlayerPrefs.DeleteKey("MainSave");
        StartCoroutine(LoadAsync(6));
        StartCoroutine(LoadingSlider());
    }

    IEnumerator LoadAsync(int Id)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(Id);
        loadAsync.allowSceneActivation = false;


        while (!loadAsync.isDone)
        {
            float progress = Mathf.Clamp01(loadAsync.progress / .9f);
            LoadingValue.value = loadAsync.progress;

            if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(.5f);
                loadAsync.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(2.2f);
        }
    }
    IEnumerator LoadingSlider()
    {
        while (LoadingValue.value <= 0.7)
        {
            yield return new WaitForSeconds(.5f);
            LoadingValue.value += .1f;
        }
    }
}
