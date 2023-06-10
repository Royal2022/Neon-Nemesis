using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SoundPlayer : NetworkBehaviour
{
    public AudioSource RunSound;

    private M_Player player;
    private bool runSound;

    void Start()
    {
        player = GetComponent<M_Player>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (player.anim.GetBool("player_run") && player.isGround && !runSound)
            VolumeControl(true);
        else if (!player.anim.GetBool("player_run") && runSound || !player.isGround && runSound)
            VolumeControl(false);
    }

    /*============================== –егул€ци€ громкости ======================================*/
    [ClientRpc]
    void RpcVolumeContro(bool value)
    {
        if (value)
        {
            RunSound.Play();
            runSound = value;
        }
        else
        {
            RunSound.Stop();
            runSound = value;
        }

    }
    [Command]
    void CmdVolumeControl(bool value)
    {
        RpcVolumeContro(value);
    }
    private void VolumeControl(bool value)
    {
        if (isServer)
            RpcVolumeContro(value);
        else if (isClient)
            CmdVolumeControl(value);
    }



    public AudioSource HandsAttack;

    [ClientRpc]
    void RpcHandsAttackSound()
    {
        HandsAttack.Play();
    }
    [Command(requiresAuthority = false)]
    void CmdHandsAttackSound()
    {
        RpcHandsAttackSound();
    }
    public void HandsAttackSound()
    {
        if (isServer)
            RpcHandsAttackSound();
        else if (isClient)
            CmdHandsAttackSound();
    }


    //public AudioSource JumpSound;
    //public AudioSource AmmoSound;
    //public AudioSource ArmorSound;
    //public AudioSource FirstAidSound;
    //public AudioSource FractureSound;

    public AudioSource[] AllSound;

    [ClientRpc]
    private void RpcJointSoundPlay(int index)
    {
        AllSound[index].Play();
    }
    [Command(requiresAuthority = false)]
    void CmdJointSoundPlay(int index)
    {
        RpcJointSoundPlay(index);
    }
    public void JointSoundPlay(int index)
    {
        if (isServer)
            RpcJointSoundPlay(index);
        else if (isClient)
            CmdJointSoundPlay(index);
    }

    /*=========================================================================================*/
}
