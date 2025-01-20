using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Void.CoreSystem
{
    public class CoreComp<T> where T : CoreComponent
    {
        private Core Core;
        private T comp;

        public T Comp => comp ? comp : Core.GetCoreComponent(ref comp);

        public CoreComp(Core core)
        {
            if(core == null)
            {
                UnityEngine.Debug.LogWarning("Core is Null for component {typeof(T)}");
            }
            this.Core = core;
        }

    }
}