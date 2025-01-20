using Void.Weapons;
using UnityEngine;

namespace Void.Interaction.Interactables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        
        public static WeaponPickup Instance;

        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        [SerializeField] private SpriteRenderer weaponIcon;
        [SerializeField] private Bobber bobber;

        [SerializeField] private WeaponDataSO WeaponData;
        [SerializeField] private GameObject interactUI;

        public WeaponDataSO GetContext() => WeaponData;
        public void SetContext(WeaponDataSO context)
        {
            WeaponData = context;
            weaponIcon.sprite = WeaponData.Icon;
        }

        public void Interact()
        {
            Destroy(gameObject);
        }

        public void EnableInteraction()
        {
            this.interactUI.SetActive(true);
            bobber.StartBobbing();
        }    
        public void DisableInteraction()
        {
            bobber.StopBobbing();
            this.interactUI.SetActive(false);

        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        protected void Awake()
        {

            interactUI = transform.Find("InteractUI").gameObject;

            Rigidbody2D ??= GetComponent<Rigidbody2D>();
            weaponIcon ??= GetComponentInChildren<SpriteRenderer>();

            if (WeaponData is null)
                return;

            weaponIcon.sprite = WeaponData.Icon;
        }
        protected void Start()
        {
            this.interactUI.SetActive(false);
        }    
    }
}