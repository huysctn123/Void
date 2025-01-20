using System;
using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public PhaseSprite[] PhaseSprites { get; private set; }
    }
    [Serializable]
    public struct PhaseSprite
    {
        [field: SerializeField] public AttackPhases phase { get; private set; }
          
        [field: SerializeField] public Sprite[] Sprites { get; private set; }

        [field: SerializeField] public AudioClip phaseSound { get; private set; }
    }
}