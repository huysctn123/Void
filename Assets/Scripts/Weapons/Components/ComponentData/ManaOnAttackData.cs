using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    public class ManaOnAttackData : ComponentData<AttackMana>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ManaOnAttack);
        }
    }
}