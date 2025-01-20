using System;
using System.Collections;
using UnityEngine;
using Void.ProjectileSystem.DataPackages;

namespace Void.ProjectileSystem.DataPackages
{

    [Serializable]
    public class PoiseDamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}