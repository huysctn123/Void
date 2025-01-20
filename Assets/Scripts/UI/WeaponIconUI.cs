using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Void.Interaction.Interactables;
using Void.Manager;
using Void.Weapons;

namespace Void.UI
{
    public class WeaponIconUI : MonoBehaviour
    {
        public GameObject WeaponPrefab;
        [SerializeField] private WeaponDataSO weaponData;

        [SerializeField] private Image weaponIcon;

        private WeaponMenuManager WeaponMenuManager;
        private SpriteRenderer sprite;

        [SerializeField] private Button buton;

        [HideInInspector] public int soul;
        private void Awake()
        {
            this.buton = GetComponentInChildren<Button>();
        }
        private void Start()
        {
            WeaponMenuManager = GetComponentInParent<WeaponMenuManager>();
            this.weaponIcon.sprite = weaponData.Icon;
            this.soul = weaponData.Soul;
        }

        private void HandleChoiceRequest()
        {
            DataTransmission();
            GetSoulAmount();
            checkCanCreate();
        }
        private void GetSoulAmount()
        {
            WeaponMenuManager.text.text = weaponData.Soul.ToString();
        }
        private void DataTransmission()
        {
            this.WeaponMenuManager.weaponInfoUI.PopulateUI(weaponData);
            this.WeaponMenuManager.setWeapon(WeaponPrefab);
            this.WeaponMenuManager.setWeaponData(weaponData);
        }
        
        public void checkCanCreate()
        {
            if (ScoreManager.Instance.Soul >= this.soul) 
            {
                WeaponMenuManager.CanCreate(true);
            }
            else
            {
                WeaponMenuManager.CanCreate(false);
            }
        }
        public void OnEnable()
        {
            buton.onClick.AddListener(HandleChoiceRequest);
        }
        public void OnDisable()
        {
            buton.onClick.RemoveListener(HandleChoiceRequest);
        }
    }
        
}
