using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Void.Interaction.Interactables;
using Void.Manager;
using Void.Manager.GameManager;
using Void.Weapons;

namespace Void.UI 
{
    [RequireComponent(typeof(ShowUI))]
    public class WeaponMenuManager : MonoBehaviour
    {
        public static WeaponMenuManager Instance;
        public UnityEvent onDisable;
        public UnityEvent onEnable;

        [SerializeField] private List<WeaponIconUI> weaponIcons;
        [SerializeField] private List<GameObject> weapons;
        [SerializeField] private Button createButton;
        

        [HideInInspector] public ShowUI showUI;       

        public WeaponInfoUI weaponInfoUI;
        public TMP_Text text;

        private Transform playerPos;
        public WeaponDataSO WeaponData { get;private set; }
        public GameObject weapon { get; private set; }

        public bool canCreate { get; private set; }

        private void Awake()
        {
            if (WeaponMenuManager.Instance != null) Debug.LogError("Only WeaponMenuManager allow");
            WeaponMenuManager.Instance = this;

            this.showUI = GetComponent<ShowUI>();
        }
        private void Start()
        {
            this.GetPlayerPos();
            this.weaponInfoUI = GetComponentInChildren<WeaponInfoUI>();
            this.LoadWeaponIcon();
        }
        private void GetPlayerPos()
        {
            this.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public void setWeaponData(WeaponDataSO weaponData)
        {
            this.WeaponData = weaponData;
        }
        public void setWeapon(GameObject weapon)
        {
            this.weapon = weapon;
        }
        public void WeaponMenuActive()
        {
            this.showUI.PauseGame();
            this.showUI.ShowCanvas();
        }
        public void WeaponMenuDeactive()
        {
            this.showUI.UnPauseGame();
            this.showUI.HideCanvas();
        }
        public void HandleSwapChoice(WeaponDataSO weaponData)
        {
            this.weaponInfoUI.PopulateUI(weaponData);
        }
        public bool CanCreate(bool value)
        {
            canCreate = value;
            return canCreate;
        }
        public void HandleCreate()
        {
            this.CreateWeapon();
            ScoreManager.Instance.Decrease(WeaponData.Soul);
        }

        public void Create()
        {
            if (canCreate)
            {
                HandleCreate();
                WeaponMenuDeactive();
            }
            else
            {
                Debug.Log("not enough soul");
            }
        }
        public void CreateWeapon()
        {
            this.LoadOldWeaponObj();
            this.ClearOldWeapon();
            Vector3 Position = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
            GameObject newWeapon = Instantiate(weapon,Position, Quaternion.identity);
            var rigibody2d = newWeapon.GetComponent<Rigidbody2D>();
            rigibody2d.AddForce(new Vector2(7f, 7f), ForceMode2D.Impulse);
            this.AddWeaponToList(newWeapon);
        }
        private void ClearOldWeapon()
        {
            if (weapons.Count == 0) return;
            foreach (GameObject item in weapons)
            {
                Destroy(item);
            }
            weapons.Clear();
        }
        private void AddWeaponToList(GameObject weapon)
        {
            weapons.Add(weapon);
        }
        private void LoadWeaponIcon()
        {
            if (this.weaponIcons.Count > 0) return;
            WeaponIconUI[] Icons = GameObject.FindObjectsOfType<WeaponIconUI>();
            //this.weaponIcons = new List<WeaponIconUI>(Icons);

            foreach (WeaponIconUI icon in Icons)
            {
                this.weaponIcons.Add(icon);
            }
        }
        private void LoadOldWeaponObj()
        {
            WeaponPickup[] weapon = GameObject.FindObjectsOfType<WeaponPickup>();
            foreach (WeaponPickup pickup in weapon)
            {
                GameObject oldWeapon = pickup.gameObject;
                this.weapons.Add(oldWeapon);
            }
        }
        private void OnEnable()
        {
            this.createButton.onClick.AddListener(Create);
        }
        private void OnDisable()
        {
            this.createButton.onClick.RemoveListener(Create);
        }
    }
}
