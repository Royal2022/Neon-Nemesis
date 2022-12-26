using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        PhotonNetwork.Instantiate(player.name, new Vector2(Random.Range(-5, 5), 0), Quaternion.identity);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    //public override void OnLeftLobby()
    //{
    //    SceneManager.LoadScene("Lobby"); 
    //}

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }


    //public void LeaveRoom()
    //{
    //    PhotonNetwork.LeaveRoom(true);
    //}
    //public override void OnLeftRoom()
    //{
    //    SceneManager.LoadScene(0);
    //    base.OnLeftRoom();
    //}
}
