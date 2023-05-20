using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WeaponHands : MonoBehaviour
{
    public void ReloadEndPistol()
    {
        if (transform.GetChild(1).gameObject.gameObject.activeSelf)
        {
            GetComponent<Animator>().SetBool("reloadPistol", false);
            Transform holdPointPistol = transform.GetChild(1).GetChild(0).GetChild(0);
            if (holdPointPistol.childCount > 0)
                holdPointPistol.GetChild(0).GetComponent<M_Pistol>().Reload();
        }
        else if (transform.GetChild(2).gameObject.gameObject.activeSelf)
        {
            GetComponent<Animator>().SetBool("reloadAutoGun", false);
            Transform holdPointAutoGun = transform.GetChild(2).GetChild(0).GetChild(0);
            if (holdPointAutoGun.childCount > 0)
                holdPointAutoGun.GetChild(0).GetComponent<M_AutomaticGun>().Reload();
        }
    }
}
