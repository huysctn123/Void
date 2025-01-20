
using UnityEngine;
using Void.Manager;


public class E3_HealState : HealState
{
    public bool canHeal { get; private set; }
    private float lastHealTime;
    private Enemy_3 enemy;
    ParticleSystem healPar;
    public E3_HealState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, HealStateData stateData, Enemy_3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        canHeal = false;
    }
    public override void Exit()
    {
        base.Exit();
        lastHealTime = Time.time;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            StateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
    public override void SoundTrigger()
    {
        base.SoundTrigger();
        audioPlay.PlayAudioClip(stateData.StateSound);
    }
    public bool CheckIfCanHeal()
    {
        return canHeal && Time.time >= lastHealTime + stateData.coolDownTime;
    }
    public void ResetCanHeal() => canHeal = true;
    public override void FXTrigger()
    {
        base.FXTrigger();
        var FX = GameObject.Instantiate(stateData.FX, entity.FXStartPos.transform.position, entity.FXStartPos.transform.rotation);
        FX.transform.localScale = entity.FXStartPos.transform.localScale;
    }
}
