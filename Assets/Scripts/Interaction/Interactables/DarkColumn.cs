using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.Manager.GameManager;
using Void.UI;

namespace Void.Interaction.Interactables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DarkColumn : VoidMonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject interactUI;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.interactUI = transform.Find("InteractUI").gameObject;
        }
        protected override void Start()
        {
            this.interactUI.SetActive(false);
        }
        public void DisableInteraction()
        {
            this.interactUI.SetActive(false);
        }

        public void EnableInteraction()
        {
            this.interactUI.SetActive(true);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Interact()
        {
            Debug.Log("Dark Column Interact");
            WeaponMenuManager.Instance.WeaponMenuActive();
        }

    }
}
