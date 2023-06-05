using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public int SceneID;

    public Slider LoadingValue;


    public void ContinueGame()
    {
        StaticGameInfo.ButtonContinueWasPressed = true;
        Loading();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("MainSave");
        gameObject.SetActive(true);
        StartCoroutine(LoadAsync(5));
        StartCoroutine(LoadingSlider());
    }

    public void Loading()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadAsync(SceneID));
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