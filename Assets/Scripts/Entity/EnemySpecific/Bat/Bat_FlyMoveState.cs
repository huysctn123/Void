using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class Bat_FlyMoveState : MoveState
{
    private Bat bat;
    public Bat_FlyMoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Bat bat) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.bat = bat;
    }
    private float Distance;
    private float closeDistanceAction = 4f;
    Vector3 followThroughPosition = new Vector3(2.5f, 1f, 0f);


    public override void DoChecks()
    {
        base.DoChecks();
        Vector2 direction = (bat.transform.position - bat.PlayerPos.position).normalized;
        Distance = Vector3.Distance(bat.transform.position, bat.PlayerPos.position);
        if (Movement.FacingDirection != direction.x / Mathf.Abs(direction.x))
        {
            Movement.Flip();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        Movement?.SetVelocityZero();
    }

    public override void FXTrigger()
    {
        base.FXTrigger();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        bat.transform.position = Vector3.Lerp(bat.transform.position, bat.PlayerPos.position + new Vector3(followThroughPosition.x * Movement.FacingDirection, followThroughPosition.y, followThroughPosition.z), stateData.MovementSpeed * Time.deltaTime);
        if (Distance <= closeDistanceAction && bat.attackState.CheckIfCanAttack())
        {
            bat.attackState.RotateTowardsTarget();
            StateMachine.ChangeState(bat.attackState);
        }
        //else if (Movement.FacingDirection != direction)
        //{
        //    StateMachine.ChangeState(bat.flyIdleState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SoundTrigger()
    {
        base.SoundTrigger();
    }
}
