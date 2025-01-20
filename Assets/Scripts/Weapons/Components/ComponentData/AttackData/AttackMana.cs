using System;
using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    [Serializable]
    public class AttackMana : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}