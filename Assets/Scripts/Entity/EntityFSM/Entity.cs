using System;
using UnityEngine;
using Void.CoreSystem;
using Void.Manager;

public class Entity : MonoBehaviour
{
    protected Movement movement;

    protected CollisionSenses collisionSenses;

    public FiniteStateMachine stateMachine;

    public EntityData entityData;

    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public SpriteRenderer Sprite { get; private set; }

     private Transform wallCheck;
     private Transform ledgeCheck;
     [SerializeField] protected Transform playerCheck;
     protected Transform groundCheck;
     protected Transform WallFrontCheck;
     protected Transform WallBackCheck;

    public Transform FXStartPos;

    
    protected float maxtHealth;
    protected float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    public Stats stats;
    protected ParryReceiver parryReceiver;

    public virtual void Awake()
    {
        SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        Core = GetComponentInChildren<Core>();

        this.stats = Core.GetCoreComponent<Stats>();
        this.parryReceiver = Core.GetCoreComponent<ParryReceiver>();
        this.collisionSenses = Core.GetCoreComponent<CollisionSenses>();
        this.movement = Core.GetCoreComponent<Movement>();

        parryReceiver.OnParried += HandleParry;

        stats.Health.SetMaxValue(entityData.maxHealth);
        maxtHealth = entityData.maxHealth;
        currentHealth = stats.Health.CurrentValue;
        currentStunResistance = entityData.stunResistance;

        this.Sprite = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();

        GetPlayerCheck();
        
    }
    public virtual void Start()
    {
        CheckCollision();
    }
    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();

        anim.SetFloat("yVelocity", movement.RB.velocity.y);
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }
    private void GetPlayerCheck()
    {
        if (playerCheck) return;
        playerCheck = transform.Find("PlayerCheck");
    }
    protected virtual void HandleParry()
    {

    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.PlayerLayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {   
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.PlayerLayer); ;
        return hit;
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.PlayerLayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(movement.RB.velocity.x, velocity);
        movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * movement.FacingDirection * entityData.wallCheckDistance));
            if (WallFrontCheck != null) Gizmos.DrawWireCube(WallFrontCheck.position, entityData.WallCheckSize);
            if (WallBackCheck != null) Gizmos.DrawWireCube(WallBackCheck.position, entityData.WallCheckSize);
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(groundCheck.position, entityData.GroundCheckSize);

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance) * movement.FacingDirection, 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance) * movement.FacingDirection, 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance) * movement.FacingDirection, 0.2f);
        }
    }
    public virtual void CheckCollision()
    {        
        this.wallCheck = collisionSenses.WallCheck;
        this.groundCheck = collisionSenses.GroundCheck;
        this.ledgeCheck = collisionSenses.LedgeCheckVertical;
        this.WallFrontCheck = collisionSenses.WallFrontCheck;
        this.WallBackCheck = collisionSenses.WallBackCheck;
        collisionSenses.GroundCheckSize = entityData.GroundCheckSize;
        collisionSenses.WallCheckSize = entityData.WallCheckSize;
        collisionSenses.LegdeCheckDistance = entityData.ledgeCheckDistance;
    }
}

