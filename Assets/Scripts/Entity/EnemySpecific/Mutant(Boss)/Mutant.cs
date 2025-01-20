using FirstGearGames.SmoothCameraShaker;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Void.CoreSystem.StatsSystem;
using Void.Projectiles;

public class Mutant : Entity
{
    public static Mutant Instance;

    public UnityEvent MutantAlive;
    public UnityEvent MutantDeath;

    public event Action onEnable;
    protected Projectile projectileScript;

    #region STATES
    public MutantIdleState idleState { get; private set; }
    public MutantWalkState walkState { get; private set; }
    public MutantDetectedState detectedState { get; private set; }
    public MutantLookForPlayerState lookForPlayerState { get; private set; }
    public MutantChaseState chaseState { get; private set; }
    public MutantSwipingState swipingState { get; private set; }
    public MutantJumpAttackState jumpAttackState { get; private set; }
    public MutantMeleeComboAttackState meleeComboAttackState { get; private set; }
    public MutantStandingMeleeAttackState standingMeleeAttackState { get; private set; }
    public MutantRoaringState roaringState { get; private set; } 
    public MutantDieState dieState { get; private set; }
    #endregion
    #region DATA
    [Header("State Data")]
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData walkStateData;
    [SerializeField] private PlayerDetectedStateData detectedStateData;
    [SerializeField] private LookForPlayerStateData LookForPlayerData;
    [SerializeField] private ChaseStateData chaseStateData;
    [SerializeField] private MeleeAttackStateData swipingData;
    [SerializeField] private MeleeAttackStateData JumpAttackData;
    [SerializeField] private MeleeAttackStateData meleeComboAttackData;
    [SerializeField] private MeleeAttackStateData standingMeleeAttackData;
    [SerializeField] private IdleStateData roaringData;
    #endregion

    #region VARIABLES
    [Header("Variables")]
    public float SwipingCoolDownTime = 2f;
    public float ChaseCoolDownTime = 2f;
    public float StandingMeleeAttackBackhandCoolDownTime = 3f;
    public float RoaringCoolDownTime;
    public float MeleeComboAttackCoolDownTime = 4f;
    public float JumpAttackCoolDownTime = 10f;

    public float AttackJumpHeight = 20f;
    #endregion
    #region TRANFORM
    [SerializeField] private Transform attackPosition;
    public Transform PlayerCheckPos { get; private set; }
    public ShakeData JumpAttackShakeData;
    #endregion

    public override void Awake()
    {
        base.Awake();
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        this.idleState = new MutantIdleState(this, stateMachine, "idle", idleStateData, this);
        this.walkState = new MutantWalkState(this, stateMachine, "walk", walkStateData, this);
        this.detectedState = new MutantDetectedState(this, stateMachine, "detected", detectedStateData, this);
        this.lookForPlayerState = new MutantLookForPlayerState(this, stateMachine, "lookForPlayer", LookForPlayerData, this);
        this.chaseState = new MutantChaseState(this, stateMachine, "chase", chaseStateData, this);
        this.swipingState = new MutantSwipingState(this, stateMachine, "swiping", attackPosition, swipingData, this);
        this.jumpAttackState = new MutantJumpAttackState(this, stateMachine, "JumpAttack", attackPosition, JumpAttackData, this);
        this.meleeComboAttackState = new MutantMeleeComboAttackState(this, stateMachine, "MelleComboAttack", attackPosition, meleeComboAttackData, this);
        this.standingMeleeAttackState = new MutantStandingMeleeAttackState(this, stateMachine, "standingMeleeAttack", attackPosition, standingMeleeAttackData, this);
        this.roaringState = new MutantRoaringState(this, stateMachine, "roaring", roaringData, this);
        this.dieState = new MutantDieState(this, stateMachine, "die", this);


        stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;
        stats.Health.OnCurrentValueZero += HandleHealthZero;
    }
    public override void Start()
    {
        base.Start();
        GetPlayerPos();
        stateMachine.Initialize(idleState);
        movement?.Flip();
        ResetAllAttack();
    }
    private void HandleCurrentHealthChange()
    {
        if (stats.Health.CurrentValue <= 1) return;
        StartCoroutine(CurrentHealChange());
    }
    private void HandleHealthZero()
    {
        stateMachine.ChangeState(dieState);
    }

    public override void Update()
    {
        base.Update();
        anim.SetBool("grounded", collisionSenses.Ground);
    }
    public void GetPlayerPos()
    {
        PlayerCheckPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void ResetAllAttack()
    {
        swipingState.ResetCanAttack();
        meleeComboAttackState.ResetCanAttack();
        standingMeleeAttackState.ResetCanAttack();
        jumpAttackState.ResetCanAttack();
        chaseState.ResetCanChase();
    }
    private IEnumerator CurrentHealChange()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }
    private void OnEnable()
    {
        onEnable?.Invoke();
        MutantAlive?.Invoke();
    }
    private void OnDisable()
    {
        MutantDeath?.Invoke();
        stats.Health.OnCurrentValueDecrease -= HandleCurrentHealthChange;
        stats.Health.OnCurrentValueZero -= HandleHealthZero;
    }
}

