using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class ReSpawnManager : NetworkBehaviour
{
    public Transform[] spawnPoint;

    public float ReSpawnTime = 5f;
    private List<float> time = new List<float>();
    private List<GameObject> SaveGMObject = new List<GameObject>();

    public static int NumberOfRounds;
    public GameObject RoundSCanvas;

    public GameObject[] AllPalyer;

    public SyncList<int> noRepeatNum = new SyncList<int>();

    public bool tf;

    public GameObject PrefabCanfiti;
    public GameObject LobbyManager;

    public void SaveAllPlayer()
    {
        AllPalyer = GameObject.FindGameObjectsWithTag("Player");
    }
   
    public void ResSpawnInfo(GameObject GMObject)
    {
        //for (int i = 0; i < AllPalyer.Length; i++)
        //{
        //    AllPalyer[i].SetActive(false);
        //    Invoke("EnabOrDisabRoundEndCanvas", 5);
        //}
        GMObject.SetActive(false);

        Invoke("EnabledRoundEndCanvas", 2);


        for (int i = 0; i < AllPalyer.Length; i++)
        {
            if (AllPalyer[i].activeSelf)
            {
                if (isServer)
                    AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins += 1;
            }
        }

      

        tf = false;
        if (isServer)
            noRepeatNum.Clear();

        //time.Add(ReSpawnTime);
        //SaveGMObject.Add(GMObject);
    }

    [ClientRpc]
    public void RpcWin()
    {
        for (int i = 0; i < AllPalyer.Length; i++)
        {
            if (AllPalyer[i].activeSelf)
            {

                //AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins += 1;


                if (AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins >= NumberOfRounds)
                {
                    AllPalyer[i].GetComponent<Rigidbody2D>().simulated = false;
                    Instantiate(PrefabCanfiti, AllPalyer[i].transform.GetChild(1));
                    CancelInvoke("EnabledRoundEndCanvas");
                    Invoke("EnabledRoundEndCanvas", 5);
                }

                break;
            }
        }
    }
    [Command]
    public void CmdWin()
    {
        RpcWin();
    }
    public void Win()
    {
        if (isServer)
            RpcWin();
        else if (isClient)
            CmdWin();
    }


    public void EnabledRoundEndCanvas()
    {
        for (int i = 0; i < AllPalyer.Length; i++)
        {
            RoundSCanvas.GetComponent<RoundsCanvas>().SetPlayerName(AllPalyer[i].GetComponent<M_Player>().TextName.text, i);
            if (AllPalyer[i].activeSelf)
            {
                //AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins += 1;
                RoundSCanvas.GetComponent<RoundsCanvas>().SetStarsWinPLayer(i, AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins);
                AllPalyer[i].SetActive(false);
            }
        }
        RoundSCanvas.SetActive(true);
        RoundSCanvas.GetComponent<RoundsCanvas>().HowManyRounds(NumberOfRounds, true);
    }



    //void Update()
    //{
    //    for (int i = 0; i < SaveGMObject.Count; i++)
    //    {
    //        if (!SaveGMObject[i].activeSelf)
    //        {
    //            if (time[i] > 0)
    //            {
    //                time[i] -= Time.deltaTime;
    //                Debug.Log(time[i]);
    //            }
    //            else
    //            {
    //                ResSpawn(SaveGMObject[i]);
    //                SaveGMObject.RemoveAt(i);
    //                time.RemoveAt(i);
    //            }
    //        }
    //    }
    //}


    // 2-7
    public void ResSpawn(GameObject GMObject)
    {
        GMObject.gameObject.GetComponent<M_Player>().Health = 10;
        GMObject.gameObject.SetActive(true);
        GMObject.GetComponent<M_WeaponSwitch>().SetActive(0);
        GMObject.gameObject.transform.Find("weapon_hands").gameObject.SetActive(true);
        GMObject.gameObject.transform.position = spawnPoint[RandomNumber()].position;
        GMObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //GMObject.gameObject.GetComponent<M_Player>().Health = 10;
        //GMObject.gameObject.SetActive(true);
        //GMObject.GetComponent<M_WeaponSwitch>().SetActive(0);
        //GMObject.gameObject.transform.Find("weapon_hands").gameObject.SetActive(true);
        //GMObject.gameObject.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
        //GMObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    
    public int RandomNumber()
    {
        int rnd = 0;

        if (isServer && !tf)
        {
            tf = true;
            rnd = Random.Range(2, 8);

            noRepeatNum.Add(rnd);
            //Debug.Log(rnd);
            return rnd;
        }
        else
        {
            for (int i = 0; i < 1; i++)
            {
                rnd = Random.Range(2, 8);

                if (noRepeatNum.Contains(rnd))
                {
                    i--;
                }
                else
                {
                    //Debug.Log(rnd);
                    return rnd;
                }
            }
        }
        Debug.Log(rnd);
        return rnd;
    }


}
