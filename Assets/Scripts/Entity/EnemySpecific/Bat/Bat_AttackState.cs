using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Transform = UnityEngine.Transform;


public class Bat_AttackState : MeleeAttackState
{
    private Bat bat;

    private float Speed = 5f;
    private float attackTime = 0.5f;
    
    private float lastAttackTime;
    private bool canAttack;
    private bool isAttackFinish;
    private quaternion normalRotate;

    public Bat_AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, MeleeAttackStateData stateData, Bat bat) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.bat = bat;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (Time.time >= startTime + attackTime)
        {
            isAttackFinish = true;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isAttackFinish = false;
        MoveTowardsTarget();
        bat.OnTriggerEnter += TriggerAttack;
    }

    public override void Exit()
    {
        base.Exit();
        bat.transform.rotation = normalRotate;
        Movement?.SetVelocityZero();
        lastAttackTime = Time.time;
        bat.OnTriggerEnter -= TriggerAttack;
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Debug.Log("attack");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAttackFinish)
        {
            StateMachine.ChangeState(bat.flyIdleState);
        }
    }
    public void MoveTowardsTarget()
    {
        Vector3 Endpos = bat.PlayerPos.position;
        Vector3 StartPos = bat.transform.position;
        Vector2 direction = StartPos - Endpos;
        Movement.addforceToEntity(-direction * Speed, ForceMode2D.Impulse);

    }
    public void RotateTowardsTarget()
    {
        normalRotate = bat.transform.rotation;
        var offset = 0f;
        Vector3 Endpos = bat.PlayerPos.position;
        Vector3 StartPos = bat.transform.position;
        Vector2 direction = StartPos - Endpos;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bat.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    public bool CheckIfCanAttack()
    {
        return canAttack && Time.time >= lastAttackTime + bat.attackCoolDownTime;
    }
    public void ResetCanAttack() => canAttack = true;
}

