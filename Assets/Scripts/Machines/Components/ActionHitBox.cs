using System;
using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public class ActionHitBox : MachineComponents
    {

        public event Action<Collider2D[]> OnDetectedCollider2D;

        public LayerMask DetectableLayers;
        public Rect hitBox;
        public Collider2D[] detected;

        public float timeBetweenAction;
        private bool canHitAction;
        private void HandleHitAction()
        {
            detected = Physics2D.OverlapBoxAll(transform.position + (Vector3)hitBox.center, hitBox.size, transform.eulerAngles.z, DetectableLayers);

            if (detected.Length == 0)
                return;
            OnDetectedCollider2D?.Invoke(detected);
            canHitAction = false;
        }
        private void Update()
        {
            CheckCanAction();
        }
        private void OnDrawGizmosSelected()
        {
            //Gizmos.DrawWireCube((transform.position + (Vector3)hitBox.center), hitBox.size);
            Color prevColor = Gizmos.color;
            Matrix4x4 prevMatrix = Gizmos.matrix;

            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;

            Vector3 boxPosition = transform.position + (Vector3)hitBox.center;

            // convert from world position to local position 
            boxPosition = transform.InverseTransformPoint(boxPosition);
            Vector3 boxSize = hitBox.size;

            Gizmos.DrawWireCube(boxPosition, boxSize);

            // restore previous Gizmos settings
            Gizmos.color = prevColor;
            Gizmos.matrix = prevMatrix;
        }
        public void StartHitAction() => canHitAction = true;
        public void StopHitAction()
        {
            canHitAction = false;
        }
        protected virtual void CheckCanAction()
        {
            if (canHitAction && isActive)
            {
                HandleHitAction();
                StartCoroutine(TimeBetweenAction());
            }
        }

        protected override void Start()
        {
            base.Start();
            canHitAction = false;
            machineAnimationEventHandler.OnStartAction += StartHitAction;
            machineAnimationEventHandler.OnStopAction += StopHitAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            machineAnimationEventHandler.OnStartAction -= StartHitAction;
            machineAnimationEventHandler.OnStopAction -= StopHitAction;
        }
       IEnumerator TimeBetweenAction()
        {

            yield return new WaitForSeconds(timeBetweenAction);
            if (isActive)
            {
                canHitAction = true;
            }
            else
            {
                canHitAction = false;

            }
        }

        protected override void Exit()
        {
            base.Exit();
            canHitAction = false;
        }
    }
}
