using System.Collections;
using UnityEngine;

namespace Void.Combat.Parry
{
    public interface IParryable 
    {
        void Parry(ParryData data);
    }
}