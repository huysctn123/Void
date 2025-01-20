using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Void.Interaction;
using Void.Interaction.Interactables;
using Void.UI;

namespace Void.CoreSystem
{
    public class DarkColumnInteract : CoreComponent
    {
        private InteractableDetector interactableDetector;
        private DarkColumn DarkColumn;
        private WeaponMenuManager weaponMenu;

        protected override void Awake()
        {
            base.Awake();
            this.interactableDetector = core.GetCoreComponent<InteractableDetector>();

        }
        private void HandleTryDarkColumnInteract(IInteractable interactable)
        {
            if (interactable is not DarkColumn darkColumn) return;
            DarkColumn = darkColumn;
            DarkColumn.Interact();
            TryGetWeaponMenu();
            weaponMenu.WeaponMenuActive();
        }
        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryDarkColumnInteract;
        }
        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryDarkColumnInteract;
        }
        private void TryGetWeaponMenu()
        {
            if (weaponMenu is not null) return;
            this.weaponMenu = GameObject.FindAnyObjectByType<WeaponMenuManager>().GetComponent<WeaponMenuManager>();
        }
    }
}