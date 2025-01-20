using System;
using System.Collections;
using UnityEngine;


public class Bat : Entity
{
    #region STATE
    public Bat_SleepState sleepState { get; private set; }
    public Bat_WakeUpState wakeUpState { get; private set; }
    public Bat_FlyIdleState flyIdleState { get; private set; }
    public Bat_FlyMoveState flyMoveState { get; private set; }
    public Bat_AttackState attackState { get; private set; }
    #endregion
    [Header("Data")]
    #region DATA
    [SerializeField] private IdleStateData sleepStateData;
    [SerializeField] private IdleStateData wakeUpStateData;
    [SerializeField] private IdleStateData flyIdleStateData;
    [SerializeField] private MoveStateData flyMoveStateData;
    [SerializeField] private MeleeAttackStateData attackStateData;

    public float attackCoolDownTime = 1f;
    #endregion
    [Header("Tranform")]

    #region TRANSFORM
    [SerializeField] private Transform AttackPos;
    public Transform PlayerPos;
    #endregion

    [Header("Fx")]
    public GameObject HealthBar;


    public event Action OnTriggerEnter;
    public override void Awake()
    {
        base.Awake();
        sleepState = new Bat_SleepState(this, stateMachine, "sleep", sleepStateData, this);
        wakeUpState = new Bat_WakeUpState(this, stateMachine, "wakeUp", this);
        flyIdleState = new Bat_FlyIdleState(this, stateMachine, "flyIdle", sleepStateData, this);
        flyMoveState = new Bat_FlyMoveState(this, stateMachine, "flyMove", flyMoveStateData, this);
        attackState = new Bat_AttackState(this, stateMachine, "attack",AttackPos ,attackStateData, this);
    }

    public override void Start()
    {
        base.Start();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine.Initialize(sleepState);
        HideHealthBar();

        stats.Health.OnCurrentValueDecrease += HandleCurrentHealthChange;
        stats.Health.OnCurrentValueDecrease += ShowHealthBar;
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTriggerEnter?.Invoke();
            Debug.Log("playertrigger");
        }
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
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, attackStateData.attackRadius);
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
