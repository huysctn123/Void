using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Void.CoreSystem;
using Void.Weapons;

namespace Void.UI
{
    public class EquippedWeaponUI : MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;

        [SerializeField] private CombatInputs input;
        [SerializeField] private WeaponInventory weaponInventory;

        private WeaponDataSO weaponData;

        private void Awake()
        {
            weaponInventory = GameObject.FindObjectOfType<Player>().GetComponentInChildren<WeaponInventory>();
        }
        private void SetWeaponIcon()
        {
            weaponIcon.sprite = weaponData ? weaponData.Icon : null;
            weaponIcon.color = weaponData ? Color.white : Color.clear;
        }

        private void HandleWeaponDataChanged(int inputIndex, WeaponDataSO data)
        {
            if (inputIndex != (int)input)
                return;

            weaponData = data;
            SetWeaponIcon();
        }

        private void Start()
        {
            weaponInventory.TryGetWeapon((int)input, out weaponData);
            SetWeaponIcon();
        }

        private void OnEnable()
        {
            weaponInventory.OnWeaponDataChanged += HandleWeaponDataChanged;
        }

        private void OnDisable()
        {
            weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
        }
    }
}