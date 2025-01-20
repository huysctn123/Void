using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Void.CameraSystem;
using Void.CoreSystem;
using Void.Manager;
using Void.Manager.Scene;
using Void.Weapons;

public class Player : MonoBehaviour
{
    public static Player Instance;
    #region STATE
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerClimbLadder ClimbLadderState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    public PlayerStunState StunState { get; private set; }
    public PlayerDieState DieState { get; private set; }

    public PlayerUseHealPotionState UseHealPotionState { get; private set; }
    public PlayerUseManaPotionState UseManaPotionState { get; private set; }
    #endregion

    #region COMPONENT
    public Core core { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandle InputHandle { get; private set; }
    [HideInInspector]
    public CapsuleCollider2D MovementCollider;
    public BoxCollider2D CombatBoxCollider { get; private set; }
    public SpriteRenderer Sprite { get; private set; }
    public Stats Stats { get; private set; }
    public InteractableDetector InteractableDetector { get; private set; }
    public PlayerPotions potions { get; private set; }
    public PlayerData PlayerData { get => playerData; set => playerData = value; }


    [SerializeField] private PlayerData playerData;
    #endregion
    #region Other Variables

    [Header("Particle")]
    public GameObject DashParticle;
    public GameObject JumpParticle;
    public GameObject landParticle;
    public GameObject StunFX;
    public GameObject DieFX;
    public Transform FxPos;


    //Combat
    private CollisionSenses CollisionSenses;
    private Movement Movement;
    public bool isFacingRight;

    private Vector2 workSpace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    [HideInInspector]public CombatCollider combatCollider;

    //Camera
    public float playerFallSpeedTheshold = -10f;
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        this.core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        primaryWeapon.SetCore(core);
        secondaryWeapon.SetCore(core);

        CollisionSenses = core.GetCoreComponent<CollisionSenses>();
        Movement = core.GetCoreComponent<Movement>();
        Stats = core.GetCoreComponent<Stats>();
        InteractableDetector = core.GetCoreComponent<InteractableDetector>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "move");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, PlayerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, PlayerData, "crouchMove");
        JumpState = new PlayerJumpState(this, StateMachine, PlayerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, PlayerData, "inAir");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, PlayerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, PlayerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, PlayerData, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, PlayerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, PlayerData, "wallGrab");
        ClimbLadderState = new PlayerClimbLadder(this, StateMachine, PlayerData, "climbLadder");
        DashState = new PlayerDashState(this, StateMachine, PlayerData, "dash");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, PlayerData, "ledgeClimbState");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "attack", secondaryWeapon, CombatInputs.secondary);
        StunState = new PlayerStunState(this, StateMachine, playerData, "stun");
        DieState = new PlayerDieState(this, StateMachine, playerData, "die");
        UseHealPotionState = new PlayerUseHealPotionState(this, StateMachine, playerData, "usePotion");
        UseManaPotionState = new PlayerUseManaPotionState(this, StateMachine, playerData, "usePotion");
    }

    private void Start()
    {
        this.RB = GetComponent<Rigidbody2D>();
        this.Anim = GetComponent<Animator>();
        this.InputHandle = GetComponent<PlayerInputHandle>();
        this.MovementCollider = GetComponent<CapsuleCollider2D>();
        this.Sprite = GetComponent<SpriteRenderer>();
        this.combatCollider = GetComponentInChildren<CombatCollider>();
        this.potions = GetComponent<PlayerPotions>();
        this.CombatBoxCollider = combatCollider.CombatBoxCollider;
        
        StateMachine.InitializeState(IdleState);

        InputHandle.OnInteractInputChanged += InteractableDetector.TryInteract;

        isFacingRight = true;

        LevelManager.Instance.OnTransitionIn += OnTransitionIn;
        LevelManager.Instance.OnTransitionOut += OnTransitionOut;

        Stats.Health.OnCurrentValueDecrease += HandleOnHealthDecrease;

    }
    private void Update()
    {
        core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        CheckIsfacingRight();
        UpdateCameraYDampForPlayerFall();
        Anim.SetFloat("VelocityX", Mathf.Round(Mathf.Abs(Movement.CurrentVelocity.x)));
        Anim.SetFloat("VelocityY", Mathf.Round(Movement.CurrentVelocity.y));
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workSpace.Set(MovementCollider.size.x, height);
        workSpace.Set(CombatBoxCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workSpace;
        CombatBoxCollider.size = workSpace;
        MovementCollider.offset = center;
        CombatBoxCollider.offset = center;
    }

    private void OnEnable()
    {
        Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
        Stats.Health.OnCurrentValueZero += HandleHealthValueZero;
    }
    private void OnDisable()
    {
        Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
        Stats.Health.OnCurrentValueZero -= HandleHealthValueZero;
        Stats.Health.OnCurrentValueDecrease -= HandleOnHealthDecrease;

        LevelManager.Instance.OnTransitionIn -= OnTransitionIn;
        LevelManager.Instance.OnTransitionOut -= OnTransitionOut;
    }
    private void HandleOnHealthDecrease()
    {
        SoundManager.Instance.PlaySoundFXClip(playerData.PlayerHurtSound);
    }
    private void HandlePoiseCurrentValueZero()
    {
        StateMachine.changeState(StunState);
    }
    private void HandleHealthValueZero()
    {
        SceneLoadManager.Instance.LoadCheckPoint();
    }
    private void UpdateCameraYDampForPlayerFall()
    {
        // if falling past a certain speed theshold
        if(RB.velocity.y < playerFallSpeedTheshold && !CameraManager.Instance.isLerpingYDamping && !CameraManager.Instance.hasLerpingYDamping)
        {
            StartCoroutine(CameraManager.Instance.LerpYDamping(true));  
        }
        // if standing still or moving up
        if (RB.velocity.y >= 0f && !CameraManager.Instance.isLerpingYDamping && CameraManager.Instance.hasLerpingYDamping)
        {
            //reset camera function
            CameraManager.Instance.hasLerpingYDamping = false;
            StartCoroutine(CameraManager.Instance.LerpYDamping(false));
        }
    }
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void FXTrigger() => StateMachine.CurrentState.FXTrigger();
    private void SoundTrigger() => StateMachine.CurrentState.SoundTrigger();

    private void CheckIsfacingRight()
    {
        if (Movement.FacingDirection == 1)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }

    #region OnTransition
    public void SetInputActive(bool value)
    {
        InputHandle.enabled = value;
    }
    private void OnTransitionIn()
    {
        SetInputActive(false);
    }
    private void OnTransitionOut()
    {
        SetInputActive(true);
    }
    #endregion
}
