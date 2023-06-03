using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsUIButton : MonoBehaviour
{
    public ApplySettings applySettings;
    public GameObject WarningCanvas;

    public GameObject MenuCanvas;
    public GameObject SettingsCanvs;

    public void OpenSettings(GameObject button)
    {
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }
        MenuCanvas.SetActive(false);
        SettingsCanvs.SetActive(true);
    }
    public void ClosedSettings(GameObject button)
    {
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }

        if (!applySettings.ChangWithoutConfirmation)
        {
            SettingsCanvs.SetActive(false);
            MenuCanvas.SetActive(true);
        }
        else
            WarningCanvas.SetActive(true);
    }
}
