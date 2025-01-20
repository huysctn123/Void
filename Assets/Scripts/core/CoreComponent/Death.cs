using System.Collections;
using UnityEngine;
using Void.CoreSystem.StatsSystem;

namespace Void.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] protected GameObject[] deathParcticles;

        private ParticleManager particleManager;
        protected ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
        private Stats stats;
        protected Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);

        public virtual void Die()
        {
            foreach (var particle in deathParcticles)
            {
                ParticleManager.StartParticles(particle, core.transform.parent.position, core.transform.parent.rotation);
            }
            core.transform.parent.gameObject.SetActive(false);
        }
        protected virtual void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }
        protected virtual void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}