using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningCanvas : MonoBehaviour
{
    public GameObject MenuCanvas;
    public ApplySettings applySettings;

    public void ClickApply()
    {
        gameObject.SetActive(false);
        applySettings.ClickApply(null);
        applySettings.gameObject.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void ClickClosed()
    {
        gameObject.SetActive(false);
    }
}
