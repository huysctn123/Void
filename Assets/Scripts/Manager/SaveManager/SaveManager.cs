using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Void.CoreSystem;
using Void.CoreSystem.StatsSystem;
using Void.Manager.Scene;

namespace Void.Manager
{
    public class SaveManager : MonoBehaviour
    {
        public event Action OnLoadSoundVolume;

        public static SaveManager Instance;

        public const string SAVE_1 = "save_1";
        [SerializeField] protected string jsonString = "";
        [SerializeField] protected SaveData saveData = new SaveData();

        public bool isNullData { get; private set; }

        private void Awake()
        {
            if (SaveManager.Instance != null) Debug.LogError("Only SaveManager allow");
            SaveManager.Instance = this;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                this.SaveSceneData();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                this.LoadSceneData();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                this.LoadCheckPointSave();
            }
        }
        public bool IsNullData()
        {
            string jsonString = SaveSystem.GetString(SaveManager.SAVE_1);

            this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

            if (this.saveData == null) isNullData = true;
            else isNullData = false;
            return isNullData;
        }
        public void CheckPointSaveGame()
        {
            this.SaveCheckPointScene(saveData);
            this.SaveSoul(saveData);
            this.SaveCheckPointPosition(Player.Instance.transform.position);
            this.SavePlayerFacingDirection(saveData);
            this.SaveWeaponData(saveData);

            this.SaveGame();
            Debug.Log("CheckPointSaveGame");
        }
        public void LoadCheckPointSave()
        {
            string jsonString = SaveSystem.GetString(SaveManager.SAVE_1);

            this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

            if (this.saveData == null) this.saveData = this.NewSaveGame();
            else
            {

                Debug.Log("LoadCheckPointSave");
                this.LoadCheckPointScene();
                this.LoadCheckPointPosition(saveData.CheckPointPosition);
                this.LoadSoundVolume(saveData);
                this.LoadSoulScore(saveData);
                Player.Instance.Stats.Health.Init();
                Player.Instance.Stats.Mana.Init();
                this.LoadWeaponData(saveData);
                this.LoadPlayerFacingDirection(saveData);
                Player.Instance.gameObject.SetActive(true);
            }
        }
        public void LoadSceneData()
        {
            string jsonString = SaveSystem.GetString(SaveManager.SAVE_1);

            this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

            if (this.saveData == null) this.saveData = this.NewSaveGame();
            else
            {
                Debug.Log("LoadSceneData");
                this.LoadSoundVolume(saveData);
                this.LoadSoulScore(saveData);
                this.LoadSpawnPosition(saveData.SpawnPosition);
                this.LoadStatValue(saveData);
                this.LoadWeaponData(saveData);
                this.LoadPotionData(saveData);
                this.LoadPlayerFacingDirection(saveData);
            }
        }
        public void SaveSceneData()
        {
            Debug.Log("SaveSceneData");
            this.SaveSoundVolume(saveData);
            this.SaveSoulScore(saveData);
            this.SaveCurrentStatValue(saveData);
            this.SavePotionData(saveData);
            this.SaveWeaponData(saveData);
            this.SavePlayerFacingDirection(saveData);

            this.SaveGame();
        }
        private SaveData NewSaveGame()
        {
            SaveData saveData = new SaveData { };
            return saveData;
        }
        public void SaveGame()
        {
            string newjsonString = JsonUtility.ToJson(this.saveData);
            this.jsonString = newjsonString;
            SaveSystem.SetString(SaveManager.SAVE_1, jsonString);
            SaveSystem.SaveToDisk();
            Debug.Log("SaveGame");
        }

        #region LOAD DATA
        public void LoadSoundVolume(SaveData saveData)
        {
            SoundManager.Instance.SetMasterVolume(saveData.masterVolume);
            SoundManager.Instance.SetMusicVolume(saveData.musicVolume);
            SoundManager.Instance.SetSFXVolume(saveData.SFXVolume);
            OnLoadSoundVolume?.Invoke();
        }
        
