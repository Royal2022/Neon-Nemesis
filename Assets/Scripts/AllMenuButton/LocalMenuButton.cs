using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMenuButton : MonoBehaviour
{
    public Button continueGame;

    private void Update()
    {
        if (PlayerPrefs.HasKey("MainSave"))
            continueGame.interactable = true;
        else
            continueGame.interactable = false;
    }
}
