using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Events;
using Void.Combat.Damage;
using Void.ProjectileSystem.Components;
using Void.ProjectileSystem.DataPackages;
using Void.Utilities;

namespace Void.ProjectileSystem
{   /*
     * The Damage component is responsible for using information provided by the HitBox component to damage any entities that are on the relevant LayerMask
     * The damage amount comes from the weapon via the ProjectileDataPackage system.
     */
    public class Damage : ProjectileComponent
    {
        public UnityEvent<IDamageable> OnDamage;
        public UnityEvent<RaycastHit2D> OnRaycastHit;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public bool SetInactiveAfterDamage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }

        private HitBox hitBox;

        [SerializeField] private float amount;

        private float lastDamageTime;

        protected override void Init()
        {
            base.Init();

            lastDamageTime = Mathf.NegativeInfinity;
        }

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active)
                return;

            if (Time.time < lastDamageTime + Cooldown)
                return;

            foreach (var hit in hits)
            {
                // Is the object under consideration part of the LayerMask that we can damage?
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;

                // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
                if (!hit.collider.transform.gameObject.TryGetComponent(out IDamageable damageable))
                    continue;
                if(damageable != null)
                {
                    damageable.Damage(new DamageData(amount, projectile.gameObject));
                    Debug.Log(projectile.name);
                }

                OnDamage?.Invoke(damageable);
                OnRaycastHit?.Invoke(hit);

                lastDamageTime = Time.time;

                if (SetInactiveAfterDamage)
                {
                    SetActive(false);
                }

                return;
            }
        }

        // Handles checking to see if the data is relevant or not, and if so, extracts the information we care about
        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DamageDataPackage package)
                return;
            amount = package.Amount;
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<HitBox>();

            hitBox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);
        }

        #endregion
    }
}