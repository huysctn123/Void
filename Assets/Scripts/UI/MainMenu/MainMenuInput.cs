using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Void.UI
{
    public class MainMenuInput : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;

        private bool isActive;
        [SerializeField] private ShowUI showUI;
        private void Awake()
        {
            isActive = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isActive)
                {
                    HandleOnExit();
                    showUI.HideCanvas();
                    showUI.UnPauseGame();
                    isActive = false;
                    return;
                }
                else
                {
                    HandleOnEnter();
                    showUI.PauseGame();
                    showUI.ShowCanvas();
                    isActive = true;
                    return;
                }
            }
        }
        public void SetBoolIsActive(bool value)
        {
            isActive = value;
        }
        public void HandleOnEnter()
        {
            OnEnter?.Invoke();
        }
        public void HandleOnExit()
        {
            OnExit?.Invoke();
        }
    }
}