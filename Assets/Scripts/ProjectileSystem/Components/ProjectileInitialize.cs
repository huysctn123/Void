using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Void.ProjectileSystem.Components
{
    public class ProjectileInitialize : ProjectileComponent
    {
        public UnityEvent OnInit;
        [SerializeField] private float DelayTime;
        private IEnumerator OnInitialize(float Delay)
        {
            yield return new WaitForSeconds(Delay);
            OnInit?.Invoke();
        }
        private void Initialize()
        {
            Debug.Log("onitnit");
            if(DelayTime > 0)
            {
                StartCoroutine(OnInitialize(DelayTime));
                return;
            }
            OnInit?.Invoke();
        }
        protected override void Init()
        {
            base.Init();
            Initialize();
        }
        protected override void ResetProjectile()
        {
            base.ResetProjectile();
            Initialize();
        }
    }
}