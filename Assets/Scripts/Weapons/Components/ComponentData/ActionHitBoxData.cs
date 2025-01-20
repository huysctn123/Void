﻿using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {

        [field:SerializeField] public LayerMask DetectableLayers { get; private set; }
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}