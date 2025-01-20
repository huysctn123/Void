public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
        base(Player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (xinput != 0)
            {
                stateMachine.changeState(player.CrouchMoveState);
            }else if(yinput != -1 && !isTouchingCeiling)
            {
                stateMachine.changeState(player.IdleState);
            }
        } 
    }
}

