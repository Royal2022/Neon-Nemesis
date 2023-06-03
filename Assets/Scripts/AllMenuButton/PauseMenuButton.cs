using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvs;
    public string SaveKey = "MainSave";

    private void Start()
    {
        if (StaticGameInfo.ButtonContinueWasPressed)
        {
            ClickLoad();
            StaticGameInfo.ButtonContinueWasPressed = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseCanvs.activeSelf)
                Pause();
            else Continue(null);
        }
    }

    public void Pause()
    {
        PauseCanvs.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void Continue(GameObject button)
    {
        if (button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            button.GetComponent<Button>().OnDeselect(null);
        }
        PauseCanvs.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void OnClick_BackMenu()
    {
        ClickSave(null);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }











    public void ClickLoad()
    {
        var data = SaveManager.Load<SaveData.Save>(SaveKey);
        Player player = FindObjectOfType<Player>();

        player.health = data.Healt;
        player.armor = data.Armor;
        Player.money = data.Money;
        Player.pistol_ammo = data.PistolAmmo;
        Player.automaticGun_ammo = data.AutoGunAmmo;
        player.transform.position = data.PlayerPosition;
        EnemyLiveLoad(data.EnemyAlive);
        ItemsExistLoad(data.ItemsExist);
        StreetLampExistLoad(data.StreetLampExist);
        AllChastLoad(data.ChestOpen);
    }

    private void EnemyLiveLoad(bool[] EnemyAlive)
    {
        GameObject AllZonaPatrols = GameObject.Find("AllZonaPatrols");

        for (int i = 0; i < AllZonaPatrols.transform.childCount; i++)
        {
            if (!EnemyAlive[i])
                Destroy(AllZonaPatrols.transform.GetChild(i).GetChild(0).gameObject);
        }
    }
    public GameObject[] ItemsPrefab;
    private void ItemsExistLoad(SaveData.DataItems[] Items)
    {
        GameObject AllItems = GameObject.Find("All Items");
        for (int i = 0; i < AllItems.transform.childCount; i++)
        {
            Destroy(AllItems.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Items.Length; i++)
        {
            for (int k = 0; k < ItemsPrefab.Length; k++)
            {
                if (Items[i].ItemTag == ItemsPrefab[k].tag)
                {
                    GameObject obj = Instantiate(ItemsPrefab[k], AllItems.transform);
                    obj.transform.position = Items[i].ItemPosition;
                }
            }
        }
    }
    private void StreetLampExistLoad(bool[] StreetLampExist)
    {
        GameObject AllStreetLamp = GameObject.Find("Street Lamp");

        for (int i = 0; i < AllStreetLamp.transform.childCount; i++)
        {
            if (!StreetLampExist[i])
            {
                AllStreetLamp.transform.GetChild(i).GetComponent<Light2D>().enabled = false;
                Destroy(AllStreetLamp.transform.GetChild(i).GetChild(0).gameObject);
            }
        }
    }
    public void AllChastLoad(bool[] ChestOpen)
    {
        GameObject AllChest = GameObject.Find("AllChest");
        for (int i = 0; i <= AllChest.transform.childCount - 1; i++)
        {
            if (ChestOpen[i])
            {
                AllChest.transform.GetChild(i).GetComponent<Chest>().OpenOrClosed = true;
                AllChest.transform.GetChild(i).GetComponent<Chest>().anim.Play("open", 0, 100);
            }
        }
    }


    private Button buttonSave;
    public void ClickSave(Button button)
    {
        SaveManager.Save(SaveKey, GetSaveSnapshot());
        if (button != null)
        {
            button.interactable = false;
            buttonSave = button;
            Invoke("CanClcick", 1);
        }
    }
    private void CanClcick()
    {
        buttonSave.interactable = true;
    }

    private SaveData.Save GetSaveSnapshot()
    {
        Player player = FindObjectOfType<Player>();

        var data = new SaveData.Save()
        {
            Healt = player.health,
            Armor = player.armor,
            Money = Player.money,
            PistolAmmo = Player.pistol_ammo,
            AutoGunAmmo = Player.automaticGun_ammo,
            PlayerPosition = player.transform.position,
            EnemyAlive = EnemyLiveSave(),
            ItemsExist = ItemsExistSave(),
            StreetLampExist = StreetLampExistSave(),
            ChestOpen = AllChastSave(),
        };
        return data;
    }

    private bool[] EnemyLiveSave()
    {
        GameObject AllZonaPatrols = GameObject.Find("AllZonaPatrols");
        bool[] EnemyAlive = new bool[AllZonaPatrols.transform.childCount];
        for (int i = 0; i < EnemyAlive.Length; i++)
        {
            if (AllZonaPatrols.transform.GetChild(i).childCount == 1)
                EnemyAlive[i] = true;
            else
                EnemyAlive[i] = false;
        }
        return EnemyAlive;
    }
    public SaveData.DataItems[] a;
    private SaveData.DataItems[] ItemsExistSave()
    {
        GameObject AllItems = GameObject.Find("All Items");
        SaveData.DataItems[] Items = new SaveData.DataItems[AllItems.transform.childCount];
        for (int i = 0; i < AllItems.transform.childCount; i++)
        {
            SaveData.DataItems item = new SaveData.DataItems();
            item.ItemTag = AllItems.transform.GetChild(i).gameObject.tag;
            item.ItemPosition = AllItems.transform.GetChild(i).position;
            Items[i] = item;
        }
        a = Items;
        return Items;
    }
    private bool[] StreetLampExistSave()
    {
        GameObject AllStreetLamp = GameObject.Find("Street Lamp");
        bool[] StreetLampExist = new bool[AllStreetLamp.transform.childCount];
        for (int i = 0; i < StreetLampExist.Length; i++)
        {
            if (AllStreetLamp.transform.GetChild(i).childCount == 1)
                StreetLampExist[i] = true;
            else
                StreetLampExist[i] = false;
        }
        return StreetLampExist;
    }
    public bool[] AllChastSave()
    {
        GameObject AllChest = GameObject.Find("AllChest");
        bool[] AllChestOpen = new bool[AllChest.transform.childCount];
        for (int i = 0; i < AllChest.transform.childCount; i++)
        {
            if (AllChest.transform.GetChild(i).GetComponent<Chest>().OpenOrClosed)
                AllChestOpen[i] = true;
        }
        return AllChestOpen;
    }
}
