using System.Collections;
using UnityEngine;
using Void.Combat.Damage;

namespace Void.Projectiles
{
    public class ProjectileDameOverTime : ProjectileComponent
    {

        [SerializeField] private float projectileDamage = 10f;
        [SerializeField] float timeBetweenDamage;
        [SerializeField] private LayerMask playerLayer;
        private float LastDamageTime;
        private bool canDamage;
        private bool isPlayer;
        private void HurtPlayerOnCollision(Collider2D collision)
        {
            if (!collision) return;
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                //Debug.Log($"Collision with {collision.gameObject.name}");
                damageable.Damage(new DamageData(projectileDamage, collision.transform.parent.gameObject));
                canDamage = false;
                LastDamageTime = Time.time;
            }
        }
        private void Update()
        {
            CheckCanDamage();
        }
        private void CheckCanDamage()
        {
            if (!isPlayer) return;
            if (Time.time >= LastDamageTime + timeBetweenDamage)
            {
                canDamage = true;
            }
        }
        protected override void OnPlayerTrigger(Collider2D collision)
        {
            base.OnPlayerTrigger(collision);
            isPlayer = true;
        }
        protected override void OnPlayerExitTrigger(Collider2D collision)
        {
            base.OnPlayerExitTrigger(collision);
            isPlayer = false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!canDamage) return;
            HurtPlayerOnCollision(collision);
        }
    }
}