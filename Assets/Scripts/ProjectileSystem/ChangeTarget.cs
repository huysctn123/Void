using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Void.ProjectileSystem.Components;

namespace Void.ProjectileSystem
{
    public class ChangeTarget : ProjectileComponent
    {
        private DirectTowardsTarget directTowardsTarget;

        private List<Transform> newTargets;
        private float startTime;
        private Transform currentTarget;

        [SerializeField] private float delayTime;

        [field: SerializeField] public Rect RectBox { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        protected override void Init()
        {
            base.Init();
            startTime = Time.time;
        }
        private void ChangeTargets()
        {
            Debug.Log("gettarget");
            this.directTowardsTarget.GetTarget(newTargets);
        }
        protected override void Awake()
        {
            base.Awake();
            this.directTowardsTarget = GetComponent<DirectTowardsTarget>();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (Time.time <= startTime + delayTime) return;
            CheckForTargets();
            if (!HasTarget())
                return;
            ChangeTargets();

        }
        private void CheckForTargets()
        {
            var pos = transform.position +
                      new Vector3(RectBox.center.x, RectBox.center.y);

            var targetColliders =
                Physics2D.OverlapBoxAll(pos, RectBox.size, 0f, LayerMask);

            newTargets = targetColliders.Select(item => item.transform).ToList();
        }
        private bool HasTarget()
        {
            //if (currentTarget)
            //    return true;

            newTargets.RemoveAll(item => item == null);

            if (newTargets.Count <= 0)
                return false;

            newTargets = newTargets.OrderBy(target => (target.position - transform.position).sqrMagnitude).ToList();
            currentTarget = newTargets[0];
            directTowardsTarget.CurrentTarget = currentTarget;
            return true;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position + (Vector3)RectBox.center, RectBox.size);
        }
    }
}