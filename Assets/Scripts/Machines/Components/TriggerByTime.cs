using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public class TriggerByTime : MachineComponents
    {
        public float coolDownTime;
        private float timeLeft;
        protected override void Start()
        {
            base.Start();
            timeLeft = coolDownTime;
        }
        private void Update()
        {
            if (isActive) return;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                machine.Enter();
                timeLeft = coolDownTime;
            }
        }
    }
}
