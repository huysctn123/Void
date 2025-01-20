using System.Collections;
using System.Linq;
using UnityEngine;
using Void.Manager;

namespace Void.Weapons.Components
{
    public class WeaponSpriteAndSFX : WeaponComponent<WeaponSpriteAndSFXData, AttackSprites>
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weapoSpriteRenderer;
        private SoundManager SoundManager;
        private AudioClip WeaponSFX;

        private int currentWeaponSpriteIndex;
        private Sprite[] CurrentPhaseSprite;


        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentWeaponSpriteIndex = 0;
        }
        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            currentWeaponSpriteIndex = 0;
            CurrentPhaseSprite = currentAttackData.PhaseSprites.FirstOrDefault(data => data.phase == phase).Sprites;
        }
        private void HandleEnterAttackSound(AttackPhases phase)
        {
            WeaponSFX = currentAttackData.PhaseSprites.FirstOrDefault(data => data.phase == phase).phaseSound;
        }
        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if (!isAttackActive)
            {
                weapoSpriteRenderer.sprite = null;
                return;
            }
            if(currentWeaponSpriteIndex >= CurrentPhaseSprite.Length)
            {
                Debug.LogWarning($"{weapon.name} weapon Sprite length mismatch");
                return;
            }

            weapoSpriteRenderer.sprite = CurrentPhaseSprite[currentWeaponSpriteIndex];
            currentWeaponSpriteIndex++;
        }
        private void SoundTrigger()
        {
            SoundManager.PlaySoundFXClip(WeaponSFX);
        }

        protected override void Start()
        {
            base.Start();
            baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            weapoSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

            data = weapon.Data.GetData<WeaponSpriteAndSFXData>();
            SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackSound;
            AnimationEventHandler.WeaponSoundTrigger += SoundTrigger;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            AnimationEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
            AnimationEventHandler.OnEnterAttackPhase -= HandleEnterAttackSound;
            AnimationEventHandler.WeaponSoundTrigger -= SoundTrigger;
        }
    }
}