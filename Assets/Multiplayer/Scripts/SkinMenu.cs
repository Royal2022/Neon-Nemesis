using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinMenu : MonoBehaviour
{
    private int SelectNumber = 0;

    public Sprite[] Skin;
    public string[] SkinName;

    public Image ImageSkinOut;
    public Text NameSkinOut;

    public Button BackButton;
    public Button NextButton;


    private void Start()
    {
        M_PlayerInfoSave.SkinNumber = SelectNumber;
        ImageSkinOut.sprite = Skin[SelectNumber];
        NameSkinOut.text = SkinName[SelectNumber];


    }

    private void FixedUpdate()
    {
        if (SelectNumber != 0)
            BackButton.gameObject.SetActive(true);
        else
            BackButton.gameObject.SetActive(false);

        if (SelectNumber != Skin.Length - 1)
            NextButton.gameObject.SetActive(true);
        else
            NextButton.gameObject.SetActive(false);
    }

    public void OnClickBack()
    {
        SelectNumber--;
        if (SelectNumber < 0)
            SelectNumber = 0;
        ImageSkinOut.sprite = Skin[SelectNumber];
        NameSkinOut.text = SkinName[SelectNumber];
        M_PlayerInfoSave.SkinNumber = SelectNumber;
    }
    public void OnClickNext()
    {
        SelectNumber++;
        if (SelectNumber >= Skin.Length - 1)
            SelectNumber = Skin.Length - 1;
        ImageSkinOut.sprite = Skin[SelectNumber];
        NameSkinOut.text = SkinName[SelectNumber];
        M_PlayerInfoSave.SkinNumber = SelectNumber;
    }
}
