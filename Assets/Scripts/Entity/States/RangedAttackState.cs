using System.Collections;
using UnityEngine;
using Void.CoreSystem;
using Void.Projectiles;
using Void.Manager;


public class RangedAttackState : AttackState
{
    protected RangedAttackStateData stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;
    protected ProjectileDamage damageScript;

    public RangedAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, RangedAttackStateData stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        audioPlay.PlayAudioClip(stateData.StateSound);
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        damageScript = projectile.GetComponent<ProjectileDamage>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance);
        damageScript.ChangeProjectileDamage(stateData.projectileDamage);
    }

}
