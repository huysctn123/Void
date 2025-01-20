using Void.CoreSystem;
using System;

public class PlayerTouchingWallState : PlayerState
{
    protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    private Movement movement;
    private CollisionSenses collisionSenses;
    public PlayerTouchingWallState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
        base(Player, stateMachine, playerData, animBoolName)
    {
    }

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallClimb;
    protected bool jumpInput;
    protected bool grabInput;
    protected bool isTouchingLedge;
    protected int xInput;
    protected int yInput;

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandle.NormInputX;
        yInput = player.InputHandle.NormInputY;
        jumpInput = player.InputHandle.JumpInput;
        grabInput = player.InputHandle.GrabInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.changeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.changeState(player.IdleState);
        }
        else if (!isTouchingWall || xInput != Movement?.FacingDirection && !grabInput)
        {
            stateMachine.changeState(player.InAirState);
        }
        else if (isTouchingWallClimb && !isTouchingLedge)
        {
            stateMachine.changeState(player.LedgeClimbState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingWallClimb = collisionSenses.WallClimb;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }
        if (!isTouchingLedge && isTouchingWallClimb)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
