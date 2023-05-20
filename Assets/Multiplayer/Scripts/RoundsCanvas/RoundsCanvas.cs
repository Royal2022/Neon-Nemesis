using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoundsCanvas : NetworkBehaviour
{
    public Text[] PlayerNameOut;

    public GameObject[] StarsTable;

    public ReSpawnManager reSpawnManager;

    public GameObject LobbyCanvas;

    public void HowManyRounds(int value, bool tf)
    {
        if (tf)
        {
            for (int i = 0; i < StarsTable.Length; i++)
            {
                for (int j = 0; j < value; j++)
                {
                    StarsTable[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < StarsTable.Length; i++)
            {
                for (int j = 0; j < value; j++)
                {
                    StarsTable[i].transform.GetChild(j).GetChild(0).gameObject.SetActive(false);
                    StarsTable[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetPlayerName(string playerName, int index)
    {
        PlayerNameOut[index].text = playerName;
    }

    public void SetStarsWinPLayer(int index, int SumWin)
    {    
        for (int i = 0; i < StarsTable.Length; i++)
        {
            if (index == i)
            {
                for (int j = 0; j < StarsTable[i].transform.childCount; j++)
                {
                    if (!StarsTable[i].transform.GetChild(j).gameObject.transform.GetChild(0).gameObject.activeSelf)
                    {
                        StarsTable[i].transform.GetChild(j).gameObject.GetComponent<Animator>().enabled = true;
                        StarsTable[i].transform.GetChild(j).gameObject.GetComponent<StarPoint>().Animator = StarsTable[i].transform.GetChild(j).gameObject.GetComponent<Animator>();

                        if (SumWin != ReSpawnManager.NumberOfRounds)
                        {                       
                            Invoke("ClosedCanvas", 5);
                            break;
                        }
                        else
                        {
                            LobbyCanvas.GetComponent<LobbyManager>().Invoke("StopClientOrHost", 5);
                            //Invoke("CmdGameEnd", 5);
                            break;
                        }
                    }
                }
            }
        }
    }
    
    public void ClosedCanvas()
    {
        for (int i = 0; i < reSpawnManager.AllPalyer.Length; i++)
        {
            reSpawnManager.ResSpawn(reSpawnManager.AllPalyer[i]);
        }
        this.gameObject.SetActive(false);
    }

    //public GameObject[] AllPalyer;
    //[Command(requiresAuthority = false)]
    //public void CmdGameEnd()
    //{
    //    AllPalyer = reSpawnManager.AllPalyer;
    //    for (int i = 0; i < AllPalyer.Length; i++)
    //    {
    //        if (!AllPalyer[i].activeSelf)
    //        {
    //            AllPalyer[i].gameObject.SetActive(true);
    //            HowManyRounds(ReSpawnManager.NumberOfRounds, false);
    //            AllPalyer[i].GetComponent<Rigidbody2D>().simulated = true;
    //            AllPalyer[i].GetComponent<M_Player>().NumberOfRoundsWins = 0;
    //            AllPalyer[i].GetComponent<M_Player>().DisabledAllChildrenAndParent();
    //        }
    //    }
    //    LobbyCanvas.gameObject.SetActive(true);
    //    this.gameObject.SetActive(false);
    //}

}
