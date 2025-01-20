using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void;
using Void.CoreSystem;
using Void.Manager;

public class PlayerJumpState : PlayerAbilityState
{
    private int maxAmountOfJump;
    public int amountOfJumpsLeft { get; private set; }
 
    public PlayerJumpState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
        maxAmountOfJump = playerData.AmountOfJumps;
        amountOfJumpsLeft = maxAmountOfJump;        
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandle.UseJumpInput();
        Movement?.SetVelocityY(playerData.JumpFocre);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0) return true;
        else return false;
    }
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = maxAmountOfJump;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    public void DecreaseAllAmountOfJumps() => amountOfJumpsLeft = 0;
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        SoundManager.Instance.PlaySoundFXClip(playerData.playerJumpSound);
    }
}

