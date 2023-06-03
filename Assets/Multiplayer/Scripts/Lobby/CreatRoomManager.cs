using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatRoomManager : MonoBehaviour
{
    public InputField NickNameInput;
    public InputField RoomCodeInput;

    public Text Warning;

    public Slider SkinNumber;
    public Text SkinNumberOut;



    public Slider LoadingValue;
    public GameObject LoadingScreenCanvas;
    public GameObject Networkmanager;


    private void Start()
    {
        NickNameInput.text = PlayerPrefs.GetString("Name");
    }

    void Update()
    {        
        PlayerPrefs.SetString("Name", NickNameInput.text);
        M_PlayerInfoSave.PlayerNickName = PlayerPrefs.GetString("Name");
        //M_PlayerInfoSave.SkinNumber = (int)SkinNumber.value;
        //SkinNumberOut.text = SkinNumber.value.ToString();
    }


    public void OnStartServer()
    {
        if (NickNameInput.text.Length >= 4 && NickNameInput.text.Length <= 12)
        {
            NetworkManager.singleton.StartHost();
            StartCoroutine(LoadAsync());
            StartCoroutine(LoadingSlider());
            M_PlayerInfoSave.HostRoomCode = CreateRoomCode(GetIP4Address());
        }
        else
            WarningLog("Длина никнейма должно быть не менее 4 и не более 12 символов!");

    }

    public void ButtonClient()
    {
        if (RoomCodeInput.text.Length == 0)
            WarningLog("Введите код комнаты, чтобы подключиться!");


        if (NickNameInput.text.Length >= 4 && NickNameInput.text.Length <= 12)
        {
            M_PlayerInfoSave.HostRoomCode = RoomCodeInput.text;
            NetworkManager.singleton.networkAddress = "192.168." + RoomCodeInput.text;
            NetworkManager.singleton.StartClient();
            Invoke("ConnectedOrNot", 1);
        }
        else
            WarningLog("Длина никнейма должно быть не менее 4 и не более 12 символов!");
    }
    private void ConnectedOrNot()
    {
        if (NetworkClient.isConnected)
        {
            StartCoroutine(LocalLoadAsync());
            StartCoroutine(LoadingSlider());
        }
        else
        {
            WarningLog("Не удалось подключиться! Проверьте корректно ли введен код комнаты.");
        }
    } 

    public static string GetIP4Address()
    {
        string IP4Address = String.Empty;

        foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (IPA.AddressFamily == AddressFamily.InterNetwork)
            {
                IP4Address = IPA.ToString();
                break;
            }
        }

        return IP4Address;
    }

    public string CreateRoomCode(string ipv4)
    {
        string result = ipv4.Replace("192.168.", "");
        return result;
    }


    public void WarningLog(string text)
    {
        Warning.text = text;
    }






    IEnumerator LoadAsync()
    {
        LoadingScreenCanvas.SetActive(true);
        AsyncOperation loadAsync = NetworkManager.loadingSceneAsync;
        loadAsync.allowSceneActivation = false;


        while (!loadAsync.isDone)
        {
            LoadingValue.value = loadAsync.progress;

            if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(1f);
                loadAsync.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(2.2f);
        }
    }


    IEnumerator LocalLoadAsync()
    {
        LoadingScreenCanvas.SetActive(true);
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        loadAsync.allowSceneActivation = false;


        while (!loadAsync.isDone)
        {
            LoadingValue.value = loadAsync.progress;

            if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(1f);
                Destroy(Networkmanager.GetComponent<SceneInterestManagement>());
                loadAsync.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(2.2f);
        }
    }

    IEnumerator LoadingSlider()
    {
        while (LoadingValue.value <= 0.7)
        {
            yield return new WaitForSeconds(.5f);
            LoadingValue.value += .1f;
        }
    }





}
