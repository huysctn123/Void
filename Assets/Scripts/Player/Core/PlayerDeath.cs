using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Void.CoreSystem;
using System;


public class PlayerDeath : Death
{
    public event Action OnPlayerDeath;
    public override void Die()
    {
        OnPlayerDeath?.Invoke();
        foreach (var particle in deathParcticles)
        {
            this.ParticleManager.StartParticles(particle, core.transform.parent.position, core.transform.parent.rotation);
        }
        core.transform.parent.gameObject.SetActive(false);

    }
}
