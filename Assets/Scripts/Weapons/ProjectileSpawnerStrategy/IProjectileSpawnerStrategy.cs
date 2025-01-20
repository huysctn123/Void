using System;
using UnityEngine;
using Void.ObjectPoolSystem;
using Void.ProjectileSystem;
using Void.Weapons.Components;

namespace Void.Weapons
{
    public interface IProjectileSpawnerStrategy 
    {
        void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection,
                   ObjectPools objectPools, Action<Projectile> OnSpawnProjectile);
    }
}