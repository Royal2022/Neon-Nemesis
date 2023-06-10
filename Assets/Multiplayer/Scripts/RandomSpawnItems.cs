using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawnItems : NetworkBehaviour
{
    public Vector3[] AllSpawnPosition;

    public GameObject[] AllItemsPrefabs;
    public GameObject[] AllGuns;


    public int[] rndPosition;

    public List<GameObject> SaveAllSpawnObject = new List<GameObject>();

    private void Start()
    {
        //rndPosition = GenerateUniqueRandomNumbers(0, transform.childCount - 1, 3);

        AllSpawnPosition = new Vector3[transform.childCount];
        for (int i = 0; i < AllSpawnPosition.Length; i++)
        {
            AllSpawnPosition[i] = transform.GetChild(i).transform.position;
        }

        InvokeRepeating("RepeatedMethodSpawn", 0f, 30);
        //StartCoroutine(SpawnItems(2));
        //StartCoroutine(SpawnGuns());
    }


    private void RepeatedMethodSpawn()
    {
        for (int i = 0; i < SaveAllSpawnObject.Count; i++)
        {
            if (SaveAllSpawnObject[i] != null && SaveAllSpawnObject[i].transform.parent == null)
                Destroy(SaveAllSpawnObject[i]);
        }


        SaveAllSpawnObject.Clear();
        rndPosition = GenerateUniqueRandomNumbers(0, transform.childCount - 1, 3);
        StartCoroutine(CoroutineSpawnItems(2));

        if (isServer)
            SpawnGuns();
    }

    IEnumerator CoroutineSpawnItems(int loopCount)
    {
        int currentLoop = 0;
        while (currentLoop < loopCount)
        {
            int SelectItems = Random.Range(0, AllItemsPrefabs.Length);
            if (isServer)
                SpawnItems(SelectItems, currentLoop);
            //GameObject obj = Instantiate(AllItemsPrefabs[SelectItems]);
            //obj.transform.position = AllSpawnPosition[rndPosition[currentLoop]];
            //SaveAllSpawnObject.Add(obj);
            currentLoop++;

            yield return null;
        }
    }

    [Server]
    private void SpawnItems(int SelectItems, int currentLoop)
    {
        GameObject obj = Instantiate(AllItemsPrefabs[SelectItems]);
        obj.transform.position = AllSpawnPosition[rndPosition[currentLoop]];
        SaveAllSpawnObject.Add(obj);
        NetworkServer.Spawn(obj);

    }
    //[Command]
    //private void CmdSpawnItems(int SelectItems, int currentLoop)
    //{
    //    RpcSpawnItems(SelectItems, currentLoop);
    //}
    //private void SpawnItems(int SelectItems, int currentLoop)
    //{
    //    if (isServer)
    //        RpcSpawnItems(SelectItems, currentLoop);
    //    else if (isClient)
    //        CmdSpawnItems(SelectItems, currentLoop);
    //}


    [Server]
    private void SpawnGuns()
    {
        int SelectItems = Random.Range(0, AllGuns.Length);
        GameObject obj = Instantiate(AllGuns[SelectItems]);
        obj.transform.position = AllSpawnPosition[rndPosition[2]];
        SaveAllSpawnObject.Add(obj);
        NetworkServer.Spawn(obj);
    }








    private int[] GenerateUniqueRandomNumbers(int minValue, int maxValue, int count)
    {
        int[] numbers = new int[count];
        int index = 0;

        while (index < count)
        {
            int randomNumber = Random.Range(minValue, maxValue + 1);
            if (!ArrayContains(numbers, randomNumber))
            {
                numbers[index] = randomNumber;
                index++;
            }
        }

        return numbers;
    }

    private bool ArrayContains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }












}
