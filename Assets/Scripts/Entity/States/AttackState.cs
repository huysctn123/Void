using System.Collections;
using UnityEngine;
using Void.CoreSystem;

public class AttackState : EntityState
{

    
    
    private Movement movement;
    private ParryReceiver parryReceiver;

    protected Transform attackPosition;


    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;

        movement = core.GetCoreComponent<Movement>();
        parryReceiver = core.GetCoreComponent<ParryReceiver>();

    }
    public override void Enter()
    {
        base.Enter();

        animToState.attackState = this;
        animToState.AttackTrigger += TriggerAttack;
        animToState.AttackFinish += FinishAttack;

        isAnimationFinished = false;
        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        animToState.AttackTrigger -= TriggerAttack;
        animToState.AttackFinish -= FinishAttack;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }

    public void SetParryWindowActive(bool value) => parryReceiver.SetParryColliderActive(value);

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }
}
