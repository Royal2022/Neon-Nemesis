using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{

    [SerializeField] private Text playerName;

    public Photon.Realtime.Player Player { get; private set; }


    public void SetPlayerInfo(Photon.Realtime.Player player)
    {
        Player = player;
        playerName.text = player.NickName;
    }


}
