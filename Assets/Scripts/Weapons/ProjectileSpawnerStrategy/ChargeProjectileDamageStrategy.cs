using System;
using System.Collections;
using UnityEngine;

namespace Void.Weapons
{
    [Serializable]
    public class ChargeProjectileDamageStrategy : ProjectileSpawnerStrategy
    {
        public float startTime;
        public float chargeTime;
        
    }
}