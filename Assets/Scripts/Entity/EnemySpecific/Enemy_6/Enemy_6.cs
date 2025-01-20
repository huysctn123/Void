using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_6 : Entity
{
    #region STATE
    public E6_idleState idleState { get; private set; }
    public E6_WalkState walkState { get; private set; }
    public E6_ChaseState chaseState { get; private set; }
    public E6_LookForPlayerState lookForPlayerState { get; private set; }
    public E6_DetectedState detectedState { get; private set; }
    public E6_Attack1State attack1State { get; private set; }
    public E6_Attack2State attack2State { get; private set; }
    public E6_StunState stunState { get; private set; }
    #endregion
    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private PlayerDetectedStateData  playerDetectedStateData;
    [SerializeField] private ChaseStateData chaseStateData;
    [SerializeField] private MeleeAttackStateData attack1StateData;
    [SerializeField] private MeleeAttackStateData attack2StateData;
    [SerializeField] private StunStateData stunStateData;
    #endregion

    #region Tranform
    [SerializeField] private Transform meleeAttackPosition;
    public Transform StunEffect;
    #endregion
    [Header("FX")]
    public GameObject HealthBar;

    public override void Awake()
    {
        base.Awake();
        idleState = new E6_idleState(this, stateMachine, "idle", idleStateData, this);
        walkState = new E6_WalkState(this, stateMachine, "move", moveStateData, this);
        detectedState = new E6_DetectedState(this, stateMachine, "detected", playerDetectedStateData, this);
        chaseState = new E6_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        attack1State = new E6_Attack1State(this, stateMachine, "attack1", meleeAttackPosition, attack1StateData, this);
        attack2State = new E6_Attack2State(this, stateMachine, "attack2", meleeAttackPosition, attack2StateData, this);
        lookForPlayerState = new E6_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new E6_StunState(this, stateMachine, "stun", stunStateData, this);

        //stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        //stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;

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
        Gizmos.DrawWireSphere(meleeAttackPosition.position, attack1StateData.attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, attack2StateData.attackRadius);
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

