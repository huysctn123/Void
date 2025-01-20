using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Void.Manager.GameManager;


public class ActionMapChanger : VoidMonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.UI:
                playerInput.SwitchCurrentActionMap("UI");
                break;
            case GameManager.GameState.Gameplay:
                playerInput.SwitchCurrentActionMap("Gameplay");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;

    }
    protected override void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }
}
