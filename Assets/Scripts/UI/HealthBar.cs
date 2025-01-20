using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Void.CoreSystem;

namespace Void.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Stats Stats;
        public Slider healthSlider;
        public Slider easeHealthSlider;
        public float maxHealth;
        public float health;
        [SerializeField] private float lerpSpeed = 0.05f;
        void Start()
        {
            maxHealth = this.Stats.Health.MaxValue;
            health = this.Stats.Health.CurrentValue;
            healthSlider.maxValue =  maxHealth;
            easeHealthSlider.maxValue = maxHealth;

            Stats.Health.OnCurrentValueChange += HealthChange;
        }


        void Update()
        {
            if (healthSlider.value != health)
            {
                healthSlider.value = health;
            }
            if (healthSlider.value != easeHealthSlider.value)
            {
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
            }
        }
        public void GetStats(Stats stats)
        {
            Stats = stats;
            Stats.Health.OnCurrentValueChange += HealthChange;
        }
        private void HealthChange()
        {
            health = Stats.Health.CurrentValue;           
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueChange -= HealthChange;
        }
    }
}

