using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Wolf : Entity
{
    public UnityEvent OnDeath;
    #region STATE
    public WolfIdleState idleState { get; private set; }
    public WolfMoveState moveState { get; private set; }
    public WolfChaseState chaseState { get; private set; }
    public WolfDetectedState detectedState { get; private set; }
    public WolfLookForPlayerState lookForPlayerState { get; private set; }
    public WolfDodgeBack1State dodge1State { get; private set; }
    public WolfDodgeBack2State dodge2State { get; private set; }
    public WolfMeleeAttack1State meleeAttack1State { get; private set; }
    public WolfMeleeAttack2State1 meleeAttack2State1 { get; private set; }
    public WolfMeleeAttack2State2 meleeAttack2State2 { get; private set; }
    public WolfMeleeAttack3State meleeAttack3State { get; private set; }
    public WolfMeleeAttack4State meleeAttack4State { get; private set; }
    public WolfRangedAttackState1 rangedAttackState1 { get; private set; }
    public WolfRangedAttackState2 rangedAttackState2 { get; private set; }

    #endregion

    #region DATA
    [Header("Data")]
    [SerializeField] private IdleStateData IdleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private ChaseStateData chaseStateData;
    [SerializeField] private PlayerDetectedStateData detectedStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private DodgeStateData dodge1StateData;
    [SerializeField] private DodgeStateData dodge2StateData;
    [SerializeField] private MeleeAttackStateData meleeAttack1StateData;
    [SerializeField] private MeleeAttackStateData meleeAttack2State1Data;
    [SerializeField] private MeleeAttackStateData meleeAttack2State2Data;
    [SerializeField] private MeleeAttackStateData meleeAttack3StateData;
    [SerializeField] private MeleeAttackStateData meleeAttack4StateData;
    [SerializeField] private RangedAttackStateData rangedAttackState1Data;
    [SerializeField] private RangedAttackStateData rangedAttackState2Data;
    #endregion

    #region Tranform
    [Header("Tranform")]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangedAttackPosition;

    #endregion

    #region VARIABLES
    [Header("Variables")]
    public float Attack1CoolDownTime = 2f;
    public float ChaseCoolDownTime = 2f;
    public float Attack2CoolDownTime = 3f;
    public float Attack3CoolDownTime = 3f;
    public float Attack4CoolDownTime = 4f;
    public float RangedAttackCoolDownTime = 10f;
    #endregion


    public override void Awake()
    {
        base.Awake();
        idleState = new WolfIdleState(this, stateMachine, "idle", IdleStateData, this);
        moveState = new WolfMoveState(this, stateMachine, "walk", moveStateData, this);
        chaseState = new WolfChaseState(this, stateMachine, "chase", chaseStateData, this);
        lookForPlayerState = new WolfLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        detectedState = new WolfDetectedState(this, stateMachine, "detected", detectedStateData, this);
        dodge1State = new WolfDodgeBack1State(this, stateMachine, "dodge", dodge1StateData, this);
        dodge2State = new WolfDodgeBack2State(this, stateMachine, "dodge", dodge2StateData, this);
        meleeAttack1State = new WolfMeleeAttack1State(this, stateMachine, "attack1", meleeAttackPosition, meleeAttack1StateData, this);
        meleeAttack2State1 = new WolfMeleeAttack2State1(this, stateMachine, "attack2", meleeAttackPosition, meleeAttack2State1Data, this);
        meleeAttack2State2 = new WolfMeleeAttack2State2(this, stateMachine, "1attack2", meleeAttackPosition, meleeAttack2State2Data, this);
        meleeAttack3State = new WolfMeleeAttack3State(this, stateMachine, "attack3", meleeAttackPosition, meleeAttack3StateData, this);
        meleeAttack4State = new WolfMeleeAttack4State(this, stateMachine, "attack4", meleeAttackPosition, meleeAttack4StateData, this);
        rangedAttackState1 = new WolfRangedAttackState1(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackState1Data, this);
        rangedAttackState2 = new WolfRangedAttackState2(this, stateMachine, "rangedAttack1", rangedAttackPosition, rangedAttackState2Data, this);

    }
    public override void Start()
    {
        base.Start();
        ResetAttack();
        stateMachine.Initialize(idleState);

        stats.Health.OnCurrentValueZero += HandlOnDeath;
    }
    private void ResetAttack()
    {
        meleeAttack1State.ResetCanAttack();
        meleeAttack2State2.ResetCanAttack();
        meleeAttack3State.ResetCanAttack();
        meleeAttack4State.ResetCanAttack();
        rangedAttackState2.ResetCanAttack();
        chaseState.ResetCanAttack();
        lookForPlayerState.ResetCanLook();
    }
    private void HandlOnDeath()
    {
        OnDeath?.Invoke();
    }
    private void OnDisable()
    {
        stats.Health.OnCurrentValueZero -= HandlOnDeath;

    }
}

