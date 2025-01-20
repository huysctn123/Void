using System.Collections;
using UnityEngine;

namespace Void.Projectiles
{
    public class ProjectileDestroyOnPlayer : ProjectileComponent
    {
        protected override void OnPlayerTrigger(Collider2D collision)
        {
            base.OnPlayerTrigger(collision);
            Destroy(gameObject);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}