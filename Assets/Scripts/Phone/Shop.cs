using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DataProduct
{
    public GameObject ProductSelection;
    public GameObject ProductPrefab;
}


public class Shop : MonoBehaviour
{
    public DataProduct[] AllProducts;

    public Transform SpawnPointDrones;

    public GameObject DronesSpawn;

    private GameObject AlreadyExistDrones;

    private void Update()
    {
        for (int i = 0; i < AllProducts.Length; i++)
        {
            DataProduct data = AllProducts[i];
            int price = int.Parse(data.ProductSelection.transform.Find("Textprice").gameObject.GetComponent<TextMeshProUGUI>().text);

            if (!AlreadyExistDrones)
            {
                if (Player.money < price)
                    data.ProductSelection.GetComponent<Button>().interactable = false;
                else if (Player.money >= price)
                    data.ProductSelection.GetComponent<Button>().interactable = true;
            }
            else
                data.ProductSelection.GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick_Buy(int id)
    {
        DataProduct data = AllProducts[id];

        int price = int.Parse(data.ProductSelection.transform.Find("Textprice").gameObject.GetComponent<TextMeshProUGUI>().text);
        if (Player.money >= price)
        {
            GameObject DronesObj = Instantiate(DronesSpawn, new Vector3(SpawnPointDrones.position.x, SpawnPointDrones.position.y, SpawnPointDrones.position.z), Quaternion.identity);
            DronesObj.GetComponent<Drones>().Product(data.ProductPrefab);
            AlreadyExistDrones = DronesObj;
            Player.money -= price;
            data.ProductSelection.GetComponent<Button>().interactable = false;
        }
    }
}
