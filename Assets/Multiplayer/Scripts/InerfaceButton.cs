using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InerfaceButton : NetworkBehaviour
{
    [Command(requiresAuthority = false)]
    public void CmdOnClick_LeaveGame()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        foreach (var player in players)
        {
            Debug.Log(player.gameObject);
            Destroy(player.gameObject);
        }

        Invoke("StopGame", 0.1f);
    }
    private void StopGame()
    {
        NetworkManager.singleton.StopHost();
    }

}
