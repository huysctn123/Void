using System.Collections;
using UnityEngine;
using Void.Combat.Damage;
using Void.Combat.KnockBack;
using Void.Combat.PoiseDamage;
using Void.CoreSystem;
using Void.Manager;
public class MeleeAttackState : AttackState
{
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected MeleeAttackStateData stateData;

    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName, attackPosition)
    {
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.PlayerLayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
            }

            IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

            if (knockBackable != null)
            {
                knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
            }

            if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(new PoiseDamageData(stateData.PoiseDamage, core.Root));
            }
        }
    }

    public override void FXTrigger()
    {
        base.FXTrigger();
        var FX = GameObject.Instantiate(stateData.FX, entity.FXStartPos.transform.position,entity.FXStartPos.transform.rotation);
        FX.transform.localScale = entity.FXStartPos.transform.localScale;
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        audioPlay.PlayAudioClip(stateData.StateSound);
    }
}