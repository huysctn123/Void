using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Void.CoreSystem;
using Void.Manager;
public class ChaseState: EntityState
{

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected ParticleManager ParticleManager { get => particleManager ?? core.GetCoreComponent(ref particleManager); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private ParticleManager particleManager;

    protected ChaseStateData stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, ChaseStateData stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void FXTrigger()
    {
        base.FXTrigger();
        GameObject.Instantiate(stateData.FX, entity.FXStartPos.position, entity.transform.rotation);
    }
}
