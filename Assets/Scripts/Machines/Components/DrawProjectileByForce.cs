using System.Collections;
using UnityEngine;
using Void.Projectiles;

namespace Void.Machine
{
    public class DrawProjectileByForce : MachineComponents
    {
        public GameObject _projectile;
        public Transform drawPostion;
        private Projectile projectileScript;
        public Vector2 force;

        protected override void Start()
        {
            machine.OnEnter += FireProjectile;
        }
        protected override void OnDisable()
        {
            machine.OnExit -= FireProjectile;
        }
        void FireProjectile()
        {
            var projectile = GameObject.Instantiate(_projectile, drawPostion.position, drawPostion.rotation);
            projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.FireProjectile(force);
        }
    }
}