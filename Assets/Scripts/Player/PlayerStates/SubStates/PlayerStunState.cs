using System.Collections;
using UnityEngine;
using Void.CoreSystem;

public class PlayerStunState : PlayerState
{
    private readonly Movement movement;
    public PlayerStunState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
        movement = core.GetCoreComponent<Movement>();
    }

    public override void Enter()
    {
        base.Enter();
        player.StunFX.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.StunFX.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movement?.SetVelocityX(0f);
        if(Time.time >= StartTime + playerData.StunTime)
        {
            stateMachine.changeState(player.IdleState);
        }
    }
}