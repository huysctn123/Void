using Void.CoreSystem;


public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded;


    public PlayerAbilityState(Player Player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
        base(Player, stateMachine, playerData, animBoolName)
    {
    }
    protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    protected ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private Movement movement;
    private CollisionSenses collisionSenses;
    private ParticleManager particleManager;

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
            {
                stateMachine.changeState(player.IdleState);
            }
            else
            {
                stateMachine.changeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

