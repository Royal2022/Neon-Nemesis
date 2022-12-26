using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomName : MonoBehaviour
{    
    void Start()
    {
        gameObject.GetComponent<Text>().text = CreateRoomManager.saveLobbyName;
    }
}
