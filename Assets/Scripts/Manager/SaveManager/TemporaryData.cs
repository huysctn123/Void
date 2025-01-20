using System;
using System.Collections;
using UnityEngine;
using Void.Weapons;

namespace Void.Manager {

    [Serializable]
    public class TemporaryData 
    {
        [Header("Sound")]
        public float masterVolume = 0f;
        public float musicVolume = 0f;
        public float SFXVolume = 0f;

        [Header("Player")]
        public float CurrentHealValue = 100f;
        public int facingDirection = 1;
        public Vector3 SpawnPosition = new Vector3(0f, 0f, 0f);
        public WeaponDataSO[] weaponData;

        [Header("Score")]
        public int Soul = 0;
    }
}