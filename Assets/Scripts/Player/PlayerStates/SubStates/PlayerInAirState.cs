
using UnityEngine;
using UnityEngine.TextCore.Text;
using Void.CoreSystem;

public class PlayerInAirState : PlayerState
{

    public PlayerInAirState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }
    protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    private Movement movement;
    private CollisionSenses collisionSenses;

    #region Input
    private int xInput;
    private int yInput;
    public bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    #endregion






    #region Check
    private bool isGrounded;
    private bool isTouchingCeiling;
    protected bool isTouchingWallClimb;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    public bool downwardJumping;

    public bool passingThroughPlatform;

    private float startWallJumpCoyoteTime;
    #endregion

    //Grabs the Collider2D of whatever GameObject the player is passing through as a one way platform
    private Collider2D nextPlatform;
    public override void DoCheck()
    {
        base.DoCheck();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        if (CollisionSenses)
        {
            checkCollision();
        }

        if (isTouchingWallClimb && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
        if (wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }

    }

    public override void Enter()
    {
        base.Enter();
        downwardJumping = false;

    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
        isJumping = false;
        Movement?.SetGravity(playerData.DefaultGravity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        xInput = player.InputHandle.NormInputX;
        yInput = player.InputHandle.NormInputY;
        jumpInput = player.InputHandle.JumpInput;
        jumpInputStop = player.InputHandle.JumpInputStop;
        grabInput = player.InputHandle.GrabInput;
        dashInput = player.InputHandle.DashInput;

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        CheckdownwardJumping();
        CheckJumpMultiplier();
        if (player.InputHandle.AttackInputs[(int)CombatInputs.primary] && player.PrimaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.changeState(player.PrimaryAttackState);
        }
        else if (player.InputHandle.AttackInputs[(int)CombatInputs.secondary] && player.SecondaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.changeState(player.SecondaryAttackState);
        }
        else if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            stateMachine.changeState(player.LandState);
        }
        else if (isTouchingWallClimb && !isTouchingLedge && !isGrounded)
        {
            stateMachine.changeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.changeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.changeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingCeiling)
        {
            stateMachine.changeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0 && isTouchingLedge && isTouchingWallClimb)
        {
            stateMachine.changeState(player.WallSlideState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.changeState(player.DashState);
        }
        else
        {
            Movement?.CheckIfShouldFlip(xInput);
            Movement?.SetVelocityX(playerData.MovementSpeed * xInput);
            Falling();
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void Falling()
    {
        if (!downwardJumping)
        {

            if (!isJumping && Movement.CurrentVelocity.y < playerData.FallSpeed * -1)
            {
                InAirGravity();
            }

            if (Movement.CurrentVelocity.y < playerData.MaxFallSpeed * -1)
            {
                Movement?.SetVelocityY(playerData.MaxFallSpeed * -1);
            }
        }
    }
    private void DownwardJump()
    {
        if (downwardJumping)
        {
            //Pushes player down instead of up for a downward jump
            Movement?.SetVelocityX(0f);
            Movement?.SetVelocityY(playerData.MaxFallSpeed * -1);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping && !downwardJumping)
        {
            if (jumpInputStop)
            {
                Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    private void CheckdownwardJumping()
    {
        if (!isGrounded && yInput < 0 && jumpInput)
        {
            player.InputHandle.UseJumpInput();
            downwardJumping = true;
            DownwardJump();
            player.JumpState.DecreaseAllAmountOfJumps();
        }
    }
    private void InAirGravity()
    {
        if (Movement?.CurrentVelocity.y < 0)
        {
            Movement?.SetGravity(5f);
        }
        else
        {
            Movement?.SetGravity(playerData.DefaultGravity);
        }
    }
    private void checkCollision()
    {
        isGrounded = CollisionSenses.Ground;
        isTouchingWallClimb = CollisionSenses.WallClimb;
        isTouchingWall = CollisionSenses.WallFront;
        isTouchingWallBack = CollisionSenses.WallBack;
        isTouchingLedge = CollisionSenses.LedgeHorizontal;
        isTouchingCeiling = CollisionSenses.Ceiling;
    }
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time >= StartTime + playerData.CoyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.CoyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StartCoyoteTime() => coyoteTime = true;
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;

}
