using System.Collections;
using UnityEngine;


public class WolfMeleeAttack3State : MeleeAttackState
{
    private Wolf enemy;
    public WolfMeleeAttack3State(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Wolf enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public bool canMeleeAttack3 { get; private set; }
    private float lastAttackTime;
    public bool CheckIfCanAttack()
    {
        return canMeleeAttack3 && Time.time >= lastAttackTime + enemy.Attack3CoolDownTime;
    }
    public void ResetCanAttack() => canMeleeAttack3 = true;
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
    public override void StartMovement()
    {
        base.StartMovement();
        Movement.SetVelocityX(stateData.Speed * Movement.FacingDirection);
    }
    public override void StopMovement()
    {
        base.StopMovement();
        Movement.SetVelocityX(0f);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;

    }

    public override void Enter()
    {
        base.Enter();
        canMeleeAttack3 = false;
    }

}
