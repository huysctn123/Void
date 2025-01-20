using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void.Weapons.Components
{
    [Serializable]
    public class AttackOptionalSprite : AttackData
    {
        [field: SerializeField] public bool UseOptionalSprite { get; private set; }
        [field: SerializeField] public float Fps { get; private set; }
        [field: SerializeField] public Sprite[] Sprite { get; private set; }
    }
}