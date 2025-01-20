using System;
using System.Collections;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager.GameManager;
using Void.Weapons;

namespace Void.UI
{
    public class WeaponSwapUI : VoidMonoBehaviour
    {
        [SerializeField] private WeaponSwap weaponSwap;
        [SerializeField] private WeaponInfoUI newWeaponInfo;
        [SerializeField] private WeaponSwapChoiceUI[] weaponSwapChoiceUIs;
        private CanvasGroup canvasGroup;

        private Action<WeaponSwapChoice> choiceSelectedCallback;

        private void HandleChoiceRequested(WeaponSwapChoiceRequest choiceRequest)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.UI);

            choiceSelectedCallback = choiceRequest.Callback;

            newWeaponInfo.PopulateUI(choiceRequest.NewWeaponData);

            foreach (var weaponSwapChoiceUi in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUi.TakeRelevantChoice(choiceRequest.Choices);
            }

            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private void HandleChoiceSelected(WeaponSwapChoice choice)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);

            choiceSelectedCallback?.Invoke(choice);
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        protected override void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
        protected override void Start()
        {
            base.Start();
            weaponSwap = Player.Instance.core.GetCoreComponent<WeaponSwap>();

            weaponSwap.OnChoiceRequested += HandleChoiceRequested;

            foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUI.OnChoiceSelected += HandleChoiceSelected;
            }
        }
        protected override void OnDisable()
        {
            weaponSwap.OnChoiceRequested -= HandleChoiceRequested;

            foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUI.OnChoiceSelected -= HandleChoiceSelected;
            }
        }
    }
}