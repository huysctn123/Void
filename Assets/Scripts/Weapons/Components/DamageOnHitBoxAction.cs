﻿using UnityEngine;
using Void.Combat.Damage;
using static Void.Utilities.CombatDamageUtilities;

namespace Void.Weapons.Components
{
    public class DamageOnHitBoxAction : WeaponComponent<DamageOnHitBoxActionData, AttackDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            // Notice that this is equal to (1), the logic has just been offloaded to a static helper class. Notice the using statement (2) is static, allowing as to call the Damage function directly instead of saying
            TryDamage(colliders, new DamageData(currentAttackData.Amount, Core.Root), out _);

            //(1)
            // foreach (var item in colliders)
            // {
            //     if (item.TryGetComponent(out IDamageable damageable))
            //     {
            //         damageable.Damage(new Combat.Damage.DamageData(currentAttackData.Amount, Core.Root));
            //     }
            // }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();

            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}