using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_4 : Entity
{

    #region STATE
    public E4_IdleState idleState { get; private set; }
    public E4_MoveState moveState { get; private set; }
    public E4_LookForPlayerState lookForPlayerState { get; private set; }
    public E4_PlayerDetectedState playerDetectedState { get; private set; }
    public E4_DodgeState dodgeState { get; private set; }
    public E4_RangedAttackState rangedAttackState { get; private set; }
    #endregion
    [Header("Data")]
    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    public DodgeStateData dodgeStateData;
    [SerializeField] private RangedAttackStateData rangedAttackStateData;
    #endregion
    [Header("Tranform")]
    #region Tranforms
    [SerializeField] private Transform AttackPosition;
    #endregion
    [Header("FX")]
    public GameObject HealthBar;

    public override void Awake()
    {
        base.Awake();
        idleState = new E4_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E4_MoveState(this, stateMachine, "move", moveStateData, this);
        lookForPlayerState = new E4_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E4_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        dodgeState = new E4_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangedAttackState = new E4_RangedAttackState(this, stateMachine, "rangedAttack", AttackPosition, rangedAttackStateData, this);

        stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;
    }
    public override void Start()
    {
        base.Start();
        stats.Health.OnCurrentValueDecrease += ShowHealthBar;
        HideHealthBar();
        stateMachine.Initialize(idleState);
    }
    private void HandleCurrentHealthChange()
    {
        if (currentHealth > 0)
        {
            StartCoroutine(CurrentHealChange());
        }
        stats.Health.OnCurrentValueDecrease -= ShowHealthBar;
    }
    private IEnumerator CurrentHealChange()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }
    private void OnDestroy()
    {
        stats.Health.OnCurrentValueDecrease -= HandleCurrentHealthChange;
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

