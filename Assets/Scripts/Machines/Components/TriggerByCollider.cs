using System;
using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TriggerByCollider : MachineComponents
    {
        public float delayTime;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isActive) return;
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(Active());

        }
        protected override void Start()
        {
            base.Start();
        }
        IEnumerator Active()
        {
            yield return new WaitForSeconds(delayTime);
            machine.Enter();
        }
    }
}