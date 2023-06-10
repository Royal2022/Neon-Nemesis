using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using System;
using Unity.VisualScripting;

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
        UniversalRenderPipelineAsset URP = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
        URP.renderScale = graphicsSettings.renderScale;
        URP.supportsHDR = graphicsSettings.HDR;

    }

    public void ClickRest(GameObject button)
    {
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }
        QualitySettings.SetQualityLevel(6);
        graphicsSettings.DropdownQuality.value = 6;
        Screen.fullScreen = true;
        graphicsSettings.ToggleFullScreenMode.isOn = true;
        QualitySettings.vSyncCount = 1;
        graphicsSettings.ToggleVSync.isOn = true;
        for (int i = 0; i < graphicsSettings.DropdownScreenResolution.options.Count; i++)
        {
            if (graphicsSettings.DropdownScreenResolution.options[i].text.Contains("1920"))
            {
                graphicsSettings.DropdownScreenResolution.value = i;
                Screen.SetResolution(1920, 1080, true);
            }
        }
        UniversalRenderPipelineAsset URP = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
        URP.renderScale = 2;
        URP.supportsHDR = true;
        ChangWithoutConfirmation = false;
    }



}
