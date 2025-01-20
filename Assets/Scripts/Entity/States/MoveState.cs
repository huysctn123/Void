using UnityEngine;
using Void;
using Void.CoreSystem;
using Void.Manager;

public class MoveState : EntityState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected MoveStateData stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performCloseRangeAction;


    public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.WallFront;
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
            performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
            isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        }

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Movement?.SetVelocityX(stateData.MovementSpeed * Movement.FacingDirection);
    }

    public override void SoundTrigger()
    {
        base.SoundTrigger();
        audioPlay.PlayAudioClip(stateData.StateSound);

    }

    public override void FXTrigger()
    {
        base.FXTrigger();
        GameObject.Instantiate(stateData.FX, entity.FXStartPos.position, Quaternion.identity);
    }
}

