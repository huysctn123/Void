using System.Collections;
using UnityEngine;
using Void.Combat.Damage;
using Void.CoreSystem;
using static Void.Utilities.CombatDamageUtilities;

namespace Void.Machine
{
    public class DamageOnActionHitbox : MachineComponents
    {
        private ActionHitBox hitBox;
        public float Amount;
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                TryDamage(colliders, new DamageData(Amount, this.gameObject), out _);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }

        protected override void Start()
        {
            base.Start();
            hitBox = GetComponent<ActionHitBox>();

            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }
    }
}