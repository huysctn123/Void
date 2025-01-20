using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public abstract class MachineComponents : VoidMonoBehaviour
    {
        protected Machine machine;
        protected MachineAnimationEventHandler machineAnimationEventHandler => machine.EventHandler;

        protected bool isActive;


        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.machine = GetComponent<Machine>();
        }
        protected override void Start()
        {
            base.Start();
            isActive = false;
            machine.OnEnter += Enter;
            machine.OnExit += Exit;
        }

        protected virtual void Enter()
        {
            isActive = true;
        }
        protected virtual void Exit()
        {
            isActive = false;
        }
        protected virtual void OnDestroy()
        {
            machine.OnExit -= Enter;
            machine.OnExit -= Exit;
        }


    }
}
