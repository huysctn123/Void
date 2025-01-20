using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.CoreSystem;
using Void.Interaction;
using Void.Interaction.Interactables;
using Void.Manager;

namespace Void.CoreSystem
{

    public class CheckPointInteract : CoreComponent
    {
        private Stats stats;
        private InteractableDetector interactableDetector;
        private CheckPoint CheckPoint;

        protected override void Awake()
        {
            base.Awake();
            interactableDetector = core.GetCoreComponent<InteractableDetector>();
            stats = core.GetCoreComponent<Stats>();

        }
        private void Start()
        {
            interactableDetector.OnTryInteract += HandleTryCheckPointInteract;
        }
        private void HandleTryCheckPointInteract(IInteractable interactable)
        {
            if (interactable is not CheckPoint checkPoint) return;
            CheckPoint = checkPoint;
            CheckPoint.Interact();
            this.stats.Health.Init();
            this.stats.Mana.Init();
            Player.Instance.potions.Init();
            SaveManager.Instance.CheckPointSaveGame();
        }
        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryCheckPointInteract;
        }
    }
}


