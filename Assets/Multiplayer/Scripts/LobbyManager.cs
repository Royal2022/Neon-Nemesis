using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text TextName;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text TextLog;

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
    }
    public void CreateRoom()
    {
        SaveName();
        Log("Create");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2});
    }
    public void JoinRoom()
    {
        SaveName();
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Log("Join");
        PhotonNetwork.LoadLevel("Online");
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
