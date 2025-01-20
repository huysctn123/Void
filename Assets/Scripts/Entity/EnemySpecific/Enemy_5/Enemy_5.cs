using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5 : Entity
{
    

    #region STATE
    public E5_IdleState idleState { get; private set; }
    public E5_MoveState moveState { get; private set; }
    public E5_LookForPlayerState lookForPlayerState { get; private set; }
    public E5_PlayerDetectedState playerDetectedState { get; private set; }
    public E5_MeleeAttackState meleeAttackState { get; private set; }


    #endregion
    [Header("Data")]
    #region DATA
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private PlayerDetectedStateData detectedStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackStateData;

    #endregion
    [Header("Tranform")]

    #region Tranforms
    [SerializeField] private Transform MeleeAttackPosition;
    public Transform playerPos;
    [Header("Fx")]
    public ParticleSystem[] particle;
    public GameObject[] trail;
    public GameObject HealthBar;
    [Header("Var")]
    public float attackCoolDownTime = 1f;

    #endregion
    public override void Awake()
    {
        base.Awake();
        idleState = new E5_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E5_MoveState(this, stateMachine, "move", moveStateData, this);
        lookForPlayerState = new E5_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E5_PlayerDetectedState(this, stateMachine, "playerDetected", detectedStateData, this);
        meleeAttackState = new E5_MeleeAttackState(this, stateMachine, "meleeAttack", MeleeAttackPosition, meleeAttackStateData, this);
        
        stats.Health.OnCurrentValueChange += HandleCurrentHealthChange;
    }
    private void HandleCurrentHealthChange()
    {
        if (currentHealth > 0)
        {
            stats.Health.OnCurrentValueDecrease -= ShowHealthBar;
            StartCoroutine(CurrentHealChange());
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(MeleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
    public override void Start()
    {

        base.Start();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        HideHealthBar();
        stats.Health.OnCurrentValueDecrease += ShowHealthBar;
        stateMachine.Initialize(idleState);
    }
    private void OnDestroy()
    {
        stats.Health.OnCurrentValueChange -= HandleCurrentHealthChange;
    }
    private IEnumerator CurrentHealChange()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }
    public void OnEnterMoveState()
    {
        foreach (var par in particle)
        {
            par.Play(); 
        }
        foreach (var item in trail)
        {
            item.SetActive(true);
        }
    }
    public void OnExitMoveState()
    {
        foreach (var par in particle)
        {
            par.Stop();
        }
        foreach (var item in trail)
        {
            item.SetActive(false);
        }
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
