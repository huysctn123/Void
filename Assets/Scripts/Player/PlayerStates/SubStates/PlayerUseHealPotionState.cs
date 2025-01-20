using System.Collections;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;


public class PlayerUseHealPotionState : PlayerGroundedState
{
    public PlayerUseHealPotionState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
        player.InputHandle.UseHealPotionInput();
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        player.potions.UseHealPotion();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.changeState(player.IdleState);
        }
    }
    public bool CheckCanUseHealPotion()
    {
        return player.potions.CurrentHealPotionUseLeft > 0;
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
        var Point = new Vector3(0, -2f, 0);
        particleSystem.StartAndSetParent(playerData.HealFX, player.transform.position + Point, Quaternion.identity, player.transform);
    }
}
