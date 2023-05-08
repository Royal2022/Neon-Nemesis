using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CreatRoomManager : MonoBehaviour
{
    public InputField NickNameInput;
    public InputField RoomCodeInput;

    public Text Warning;

    public Slider SkinNumber;
    public Text SkinNumberOut;

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
            M_PlayerInfoSave.HostRoomCode = CreateRoomCode(GetIP4Address());
        }
        else
            WarningLog("����� �������� ������ ���� �� ����� 4 � �� ����� 12 ��������!");

    }

    public void ButtonClient()
    {
        if (RoomCodeInput.text.Length == 0)
            WarningLog("������� ��� �������, ����� ������������!");


        if (NickNameInput.text.Length >= 4 && NickNameInput.text.Length <= 12)
        {
            M_PlayerInfoSave.HostRoomCode = RoomCodeInput.text;
            NetworkManager.singleton.networkAddress = "192.168." + RoomCodeInput.text;
            NetworkManager.singleton.StartClient();

            if (!NetworkClient.isConnected)
                WarningLog("�� ������� ������������! ��������� ��������� �� ������ ��� �������.");
                //������� ������: UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
        }
        else
            WarningLog("����� �������� ������ ���� �� ����� 4 � �� ����� 12 ��������!");
    }


    //public string GetLocalIPv4Address()
    //{
    //    string strIP = "";
    //    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
    //    foreach (IPAddress ip in host.AddressList)
    //    {
    //        if (ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip))
    //        {
    //            strIP = ip.ToString();
    //            break;
    //        }
    //    }
    //    return strIP;
    //}
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
}
