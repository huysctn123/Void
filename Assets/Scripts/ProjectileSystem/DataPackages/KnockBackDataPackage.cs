﻿using System;
using System.Collections;
using UnityEngine;

namespace Void.ProjectileSystem.DataPackages
{
    [Serializable]
    public class KnockBackDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Strength;
        [field: SerializeField] public Vector2 Angle;
    }
}