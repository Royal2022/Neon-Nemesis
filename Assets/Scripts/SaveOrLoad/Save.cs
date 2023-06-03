using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    public class DataItems
    {
        public string ItemTag;
        public Vector3 ItemPosition;
    }

    [System.Serializable]
    public class Save
    {
        public float Healt;
        public float Armor;
        public int Money;
        public int PistolAmmo;
        public int AutoGunAmmo;
        public int GranadeCount;
        public Vector3 PlayerPosition;

        public bool[] EnemyAlive;
        public DataItems[] ItemsExist;
        public bool[] StreetLampExist;
        public bool[] ChestOpen;
    }
}



