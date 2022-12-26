using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoomMenu : MonoBehaviourPunCallbacks
{

    //public void OnClick_LeaveRoom()
    //{
    //    PhotonNetwork.LeaveRoom(true);
    //    SceneManager.LoadScene(0);
    //}



    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
}
