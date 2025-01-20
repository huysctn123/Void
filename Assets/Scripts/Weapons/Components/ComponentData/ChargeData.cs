using Void.Weapons.Components;

namespace Void.Weapons.Components
{
    public class ChargeData : ComponentData<AttackCharge>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Charge);
        }
    }
}
