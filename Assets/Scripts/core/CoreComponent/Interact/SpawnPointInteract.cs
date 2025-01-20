using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Void.Interaction.Interactables;
using Void.Interaction;
using Void.Manager;

namespace Void.CoreSystem
{
    public class SpawnPointInteract : CoreComponent
    {
        private InteractableDetector interactableDetector;
        private SpawnPoint SpawnPoint;

        protected override void Awake()
        {
            base.Awake();
            interactableDetector = core.GetCoreComponent<InteractableDetector>();

        }
        private void Start()
        {
            interactableDetector.OnTryInteract += HandleTryCheckPointInteract;
        }
        private void HandleTryCheckPointInteract(IInteractable interactable)
        {
            if (interactable is not SpawnPoint spawnPoint) return;
            SpawnPoint = spawnPoint;
            SpawnPoint.Interact();
        }
        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryCheckPointInteract;
        }
    }
}