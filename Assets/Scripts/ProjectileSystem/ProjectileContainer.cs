using System.Collections;
using UnityEngine;

namespace Void.ProjectileSystem
{
    public class ProjectileContainer : VoidMonoBehaviour
    {
        public static ProjectileContainer Instance;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Only one Projectile Container in scene");
            }
        }
    }
}