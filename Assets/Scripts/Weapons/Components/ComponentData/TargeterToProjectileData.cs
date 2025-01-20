using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    public class TargeterToProjectileData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(TargeterToProjectile);
        }
    }
}