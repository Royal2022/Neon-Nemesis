using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListing : NetworkBehaviour
{
    public void DestroyListingPlayer()
    {
        NetworkServer.Destroy(gameObject);
    }
}
