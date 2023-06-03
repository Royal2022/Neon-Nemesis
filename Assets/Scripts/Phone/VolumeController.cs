using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeController : MonoBehaviour
{
    private int volume = 0;

    public Sprite[] AllImages;
    public Image SliderImage;

    public void AddVolume()
    {
        if (volume < AllImages.Length - 1)
        {
            volume += 1;
            VolumeSlider(volume);
        }

    }
    public void RemoveVolume()
    {
        if (volume > 0)
        {
            volume -= 1;
            VolumeSlider(volume);
        }
    }

    private void Update()
    {
        Debug.Log(volume);
    }
    private void VolumeSlider(int index)
    {
        SliderImage.sprite = AllImages[index];
    }
}
