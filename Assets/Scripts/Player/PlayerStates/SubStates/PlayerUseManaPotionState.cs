using System.Collections;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;


public class PlayerUseManaPotionState : PlayerGroundedState
{
    
    public PlayerUseManaPotionState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
        player.InputHandle.UseManaPotionInput();
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        player.potions.UseManaPotion();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.changeState(player.IdleState);
        }
    }
    public bool CheckCanUseManaPotion()
    {
        return player.potions.CurrentManaPotionUseLeft > 0;
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        SoundManager.Instance.PlaySoundFXClip(playerData.UsePotionAudio);
    }
    public override void FXTrigger()
    {
        base.FXTrigger();
        var particleSystem = core.GetCoreComponent<ParticleManager>();
        var Point = new Vector3(0,-2f, 0);
        particleSystem.StartAndSetParent(playerData.ManaFX, player.transform.position + Point, Quaternion.identity, player.transform);
    }
}
