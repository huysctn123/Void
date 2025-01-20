using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Void.CoreSystem;
using Void.UI;

public class Enemy_1 : Entity
{
    #region STATE
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChaseState chargeState { get; private set; }
    public E1_MeleeAttack1State meleeAttack1State { get; private set; }
    public E1_MeleeAttack2State meleeAttack2State { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_StunState stunState { get; private set; }
    #endregion
    [Header("Data")]

    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    [SerializeField] private ChaseStateData chargeStateData;
    [SerializeField] private MeleeAttackStateData meleeAttack1StateData;
    [SerializeField] private MeleeAttackStateData meleeAttack2StateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private StunStateData stunStateData;
    #endregion
    [Header("Tranform")]

    #region Tranform
    [SerializeField] private Transform meleeAttackPosition;
    public Transform StunEffect;
    #endregion
    [Header("FX")]
    public GameObject HealthBar;

    public override void Awake()
    {
        base.Awake();
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_ChaseState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttack1State = new E1_MeleeAttack1State(this, stateMachine, "attack1", meleeAttackPosition, meleeAttack2StateData, this);
        meleeAttack2State = new E1_MeleeAttack2State(this, stateMachine, "attack2", meleeAttackPosition, meleeAttack2StateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;

    }
    private void HandleCurrentHealthChange()
    {
        if (currentHealth > 0)
        {
            StartCoroutine(CurrentHealChange());
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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttack1StateData.attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttack2StateData.attackRadius);
    }
    public override void Start()
    {
        base.Start();
        stats.Health.OnCurrentValueDecrease += ShowHealthBar;
        HideHealthBar();
        stateMachine.Initialize(idleState);
    }
    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
        stats.Health.OnCurrentValueDecrease -= HandleCurrentHealthChange;
    }
    private IEnumerator CurrentHealChange()
    {    
            Sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            Sprite.color = Color.white;
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

