using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InerfaceButton : NetworkBehaviour
{
    [Command(requiresAuthority = false)]
    public void CmdOnClick_LeaveGame()
    {
        //SceneManager.LoadScene(4);       
        NetworkManager.singleton.StopHost();
    }
}
