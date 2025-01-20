using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void.UI;
public class Enemy_2 : Entity
{
    #region STATE
    public E2_IdleState idleState { get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }
    #endregion
    [Header("Data")]

    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    public DodgeStateData dodgeStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackStateData;
    [SerializeField] private RangedAttackStateData rangedAttackStateData;
    #endregion
    [Header("Tranform")]

    #region Tranforms
    [SerializeField] private Transform MeleeAttackPosition;
    [SerializeField] private Transform RangedAttackPostion;
    #endregion
    [Header("FX")]
    public GameObject HealthBar;

    public override void Awake()
    {
        base.Awake();
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack",MeleeAttackPosition, meleeAttackStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack",RangedAttackPostion, rangedAttackStateData, this);

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
        Gizmos.DrawWireSphere(MeleeAttackPosition.position, meleeAttackStateData.attackRadius);
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
