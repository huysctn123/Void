using System;
using UnityEngine;
using Void.Weapons;

namespace Void.Manager
{
    [Serializable]
    public class SaveData
    {
        [Header("Sound")]
        public float masterVolume = 1f;
        public float musicVolume = 1f;
        public float SFXVolume = 1f;

        [Header("Player")]
        public float CurrentHealValue = 100f;
        public float CurrentManaValue = 100f;
        public int facingDirection = 1;
        public Vector3 CheckPointPosition = new Vector3(-9.88f, -1.43f , 0f);
        public Vector3 SpawnPosition = new Vector3(0f, 0f , 0f);
        public WeaponDataSO[] weaponData = new WeaponDataSO[2];
        public int CurrentHealPotion;
        public int CurrentManaPotion;

        [Header("Score")]
        public int SaveSoul = 0;
        public int Soul = 0;

        [Header("Scene")]
        public string NextScene;
        public string CheckPointScene = "Level_1";

    }
}
