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
        WeaponsLoad(data.Weapons);
        player.weaponSwitch.handIsNotEmpty = data.HandIsNotEmpty;
        AllFirePlugsLoad(data.FirePlugsDestroy);
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
    public GameObject[] AllPistolPrefab;
    public GameObject[] AllAutoGunPrefab;
    private void WeaponsLoad(SaveData.DataWeapon[] Weapons)
    {
        GameObject player = GameObject.Find("Player");

        GameObject PistolParent = player.GetComponent<WeaponHold>().holdPointPistol.gameObject;
        GameObject AutoGunParent = player.GetComponent<WeaponHold>().holdPointAutomaticGun.gameObject;

        for (int i = 0; i < AllPistolPrefab.Length; i++)
        {
            if (Weapons[0].WeaponTag == AllPistolPrefab[i].tag && Weapons[0].WeaponID == AllPistolPrefab[i].GetComponent<Pistol>().ID)
            {
                GameObject obj = Instantiate(AllPistolPrefab[i], PistolParent.transform.GetChild(0));
                obj.GetComponent<Rigidbody2D>().simulated = false;
                obj.GetComponent<Pistol>().currentAmmo = Weapons[0].WeaponAmmoCount;
            }
        }
        for (int i = 0; i < AllAutoGunPrefab.Length; i++)
        {
            if (Weapons[1].WeaponTag == AllAutoGunPrefab[i].tag && Weapons[1].WeaponID == AllAutoGunPrefab[i].GetComponent<AutomaticGun>().ID)
            {
                GameObject obj = Instantiate(AllAutoGunPrefab[i], AutoGunParent.transform.GetChild(0));
                obj.GetComponent<Rigidbody2D>().simulated = false;
                obj.GetComponent<AutomaticGun>().currentAmmo = Weapons[1].WeaponAmmoCount;
            }
        }
    }
    public void AllFirePlugsLoad(bool[] AllFireplugsStatus)
    {
        GameObject AllFirePlugs = GameObject.Find("AllFirePlugs");
        for (int i = 0; i < AllFirePlugs.transform.childCount; i++)
        {
            AllFirePlugs.transform.GetChild(i).GetComponent<Fireplugs>().FirePlugsDestroy = AllFireplugsStatus[i];
        }
    }




    private Button buttonSave;
    public void ClickSave(Button button)
    {
        PlayerPrefs.SetInt("SaveMyScene", SceneManager.GetActiveScene().buildIndex);
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
            Weapons = SaveWeapons(),
            HandIsNotEmpty = player.weaponSwitch.handIsNotEmpty,
            FirePlugsDestroy = SaveAllFirePlugs(),
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
    public SaveData.DataWeapon[] a;
    public SaveData.DataWeapon[] SaveWeapons()
    {
        GameObject player = GameObject.Find("Player");

        GameObject PistolParent = player.GetComponent<WeaponHold>().holdPointPistol.gameObject;
        GameObject AutoGunParent = player.GetComponent<WeaponHold>().holdPointAutomaticGun.gameObject;
        SaveData.DataWeapon[] Weapons = new SaveData.DataWeapon[2];

        if (PistolParent.transform.GetChild(0).childCount == 1)
        {
            SaveData.DataWeapon Pistol = new SaveData.DataWeapon();
            Pistol.WeaponTag = PistolParent.transform.GetChild(0).GetChild(0).tag;
            Pistol.WeaponID = PistolParent.transform.GetChild(0).GetChild(0).GetComponent<Pistol>().ID;
            Pistol.WeaponAmmoCount = PistolParent.transform.GetChild(0).GetChild(0).GetComponent<Pistol>().currentAmmo;
            Weapons[0] = Pistol;
        }
        if (AutoGunParent.transform.GetChild(0).childCount == 1)
        {
            SaveData.DataWeapon AutoGun = new SaveData.DataWeapon();
            AutoGun.WeaponTag = AutoGunParent.transform.GetChild(0).GetChild(0).tag;
            AutoGun.WeaponID = AutoGunParent.transform.GetChild(0).GetChild(0).GetComponent<AutomaticGun>().ID;
            AutoGun.WeaponAmmoCount = AutoGunParent.transform.GetChild(0).GetChild(0).GetComponent<AutomaticGun>().currentAmmo;
            Weapons[1] = AutoGun;
        }
        a = Weapons;
        return Weapons;
    }
    public bool[] SaveAllFirePlugs()
    {
        GameObject AllFirePlugs= GameObject.Find("AllFirePlugs");
        bool[] AllFirePlugsStatus = new bool[AllFirePlugs.transform.childCount];
        for (int i = 0; i < AllFirePlugs.transform.childCount; i++)
        {
            if (AllFirePlugs.transform.GetChild(i).GetComponent<Fireplugs>().FirePlugsDestroy)
                AllFirePlugsStatus[i] = true;
        }
        return AllFirePlugsStatus;
    }
}
