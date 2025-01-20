using System.Collections;
using UnityEngine;

namespace Void.Projectiles
{
    public class ProjectileOnCollider : ProjectileComponent
    {
        [SerializeField] private GameObject OnPlayerTriggerOBJ;
        [SerializeField] private GameObject OnGroundTriggerOBJ;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;

        protected override void OnGroundTrigger(Collider2D collision)
        {
            base.OnGroundTrigger(collision);
            if (!OnGroundTriggerOBJ) return;
            Instantiate(OnGroundTriggerOBJ, transform);

        }

        protected override void OnPlayerTrigger(Collider2D collision)
        {
            base.OnPlayerTrigger(collision);
            if (!OnPlayerTriggerOBJ) return;
            Instantiate(OnPlayerTriggerOBJ, transform);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}