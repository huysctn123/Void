using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : VoidMonoBehaviour
{
    public event Action<bool> OnInteractInputChanged;

    public PlayerInput playerInput;

    [SerializeField] private float inputHoldTime = 0.2f;
    private float dashInputStartTime;
    private float jumpInputStartTime;
    private float manaPotionInputStartTime;
    private float healPotionInputStartTime;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX;
    public int NormInputY;
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }

    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool[] AttackInputs { get; private set; }

    public bool ManaPotionInput { get; private set; }
    public bool ManaPotionInputStop { get; private set; }
    public bool HealPotionInput { get; private set; }
    public bool HealPotionInputStop { get; private set; }



    protected override void Start()
    {

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckManaPotionInputHoldTime();
        CheckHealPotionInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {

        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);


    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Use Jump");
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;

        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }

    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteractInputChanged?.Invoke(true);
            return;
        }

        if (context.canceled)
        {
            OnInteractInputChanged?.Invoke(false);
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
    private void CheckManaPotionInputHoldTime()
    {
        if (Time.time >= manaPotionInputStartTime + inputHoldTime)
        {
            ManaPotionInput = false;
        }
    }
    private void CheckHealPotionInputHoldTime()
    {
        if (Time.time >= healPotionInputStartTime + inputHoldTime)
        {
            HealPotionInput = false;
        }
    }
    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }
    public void OnManaPotionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("ON Use Mana Input");
            ManaPotionInput = true;
            ManaPotionInputStop = false;
            manaPotionInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            ManaPotionInputStop = true;
        }
    }
    public void OnHealPotionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            HealPotionInput = true;
            HealPotionInputStop = false;
            healPotionInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            HealPotionInputStop = true;
        }
    }
    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UsegrabInput() => GrabInput = false;
    public void UseAttackInput(int i) => AttackInputs[i] = false;
    public void UseManaPotionInput() => ManaPotionInput = false;
    public void UseHealPotionInput() => HealPotionInput = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        playerInput = GetComponent<PlayerInput>();
    }
}
public enum CombatInputs
{
    primary,
    secondary
}

