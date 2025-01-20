using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using Void.CoreSystem;

namespace Void.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        private Movement movement;
        private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
        public Transform GroundCheck
        {
            get => groundCheck;
            private set => groundCheck = value;
        }
        public Transform WallCheck
        {
            get => wallCheck;
            private set => wallCheck = value;
        }
        public Transform WallFrontCheck
        {
            get => wallFrontCheck;
            private set => wallFrontCheck = value;
        }
        public Transform WallBackCheck
        {
            get => wallBackCheck;
            private set => wallBackCheck = value;
        }
        public Transform LedgeCheckHorizontal
        {
            get => ledgeCheckHorizontal;
            private set => ledgeCheckHorizontal = value;
        }
        public Transform LedgeCheckVertical
        {
            get => ledgeCheckVertical;
            private set => ledgeCheckVertical = value;
        }
        public Transform CeillingCheck
        {
            get => ceillingCheck;
            private set => ceillingCheck = value;
        }
        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
        public float LegdeCheckDistance { get => legdeCheckDistance; set => legdeCheckDistance = value; }
        public float CeilingCheckRadius { get => ceilingCheckRadius; set => ceilingCheckRadius = value; }
        public Vector2 GroundCheckSize { get => groundCheckSize; set => groundCheckSize = value; }
        public Vector2 WallCheckSize { get => wallCheckSize; set => wallCheckSize = value; }
        public LayerMask GroundLayer { get => groundLayer; set => groundLayer = value; }

        #region TranForms
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform wallFrontCheck;
        [SerializeField] private Transform wallBackCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private Transform ceillingCheck;
        #endregion

        #region Variables
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float legdeCheckDistance;
        [SerializeField] private float ceilingCheckRadius;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] protected bool debug;

        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private Vector2 wallCheckSize;
        #endregion

        #region Check
        public bool Ground { get => Physics2D.OverlapBox(GroundCheck.position, GroundCheckSize, 0, GroundLayer); }
        public bool WallClimb { get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, GroundLayer); }
        public bool WallFront { get => Physics2D.OverlapBox(WallFrontCheck.position, WallCheckSize, 0, GroundLayer); }
        public bool WallBack { get => Physics2D.OverlapBox(WallBackCheck.position, WallCheckSize, 0, GroundLayer); }
        public bool LedgeHorizontal { get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, legdeCheckDistance, GroundLayer); }
        public bool LedgeVertical { get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, legdeCheckDistance, GroundLayer); }
        public bool Ceiling { get => Physics2D.OverlapCircle(CeillingCheck.position, ceilingCheckRadius, GroundLayer); }
        #endregion

        private void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.color = Color.white;
                if(WallFrontCheck != null) Gizmos.DrawWireCube(WallFrontCheck.position, WallCheckSize);
                if(WallBackCheck != null) Gizmos.DrawWireCube(WallBackCheck.position, WallCheckSize);
                if(WallCheck) Gizmos.DrawRay(WallCheck.position, Vector2.right * wallCheckDistance * Movement.FacingDirection);
                if(LedgeCheckHorizontal != null) Gizmos.DrawRay(ledgeCheckHorizontal.position, Vector2.right * legdeCheckDistance * Movement.FacingDirection);
                if (CeillingCheck != null) Gizmos.DrawWireSphere(ceillingCheck.position, ceilingCheckRadius);
                Gizmos.color = Color.blue;
                if(GroundCheck != null)Gizmos.DrawWireCube(GroundCheck.position, GroundCheckSize);
            }
        }

    }
}