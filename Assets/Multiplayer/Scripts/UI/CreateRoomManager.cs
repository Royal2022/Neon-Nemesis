using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class CreateRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text TextName;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text TextLog;
    [SerializeField] private InputField RoomName;
    public static bool connected;
    public static string saveLobbyName;

    private void Start()
    {
        inputField.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName= TextName.text;
        Log("Welcome");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connect");
        connected = true;
    }
    public void CreateRoom()
    {
        if (RoomName.text != "" && RoomName.text.Length < 8 && connected)
        {
            SaveName();
            Log("Create");
            PhotonNetwork.CreateRoom(RoomName.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
            saveLobbyName = RoomName.text;
        }
        else if (RoomName.text == "")
        {
            Log("Введите название комнаты!");
        }
        else if (RoomName.text.Length > 8)
        {
            Log("Название комнаты может содержать не более 8 символов!");
        }
    }
    public void JoinRoom()
    {
        if (RoomName.text != "" && RoomName.text.Length <= 8 && connected)
        {
            SaveName();
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom(RoomName.text);
            saveLobbyName = RoomName.text;
        }
        else if (RoomName.text == "")
        {
            Log("Введите название комнаты!");
        }
        else if (RoomName.text.Length > 8)
        {
            Log("Название комнаты может содержать не более 8 символов!");
        }
    }
    public override void OnJoinedRoom()
    {
        Log("Join");
        //PhotonNetwork.LoadLevel("Online");
        PhotonNetwork.LoadLevel("Lobby");
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        Log("Ошибка! Возможно комната с таким именем уже существует.");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Log("Комната с таким именем не найдена!");
    }


    private void SaveName()
    {
        PlayerPrefs.SetString("name", TextName.text);
        PhotonNetwork.NickName = PlayerPrefs.GetString("name");
    }

    private void Log(string meesange)
    {
        TextLog.text = meesange;
    }
}
