using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] AllMusic;

    public Slider AudioLength;

    public Image ButtonPlayOrStopImg;
    public Sprite PlayButtonImg;
    public Sprite StopButtonImg;

    public Text OutTextNumberAudio;
    public int nowMusic = 0;

    private void Start()
    {
        OutTextNumberAudio.text = "№ " + (nowMusic + 1);
        PlayListLoading();
    }

    /* Функцию запуска плейлиста, которая загружает аудиофайлы заранее, чтобы уменьшить задержки при переключении музыки. */
    private void PlayListLoading()
    {
        for (int i = AllMusic.Length - 1; i >= 0; i--)
        {
            audioSource.clip = AllMusic[i];
            audioSource.Play();
        }
    }
    /*=====================================================================================================================*/

    public bool playOrStop = true;
    private float saveAudiotime;
    void Update()
    {
        AudioLength.maxValue = audioSource.clip.length;

        if (playOrStop)
            AudioLength.value = audioSource.time;

        if (audioSource.time == audioSource.clip.length)
            ClickNextAudio();
    }


    public void ButtonPlayOrStop()
    {
        if (!playOrStop)
        {
            audioSource.Play();
            audioSource.time = saveAudiotime;
            ButtonPlayOrStopImg.sprite = StopButtonImg;
            playOrStop = true;
        }
        else if (playOrStop)
        {
            saveAudiotime = audioSource.time;
            audioSource.Stop();
            ButtonPlayOrStopImg.sprite = PlayButtonImg;
            playOrStop = false;
        }
        AudioLength.value = saveAudiotime;
    }


    public void ClickNextAudio()
    {
        playOrStop = false;
        saveAudiotime = 0;
        ButtonPlayOrStop();
        nowMusic++;
        if (nowMusic < AllMusic.Length)
        {
            audioSource.clip = AllMusic[nowMusic];
            audioSource.Play();
        }
        else
        {
            nowMusic = 0;
            audioSource.clip = AllMusic[nowMusic];
            audioSource.Play();
        }
        OutTextNumberAudio.text = "№ " + (nowMusic + 1);
    }
    public void ClickBackAudio()
    {
        playOrStop = false;
        saveAudiotime = 0;
        ButtonPlayOrStop();
        nowMusic--;
        if (nowMusic >= 0)
        {
            audioSource.clip = AllMusic[nowMusic];
            audioSource.Play();
        }
        else
        {
            nowMusic = AllMusic.Length - 1;
            audioSource.clip = AllMusic[nowMusic];
            audioSource.Play();
        }
        OutTextNumberAudio.text = "№ " + (nowMusic + 1);
    }




}
