using Void.CoreSystem;


public class PlayerClimbLadder : PlayerState
{
    protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    private Movement movement;
    private CollisionSenses collisionSenses;
    public PlayerClimbLadder(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(Player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
    }
}
