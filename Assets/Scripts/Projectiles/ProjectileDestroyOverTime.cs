using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Void.Projectiles
{
    public class ProjectileDestroyOverTime : ProjectileComponent
    {
        public float timeToDestroy;
        public float startTime { get; private set; }
        private void FixedUpdate()
        {
            startTime += Time.deltaTime;
            if(startTime >= timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}