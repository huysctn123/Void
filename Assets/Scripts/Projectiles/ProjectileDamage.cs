using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Void.Combat.Damage;

namespace Void.Projectiles
{
    public class ProjectileDamage : ProjectileComponent
    {
        [SerializeField] private float projectileDamage = 10f;

        [SerializeField] private LayerMask playerLayer;

        public void ChangeProjectileDamage(float amount)
        {
            this.projectileDamage = amount;
        }
        protected override void OnPlayerTrigger(Collider2D collision)
        {
            base.OnPlayerTrigger(collision);
            HurtPlayerOnCollision(collision);
        }
        private void HurtPlayerOnCollision(Collider2D collision)
        {
            if (!collision) return;
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                //Debug.Log($"Collision with {collision.gameObject.name}");
                damageable.Damage(new DamageData(projectileDamage, collision.transform.parent.gameObject));
            }
        }
    }
}