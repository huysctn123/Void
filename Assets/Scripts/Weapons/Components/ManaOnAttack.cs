using System.Collections;
using UnityEngine;


namespace Void.Weapons.Components
{
    public class ManaOnAttack : WeaponComponent<ManaOnAttackData, AttackMana>
    {
        private CoreSystem.Stats coreStats;
        private bool isCanAttack;
        private void CheckCanAttack()
        {
            if (coreStats.Mana.CurrentValue >= currentAttackData.Amount) isCanAttack = true;
            else isCanAttack = false;         
        }
        private void Update()
        {
            CheckCanAttack();
            weapon.SetCanEnterAttack(isCanAttack);
        }
        public override void Init()
        {
            base.Init();
            coreStats = Core.GetCoreComponent<CoreSystem.Stats>();

            isCanAttack = false;
            HandleEnter();
        }
        private void OnAttackHandled()
        {
            coreStats.Mana.Decrease(currentAttackData.Amount);
        }
        #region Plumbing
        protected override void Start()
        {
            base.Start();
            weapon.OnEnter += OnAttackHandled;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            weapon.OnEnter -= OnAttackHandled;
        }
        #endregion
    }
}