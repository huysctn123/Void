
using UnityEngine;
using Void.CoreSystem.StatsSystem;

namespace Void.CoreSystem
{
    public class Stats : CoreComponent
    {
        [field:SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Mana { get; private set; }
        [field:SerializeField] public Stat Poise { get; private set; }

        [SerializeField] private float poiseRecoveryRate;

        protected override void Awake()
        {
            base.Awake();    
        }
        private void Start()
        {
            Health.Init();
            Mana.Init();
            Poise.Init();
        }
        private void Update()
        {
            if (Poise.CurrentValue.Equals(Poise.MaxValue)) return;
            Poise.Increase(poiseRecoveryRate * Time.deltaTime);
        }
    }
}
