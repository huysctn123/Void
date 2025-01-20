using System;
using System.Collections;
using UnityEngine;
using static Void.Manager.GameManager.GameManager;

namespace Void.UI
{
    public class GetPotionData : VoidMonoBehaviour
    {
        public Potion CurrentPotion;
        public int currentUseTimeLeft { get; private set; }

        public PlayerPotions playerPotions;

        protected override void Start()
        {
            base.Start();
            //SelectPotionData(CurrentPotion);
            StartCoroutine(GetPlayerPotion());
        }
        IEnumerator GetPlayerPotion()
        {
            yield return new WaitForFixedUpdate();
            this.playerPotions = Player.Instance.potions;
            SelectPotionData(CurrentPotion);
        }
        private void UpdateCurrentUseTimeLeft()
        {
            SelectPotionData(CurrentPotion);
        }
        private void SelectPotionData(Potion potion)
        {
            switch (potion)
            {
                case Potion.HealPotion:
                    currentUseTimeLeft = Player.Instance.potions.CurrentHealPotionUseLeft;
                    playerPotions.OnHealUseChange += UpdateCurrentUseTimeLeft;
                    break;
                case Potion.ManaPotion:
                    currentUseTimeLeft = Player.Instance.potions.CurrentManaPotionUseLeft;
                    playerPotions.OnManaUseChange += UpdateCurrentUseTimeLeft;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(potion), potion, null);
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            playerPotions.OnHealUseChange -= UpdateCurrentUseTimeLeft;
            playerPotions.OnManaUseChange -= UpdateCurrentUseTimeLeft;
        }
    }
    
    public enum Potion
    {
        HealPotion,
        ManaPotion
    }
}