        private void LoadSoulScore(SaveData saveData)
        {
            ScoreManager.Instance.LoadSoulAmount(saveData.Soul);
        }
        private void LoadStatValue(SaveData saveData)
        {
            var stat = Player.Instance.core.GetCoreComponent<Stats>();
            stat.Health.SetCurrentValue(saveData.CurrentHealValue);
            stat.Mana.SetCurrentValue(saveData.CurrentManaValue);
        }
        private void LoadSpawnPosition(Vector3 Position)
        {
            Player.Instance.transform.position = Position;
        }
        private void LoadCheckPointPosition(Vector3 Position)
        {
            Player.Instance.transform.position = Position;
        }
        private void LoadWeaponData(SaveData saveData)
        {
            for(var i = 0; i < Player.Instance.core.GetCoreComponent<WeaponInventory>().weaponData.Length; i++)
            {
                Player.Instance.core.GetCoreComponent<WeaponInventory>().weaponData[i] = saveData.weaponData[i];
                Player.Instance.core.GetCoreComponent<WeaponInventory>().TrySetWeapon(saveData.weaponData[i], i, out _);
            }
        }
        private void LoadPotionData(SaveData saveData)
        {
            Player.Instance.potions.SetCurrentPotionValue(saveData.CurrentHealPotion, saveData.CurrentManaPotion);
        }
        private void LoadPlayerFacingDirection(SaveData saveData)
        {
            if(Player.Instance.core.GetCoreComponent<Movement>().FacingDirection != saveData.facingDirection)
            {
                Player.Instance.core.GetCoreComponent<Movement>().Flip();
            }
        }
        private void LoadSaveScene(SaveData saveData)
        {
            SceneLoadManager.Instance.GetLoadScene(saveData.NextScene);
        }
        public void LoadCheckPointScene()
        {
            string jsonString = SaveSystem.GetString(SaveManager.SAVE_1);

            this.saveData = JsonUtility.FromJson<SaveData>(jsonString);
            SceneLoadManager.Instance.GetLoadScene(saveData.CheckPointScene);
        }
        #endregion

        public void SaveVolume()
        {
            this.SaveSoundVolume(saveData);
            this.SaveGame();
        }
        public void LoadVolume()
        {
            string jsonString = SaveSystem.GetString(SaveManager.SAVE_1);

            this.saveData = JsonUtility.FromJson<SaveData>(jsonString);

            if (this.saveData == null) this.saveData = this.NewSaveGame();
            else
            {
                this.LoadSoundVolume(this.saveData);
            }
        }
        #region SAVE DATA

        private void SaveSoundVolume(SaveData saveData)
        {
            if (saveData == null) return;
            saveData.masterVolume = SoundManager.Instance.MasterVolume;
            saveData.musicVolume = SoundManager.Instance.MusicVolume;
            saveData.SFXVolume = SoundManager.Instance.SFXVolume;
        }
        private void SaveSoul(SaveData saveData)
        {

            saveData.SaveSoul = ScoreManager.Instance.Soul;
        }
        private void SaveSoulScore(SaveData saveData)
        {

            saveData.Soul = ScoreManager.Instance.Soul;
        }
        private void SaveCurrentStatValue(SaveData saveData)
        {
            if (Player.Instance.Stats.Health.CurrentValue <= 0)
            {
                Player.Instance.Stats.Health.Init();
                Player.Instance.Stats.Mana.Init();
                return;
            }
            saveData.CurrentHealValue = Player.Instance.Stats.Health.CurrentValue;
            saveData.CurrentManaValue = Player.Instance.Stats.Mana.CurrentValue;
        }
        public void SaveSpawnPosition(Vector3 position)
        {
            saveData.SpawnPosition = position;
        }
        public void SaveCheckPointPosition(Vector3 position)
        {
            saveData.CheckPointPosition = position;
        }
        public void SavePlayerFacingDirection(SaveData saveData)
        {
            saveData.facingDirection = Player.Instance.core.GetCoreComponent<Movement>().FacingDirection;
        }
        public void SaveWeaponData(SaveData saveData)
        {

            for (var i = 0; i < Player.Instance.core.GetCoreComponent<WeaponInventory>().weaponData.Length; i++)
            {
                saveData.weaponData[i] = Player.Instance.core.GetCoreComponent<WeaponInventory>().weaponData[i];
            }
        }
        public void SavePotionData(SaveData saveData)
        {
            saveData.CurrentHealPotion = Player.Instance.potions.CurrentHealPotionUseLeft;
            saveData.CurrentManaPotion = Player.Instance.potions.CurrentManaPotionUseLeft;
        }
        private void SaveCheckPointScene(SaveData saveData)
        {
            saveData.CheckPointScene = SceneLoadManager.Instance.CurrentSceneName;
        }
        private void SaveNextScene()
        {
            saveData.NextScene = SceneLoadManager.Instance.LoadSceneName;
        }
        #endregion
    }
}
