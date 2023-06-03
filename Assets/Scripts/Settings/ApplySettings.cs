using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ApplySettings : MonoBehaviour
{
    public bool ChangWithoutConfirmation;

    public VolumeSettings volumeSettings;
    public GraphicsSettings graphicsSettings;


    public void ClickApply(GameObject button)
    {
        ChangWithoutConfirmation = false;
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }
        QualitySettings.SetQualityLevel(graphicsSettings.TemporarySaveQuality);
        Screen.fullScreen = graphicsSettings.TemporarySaveScreen;
        QualitySettings.vSyncCount = graphicsSettings.TemporarySaveVSync;
        Screen.SetResolution(graphicsSettings.TemporarySaveScreenResolution.width, graphicsSettings.TemporarySaveScreenResolution.height, graphicsSettings.TemporarySaveScreen);
    }

}
