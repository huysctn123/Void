using System;
using System.Collections;
using UnityEngine;

namespace Void.Projectiles
{   
    
    public class projectileStickToCollider : ProjectileComponent
    {
        private void StickToCollider(Collider2D collision)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.parent = collision.transform;
            rb.gravityScale = 0.0f;
            projectile.IsGravityOn = false;
            Destroy(gameObject, UnityEngine.Random.Range(3, 5));
        }

        protected override void OnGroundTrigger(Collider2D collision)
        {
            base.OnGroundTrigger(collision);
            StickToCollider(collision);

        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}