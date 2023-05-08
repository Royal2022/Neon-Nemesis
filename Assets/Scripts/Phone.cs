using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Phone : MonoBehaviour
{
    public Animator anim;

    public Transform SpawnPointDrones;

    public GameObject DronesSpawn;

    public GameObject[] ProductSelection;

    public GameObject[] allProducts;

    public GameObject AlreadyExistDrones;

    private void Update()
    {
        AlreadyExistDrones = GameObject.FindGameObjectWithTag("Drones");

        if (Input.GetKeyDown(KeyCode.Tab) && !anim.GetBool("up_or_down"))
            anim.SetBool("up_or_down", true);
        else if (Input.GetKeyDown(KeyCode.Tab) && anim.GetBool("up_or_down"))
            anim.SetBool("up_or_down", false);

        for(int i = 0; i < ProductSelection.Length; i++)
        {
            int price = int.Parse(ProductSelection[i].transform.Find("Textprice").gameObject.GetComponent<TextMeshProUGUI>().text);

            if (!AlreadyExistDrones)
            {
                if (Player.money < price)
                    ProductSelection[i].GetComponent<Button>().interactable = false;
                else if (Player.money >= price)
                    ProductSelection[i].GetComponent<Button>().interactable = true;
            }
            else
                ProductSelection[i].GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick_Buy(int id)
    {
        Debug.Log(id);
        int price = int.Parse(ProductSelection[id].transform.Find("Textprice").gameObject.GetComponent<TextMeshProUGUI>().text);
        if (Player.money >= price)
        {
            Instantiate(DronesSpawn, new Vector3(SpawnPointDrones.position.x, SpawnPointDrones.position.y, SpawnPointDrones.position.z),
                Quaternion.identity).GetComponent<Drones>().Product(allProducts[id]);
            Player.money -= price;
            ProductSelection[id].GetComponent<Button>().interactable = false;
        }
    }
}
