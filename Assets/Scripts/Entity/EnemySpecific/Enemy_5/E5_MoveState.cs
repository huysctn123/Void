using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class E5_MoveState : MoveState
{
    private Enemy_5 enemy;
    public E5_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Enemy_5 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    private float Distance;
    private float speed;
    private float maxSpeed = 0.8f;
    private float closeDistanceAction = 3.3f;
    Vector3 followThroughPosition = new Vector3(2f, 0.5f, 0f);

    public override void DoChecks()
    {
        base.DoChecks();
        Vector2 direction = (enemy.transform.position - enemy.playerPos.position).normalized;
        Distance = Vector3.Distance(enemy.transform.position, enemy.playerPos.position);
        if (Movement.FacingDirection != direction.x / Mathf.Abs(direction.x))
        {
            Movement.Flip();
        }
        if(speed <= maxSpeed)
        {
            speed += (Time.deltaTime * stateData.MovementSpeed) / Distance;
        }
    }

    public override void Enter()
    {
        base.Enter();
        enemy.OnEnterMoveState();
        enemy.stats.Health.OnCurrentValueDecrease += CurrentHealhChange;
        enemy.meleeAttackState.ResetCanAttack();
        speed = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.OnExitMoveState();

        enemy.stats.Health.OnCurrentValueDecrease -= CurrentHealhChange;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.playerPos.position + new Vector3(followThroughPosition.x * Movement.FacingDirection, followThroughPosition.y, followThroughPosition.z), speed);
        if (Distance <= closeDistanceAction)
        {
            StateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
    private void CurrentHealhChange()
    {
        StateMachine.ChangeState(enemy.playerDetectedState);
    }
}
