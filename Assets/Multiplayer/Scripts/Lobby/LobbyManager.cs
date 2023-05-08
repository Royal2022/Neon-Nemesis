using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Threading.Tasks;
using Telepathy;
using System;


public class LobbyManager : NetworkBehaviour
{
    public string TextName;


    private GameObject player;
    public Button Play;

    public GameObject PlayerListingPrefab;
    public GameObject PlayerListingMenuContent;
    public Text LobbyCode;
    private NetworkManager networkManager;

    public Transform[] PositionStart;

    public ReSpawnManager ReSpawnManager;

    public RoundsCanvas RoundsCanvas;


    private void Start()
    {
        ReSpawnManager.RoundSCanvas.SetActive(false);
        player = FindObjectOfType<M_Player>().gameObject;
        //player.transform.GetChild(3).gameObject.SetActive(false);
        //player.GetComponent<Rigidbody2D>().simulated = false;
        networkManager = FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();
        LobbyCode.text = M_PlayerInfoSave.HostRoomCode;
    }

    private void Update()
    {
        if (isServer && NetworkServer.connections.Count >= 1)
            Play.interactable = true;
        else
            Play.interactable = false;  

    }

    public GameObject[] AllPlayer;

    [ClientRpc]
    public void RpcOnClick()
    {
        ReSpawnManager.SaveAllPlayer();

        //player.GetComponent<M_Player>().OFF(true);
        this.gameObject.SetActive(false);
        //player.transform.GetChild(3).gameObject.SetActive(true);
        //player.GetComponent<Rigidbody2D>().simulated = true;

        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (i == 0)
            {
                player.transform.position = PositionStart[0].position;
            }
            else
            {
                player.transform.position = PositionStart[1].position;
            }
        }


        AllPlayer = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < AllPlayer.Length; i++)
        {
            //AllPlayer[i].GetComponent<M_Player>().enabled = true;
            AllPlayer[i].GetComponent<M_WeaponSwitch>().enabled = true;
            AllPlayer[i].GetComponent<Rigidbody2D>().simulated = true;

            for (int j = 0; j < AllPlayer[i].transform.childCount; j++)
            {
                AllPlayer[i].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }
    [Command]
    public void CmdOnClick()
    {
        RpcOnClick();
    }
    public void OnClickPlay()
    {
        if (isServer)
            RpcOnClick();
        else
            CmdOnClick();
    }


    public SyncList<string> AllPlayerName = new SyncList<string>();

    [Command(requiresAuthority = false)]
    public void CmdSavePlayerName(string name)
    {
        AllPlayerName.Add(name);
        //CreatePlayerListing(name);
    }
    [Command(requiresAuthority = false)]
    public void CmdRemoveAllPlayerName(int index)
    {
        if (isServer)
            AllPlayerName.RemoveAt(index);
        //CreatePlayerListing(name);
    }

    private int a = 0;
    private void FixedUpdate()
    {
        if (PlayerListingMenuContent.transform.childCount < AllPlayerName.Count)
            for (int i = 0; i < AllPlayerName.Count; i++)
            {
                if (i == a)
                {
                    CreatPlayerListing(i);
                    a += 1;
                }
            }
        else if (PlayerListingMenuContent.transform.childCount > AllPlayerName.Count)
            for (int i = 0; i < PlayerListingMenuContent.transform.childCount; i++)
            {
                for (int j = 0; j < AllPlayerName.Count; j++)
                {
                    if (PlayerListingMenuContent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text != AllPlayerName[j])
                    {
                        Destroy(PlayerListingMenuContent.transform.GetChild(i).gameObject);
                        a -= 1;
                    }
                }

            }
    }

    public void CreatPlayerListing(int index)
    {
        GameObject gm = Instantiate(PlayerListingPrefab, transform);
        gm.transform.GetChild(0).GetComponent<Text>().text = AllPlayerName[index];
        gm.transform.SetParent(PlayerListingMenuContent.transform, false);
    }



    //[ClientRpc]
    //public void RpcDestroyPlayerListing(string name)
    //{
    //    for (int i = 0; i < AllPlayerName.Count; i++)
    //    {
    //        if (AllPlayerName[i] == name)
    //        {
    //            CmdRemoveAllPlayerName(i);
    //            a -= 1;
    //            break;
    //        }
    //    }
    //}
    //[Command(requiresAuthority = false)]
    //public void CmdDestroyPlayerListing(string name)
    //{
    //    RpcDestroyPlayerListing(name);
    //}
    public void DestroyPlayerListing(string name)
    {
        //if (isServer)
        //    RpcDestroyPlayerListing(name);
        //else if (isClient)
        //    CmdDestroyPlayerListing(name);
        for (int i = 0; i < AllPlayerName.Count; i++)
        {
            if (AllPlayerName[i] == name)
            {
                CmdRemoveAllPlayerName(i);
                a -= 1;
                break;
            }
        }
    }


    public void ButtonLeave()
    {
        DestroyPlayerListing(M_PlayerInfoSave.PlayerNickName);
        Invoke("StopClientOrHost", 0.1f);
    }

    public void StopClientOrHost()
    {
        if (networkManager.mode == NetworkManagerMode.ClientOnly)
            NetworkManager.singleton.StopClient();
        if (isServer)
            NetworkManager.singleton.StopHost();
    }


}
