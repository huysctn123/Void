using System.Collections;
using UnityEngine;
using Void.Interaction;
using Void.Interaction.Interactables;

namespace Void.CoreSystem
{
    public class DoorInteract : CoreComponent
    {
        private InteractableDetector interactableDetector;
        private Door Door;
        protected override void Awake()
        {
            base.Awake();
            this.interactableDetector = core.GetCoreComponent<InteractableDetector>();
        }

        private void Start()
        {
            interactableDetector.OnTryInteract += HandleTryDoorInteract;
        }
        private void HandleTryDoorInteract(IInteractable interactable)
        {
            if (interactable is not Door door) return;
            Door = door;
            Door.Interact();
        }

        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryDoorInteract;
        }
    }
}