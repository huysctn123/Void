using System.Collections;
using UnityEngine;

namespace Void.Combat.PoiseDamage
{
    public interface IPoiseDamageable 
    {
        void DamagePoise(PoiseDamageData data);
    }
}