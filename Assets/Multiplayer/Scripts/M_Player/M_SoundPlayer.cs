using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SoundPlayer : NetworkBehaviour
{
    public AudioSource RunSound;

    private M_Player player;

    void Start()
    {
        player = GetComponent<M_Player>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (player.anim.GetBool("player_run") && player.isGround)
            VolumeControl(1);
        else
            VolumeControl(0);
    }

    /*============================== –егул€ци€ громкости ======================================*/
    [ClientRpc]
    void RpcVolumeContro(int value)
    {
        RunSound.volume = value;
    }
    [Command]
    void CmdVolumeControl(int value)
    {
        RunSound.volume = value;
        RpcVolumeContro(value);
    }
    private void VolumeControl(int value)
    {
        if (isServer)
            RpcVolumeContro(value);
        else if (isClient)
            CmdVolumeControl(value);
    }
    /*=========================================================================================*/
}
