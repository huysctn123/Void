using System.Collections;
using UnityEngine;
using Void.Combat.KnockBack;

namespace Void.Projectiles
{
    public class ProjectileKnockBack : ProjectileComponent
    {
        [Header("Knock Back")]
        [SerializeField] private Vector2 knockbackAngle;
        [SerializeField] private float knockbackStrength;
        private int Direction;
        [SerializeField] private LayerMask whatIsPlayer;

        protected override void OnPlayerTrigger(Collider2D collision)
        {
            base.OnPlayerTrigger(collision);
            KnockBackPlayerOnCollision(collision); ;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
        private void KnockBackPlayerOnCollision(Collider2D collision)
        {
            if (collision)
            {
                Direction = (int)Mathf.Sign(transform.forward.x);
                IKnockBackable knockBackable = collision.GetComponent<IKnockBackable>();

                if (knockBackable != null)
                {

                    knockBackable.KnockBack(new KnockBackData(knockbackAngle, knockbackStrength, Direction, collision.transform.parent.gameObject));
                }
            }
        }
    }
}