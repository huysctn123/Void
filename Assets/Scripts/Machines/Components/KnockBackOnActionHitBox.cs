using System.Collections;
using UnityEngine;
using Void.Combat.KnockBack;

namespace Void.Machine
{
    public class KnockBackOnActionHitBox : MachineComponents
    {
        private ActionHitBox hitBox;

        public float Strength;
        public Vector2 Angle;
        private int Direction;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                Direction = (int)Mathf.Sign(transform.forward.x);
                IKnockBackable knockBackable = item.GetComponent<IKnockBackable>();
                if (knockBackable != null)
                {
                    knockBackable.KnockBack(new KnockBackData(Angle, Strength, Direction, this.gameObject));
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}
