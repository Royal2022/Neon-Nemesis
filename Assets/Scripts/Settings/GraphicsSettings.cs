using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public ApplySettings applySettings;

    public Dropdown DropdownQuality;
    public Toggle ToggleFullScreenMode;
    public Toggle ToggleVSync;
    public Dropdown DropdownScreenResolution;

    public int TemporarySaveQuality;
    public bool TemporarySaveScreen;
    public int TemporarySaveVSync;
    public Resolution TemporarySaveScreenResolution;


    public Resolution[] resolutions;
    private int save;
    void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        //foreach (Resolution item in resolutions)
        //{
        //    options.Add(item.ToString());
        //}
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].ToString());
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                save = i;
            }
        }
        DropdownScreenResolution.AddOptions(options);
        DropdownScreenResolution.value = save;
        DropdownQuality.value = QualitySettings.GetQualityLevel();


        ToggleFullScreenMode.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
            ToggleVSync.isOn = false;
        else if (QualitySettings.vSyncCount == 1)
            ToggleVSync.isOn = true;

        TemporarySaveScreen = Screen.fullScreen;
        TemporarySaveVSync = QualitySettings.vSyncCount;

        applySettings.ChangWithoutConfirmation = false;
    }

    public void SetQuality(int qualityindex)
    {
        applySettings.ChangWithoutConfirmation = true;
        //QualitySettings.SetQualityLevel(qualityindex);
        TemporarySaveQuality = qualityindex;
    }
    public void SetFullScreenMode(bool index)
    {
        applySettings.ChangWithoutConfirmation = true;
        TemporarySaveScreen = index;
    }
    public void SetVSync(bool index)
    {
        applySettings.ChangWithoutConfirmation = true;

        if (index)
            TemporarySaveVSync = 1;
        else
            TemporarySaveVSync = 0;
    }
    public void SetScreenResolution(int index)
    {
        applySettings.ChangWithoutConfirmation = true;
        TemporarySaveScreenResolution = Screen.resolutions[index];
    }

}
