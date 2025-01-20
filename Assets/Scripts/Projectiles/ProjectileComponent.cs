
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Void.Projectiles
{
    public abstract class ProjectileComponent : MonoBehaviour
    {

        protected Projectile projectile;
        protected Rigidbody2D rb;
        protected BoxCollider2D boxCollider;

        protected virtual void Start()
        {
            this.projectile = GetComponent<Projectile>();
            this.rb = projectile.rb;
            this.boxCollider = projectile.boxCollider;
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                OnPlayerTrigger(collision);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                OnGroundTrigger(collision);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                OnPlayerExitTrigger(collision);
            }
        }
        protected virtual void OnPlayerTrigger(Collider2D collision)
        {
        }
        protected virtual void OnGroundTrigger(Collider2D collision)
        {
        }
        protected virtual void OnPlayerExitTrigger(Collider2D collision)
        {
        }

    }
}