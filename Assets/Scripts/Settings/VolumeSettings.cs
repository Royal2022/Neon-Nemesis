using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixerGroup Master;
    public AudioMixerGroup VolumeGame;
    public AudioMixerGroup Music;


    public Slider TotalVolume;
    public Slider GameVolume;
    public Slider MusicVolume;
    void Start()
    {
        float indexMaster, indexGame, indexMusic;
        Master.audioMixer.GetFloat("MasterVolume",out indexMaster);
        TotalVolume.value = indexMaster;
        VolumeGame.audioMixer.GetFloat("GameVolume", out indexGame);
        GameVolume.value = indexGame;
        Music.audioMixer.GetFloat("MusicVolume", out indexMusic);
        MusicVolume.value = indexGame;
    }


    public void ChangeMaster(float index)
    {
        Master.audioMixer.SetFloat("MasterVolume", index);
    }
    public void ChangeVolumeGame(float index)
    {
        VolumeGame.audioMixer.SetFloat("GameVolume", index);
    }
    public void ChangeMusic(float index)
    {
        Music.audioMixer.SetFloat("MusicVolume", index);
    }
}
