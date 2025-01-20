using System;
using System.Collections;
using UnityEngine;
using Void.UI;


namespace Void.Manager.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public event Action<GameState> OnGameStateChanged;

        private GameState currentGameState = GameState.Gameplay;

        private void Awake()
        {
            if (GameManager.Instance != null) Debug.LogError("Only GameManager allow");
            GameManager.Instance = this;
        }
        public void ChangeState(GameState state)
        {
            if (state == currentGameState)
                return;

            switch (state)
            {
                case GameState.UI:
                    EnterUIState();
                    break;
                case GameState.Gameplay:
                    EnterGameplayState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            currentGameState = state;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        private void EnterUIState()
        {
            SoundManager.Instance.PausePlay();
            Time.timeScale = 0f;
        }

        private void EnterGameplayState()
        {
            Time.timeScale = 1f;
            SoundManager.Instance.UnPausePlay();
        }


        public enum GameState
        {
            UI,
            Gameplay
        }
    }
}