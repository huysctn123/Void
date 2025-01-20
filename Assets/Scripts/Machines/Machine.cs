using System;
using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public class Machine : VoidMonoBehaviour
    {
        public event Action OnEnter;
        public event Action OnExit;


        public Animator animator;
        public MachineAnimationEventHandler EventHandler;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.animator = GetComponentInChildren<Animator>();
            this.EventHandler = GetComponentInChildren<MachineAnimationEventHandler>();
        }
        public void Enter()
        {
            animator.SetTrigger("active");
            OnEnter?.Invoke();
        }
        public void Exit()
        {
            OnExit?.Invoke();
        }
        protected override void Start()
        {
            base.Start();
            EventHandler.OnFinish += Exit;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            EventHandler.OnFinish -= Exit;
        }
    }
}
