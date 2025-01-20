using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Void.CoreSystem;

namespace Void.UI
{
    public class ManaBar : MonoBehaviour
    {

        [SerializeField] private Stats Stats;
        public Slider manaSlider;
        public Slider easeManaSlider;
        public float maxMana;
        public float mana;
        [SerializeField] private float lerpSpeed = 0.05f;
        void Start()
        {
            maxMana = this.Stats.Mana.MaxValue;
            mana = this.Stats.Mana.CurrentValue;
            manaSlider.maxValue = maxMana;
            easeManaSlider.maxValue = maxMana;

            Stats.Mana.OnCurrentValueChange += ManaChange;
        }


        void Update()
        {
            if (manaSlider.value != mana)
            {
                manaSlider.value = mana;
            }
            if (manaSlider.value != easeManaSlider.value)
            {
                easeManaSlider.value = Mathf.Lerp(easeManaSlider.value, mana, lerpSpeed);
            }
        }
        public void GetStats(Stats stats)
        {
            Stats = stats;
            Stats.Mana.OnCurrentValueChange += ManaChange;
        }
        private void ManaChange()
        {
            mana = Stats.Mana.CurrentValue;
        }

        private void OnDisable()
        {
            Stats.Mana.OnCurrentValueChange -= ManaChange;
        }
    }

}