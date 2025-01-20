using System.Collections;
using UnityEngine;
using Void.Weapons.Components;

namespace Void.Weapons.Components
{
    public class DrawData : ComponentData<AttackDraw>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Draw);
        }
    }
}