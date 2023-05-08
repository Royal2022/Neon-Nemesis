using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMONetworkManager : NetworkManager
{
    public GameObject[] player;


    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateMMOCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CreateMMOCharacterMessage characterMessage = new CreateMMOCharacterMessage
        {
            SkinNumber = M_PlayerInfoSave.SkinNumber,
        };

        NetworkClient.Send(characterMessage);
    }

    void OnCreateCharacter(NetworkConnectionToClient conn, CreateMMOCharacterMessage message)
    {
        GameObject gameobject = Instantiate(player[message.SkinNumber]);

        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
}
