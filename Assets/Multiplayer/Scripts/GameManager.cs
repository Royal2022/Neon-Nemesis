using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;

    public Transform[] spawnPoint;
    private bool[] spawnPointBusy;


    private int quantityOfPlayerConn = 0;


    private void Start()
    {
        spawnPointBusy = new bool[spawnPoint.Length];

        //int a = Random.Range(0, spawnPoint.Length);
        //for (int i = 0; i < spawnPointBusy.Length; i++)
        //{
        //    if (!spawnPointBusy[i])
        //    {
        //        spawnPointBusy[a] = true;
        //        PhotonNetwork.Instantiate(player.name, spawnPoint[a].position, Quaternion.identity);
        //        break;
        //    }
        //}


        if (photonView.IsMine)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint[0].position, Quaternion.identity);
        }
        else if (!photonView.IsMine) {
            PhotonNetwork.Instantiate(player.name, spawnPoint[1].position, Quaternion.identity);
        }



        //PhotonNetwork.Instantiate(player.name, new Vector2(Random.Range(-5, 5), 0), Quaternion.identity);
    }

    [PunRPC]
    public void Spwan()
    {
        //int a = Random.Range(0, spawnPoint.Length);
        spawnPointBusy[quantityOfPlayerConn] = true;
        quantityOfPlayerConn++;
    }


    //private void Update()
    //{
    //    for(int i = 0; i < spawnPoint.Length; i++)
    //        Debug.Log(i + ":" + spawnPointBusy[i]);
    //}



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
