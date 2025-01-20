using System;
using System.Collections;
using UnityEngine;

namespace Void.CoreSystem
{
    public class Movement : CoreComponent
    {
        public event Action isFlip;
        public Rigidbody2D RB { get; private set; }
        public int FacingDirection { get; private set; }
        public bool canSetVelocity { get;  set; }

        
        public Vector2 CurrentVelocity { get; private set; }

        private Vector2 workSpace;


        protected override void Awake()
        {
            base.Awake();
            
            RB = GetComponentInParent<Rigidbody2D>();
            FacingDirection = 1;
            canSetVelocity = true;
        }
        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }
        public void SetVelocityZero()
        {
            workSpace = Vector2.zero;
            SetFinalVelocity();
        }
        
        public void SetVelocity(float velocity, Vector2 direction)
        {
            workSpace = direction * velocity;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
            SetFinalVelocity();

        }
        public void SetVelocityX(float Velocity)
        {
            workSpace.Set(Velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }
        public void SetVelocityY(float Velocity)
        {
            workSpace.Set(CurrentVelocity.x, Velocity);
            SetFinalVelocity();
        }
        private void SetFinalVelocity()
        {
            if (canSetVelocity)
            {
                RB.velocity = workSpace;
                CurrentVelocity = workSpace;
            }
        }
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }
        public void Flip()
        {
            FacingDirection *= -1;
            RB.transform.Rotate(0, 180, 0);
            isFlip?.Invoke();
        }

        public void SetGravity(float value)
        {
            RB.gravityScale = value;         
        }
        public Vector2 FindRelativePoint(Vector2 offset)
        {
            offset.x *= FacingDirection;
            return transform.position + (Vector3)offset;
        }
        public void addforceToEntity(Vector2 force, ForceMode2D forceMode2D)
        {
            RB.AddForce( force, forceMode2D);
        }
    }
}