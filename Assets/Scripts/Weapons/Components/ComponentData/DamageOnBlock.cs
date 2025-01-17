﻿using System.Collections;
using UnityEngine;
using Void.Combat.Damage;
using static Void.Utilities.CombatDamageUtilities;

namespace Void.Weapons.Components
{
    public class DamageOnBlock : WeaponComponent<DamageOnBlockData, AttackDamage>
    {
        private Block block;

        private void HandleBlock(GameObject blockedGameObject)
        {
            // Notice here the "out _". This indicates that in this case we do not care about the damageable that is assigned there
            TryDamage(blockedGameObject, new DamageData(currentAttackData.Amount, Core.Root), out _);
        }

        protected override void Start()
        {
            base.Start();

            block = GetComponent<Block>();

            block.OnBlock += HandleBlock;
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();

            block.OnBlock -= HandleBlock;
        }
    }
}