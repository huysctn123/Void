using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Void.Projectiles
{
    public class ProjectileDestroyOnGround : ProjectileComponent
    {
        protected override void OnGroundTrigger(Collider2D collision)
        {
            base.OnGroundTrigger(collision);
            Destroy(gameObject);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}