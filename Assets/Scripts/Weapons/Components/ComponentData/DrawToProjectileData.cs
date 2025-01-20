using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    public class DrawToProjectileData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(DrawToProjectile);
        }
    }
}