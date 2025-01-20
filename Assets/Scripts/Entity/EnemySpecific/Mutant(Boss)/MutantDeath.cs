using System.Collections;
using UnityEngine;

namespace Void.CoreSystem
{
    public class MutantDeath : Death
    {
        private Mutant mutant;
        private void Start()
        {
            mutant = GetComponentInParent<Mutant>();
        }
        public override void Die()
        {
            mutant.stateMachine.ChangeState(mutant.dieState);
            foreach (var particle in deathParcticles)
            {
                ParticleManager.StartParticles(particle);
            }           

        }
    }
}