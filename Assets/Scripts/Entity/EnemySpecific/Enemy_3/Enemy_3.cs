using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_3 : Entity
{
    #region STATE
    public E3_IdleState idleState { get; private set; }
    public E3_MoveState moveState { get; private set; }
    public E3_LookForPLayerState lookForPlayerState { get; private set; }
    public E3_PlayerDetetedState playerDetectedState { get; private set; }
    public E3_StunState stunState { get; private set; }
    public E3_HealState healState { get; private set; }
    public E3_MeleeAttackState1 meleeAttackState1 { get; private set; }
    public E3_MeleeAttackState2 meleeAttackState2 { get; private set; }
    public E3_ChaseState chaseState { get; private set; }
    #endregion
    [Header("Data")]
    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private HealStateData healStateData;
    [SerializeField] private ChaseStateData chaseStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackState1Data;
    [SerializeField] private MeleeAttackStateData meleeAttackState2Data;
    #endregion
    [Header("Tranform")]
    #region TRANFORM
    [SerializeField] private Transform MeleeAttackPosition;
    #endregion
    [Header("FX")]
    #region FX
    public GameObject HealthBar;
    #endregion

    public override void Awake()
    {
        base.Awake();
        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        lookForPlayerState = new E3_LookForPLayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E3_PlayerDetetedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        stunState = new E3_StunState(this, stateMachine, "stun", stunStateData, this);
        healState = new E3_HealState(this, stateMachine, "heal", healStateData, this);
        chaseState = new E3_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        meleeAttackState1 = new E3_MeleeAttackState1(this, stateMachine, "meleeAttack1", MeleeAttackPosition, meleeAttackState1Data, this);
        meleeAttackState2 = new E3_MeleeAttackState2(this, stateMachine, "meleeAttack2", MeleeAttackPosition, meleeAttackState2Data, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;
    }
    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        HideHealthBar();
        stats.Health.OnCurrentValueDecrease += ShowHealthBar;

    }
    private void HandleCurrentHealthChange()
    {
        if(currentHealth > 0)
        {
            StartCoroutine(CurrentHealChange());
        }
        if (currentHealth <= maxtHealth / 2)
        {
            healState.ResetCanHeal();
        }
        stats.Health.OnCurrentValueDecrease -= ShowHealthBar;

    }
    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    protected override void HandleParry()
    {
        base.HandleParry();
        stateMachine.ChangeState(stunState);
    }
    private IEnumerator CurrentHealChange()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;    
    }
    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
        stats.Health.OnCurrentValueDecrease -= HandleCurrentHealthChange;
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(MeleeAttackPosition.position, meleeAttackState1Data.attackRadius);
    }
    public void ShowHealthBar()
    {
        var canvasGroup = HealthBar.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
    }
    public void HideHealthBar()
    {
        var canvasGroup = HealthBar.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }
}