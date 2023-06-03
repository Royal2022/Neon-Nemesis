using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaundInfo : NetworkBehaviour
{
    [SyncVar]
    public int NumberOfRounds = 1;

    public Text RaundNumber;
    public GameObject ButtonAddRaund;
    public GameObject ButtonRemoveRaund;


    private void Start()
    {
        if (!isServer)
        {
            ButtonAddRaund.SetActive(false);
            ButtonRemoveRaund.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        RaundNumber.text = NumberOfRounds.ToString();
        ReSpawnManager.NumberOfRounds = NumberOfRounds;

        if (isServer)
        {
            if (NumberOfRounds == 1)
                ButtonRemoveRaund.SetActive(false);
            else
                ButtonRemoveRaund.SetActive(true);

            if (NumberOfRounds == 8)
                ButtonAddRaund.SetActive(false);
            else
                ButtonAddRaund.SetActive(true);
        }
    }

    public void ClickAddRaund()
    {
        if (NumberOfRounds < 8)
        {
            NumberOfRounds += 1;
        }
    }
    public void ClickRemoveRaund()
    {
        if (NumberOfRounds > 1 )
        {
            NumberOfRounds -= 1;
        }
    }
}
