using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void.CoreSystem.StatsSystem
{
    [Serializable]

    public class Stat
    {
        public event Action OnCurrentValueZero;
        public event Action OnCurrentValueChange;
        public event Action OnCurrentValueDecrease;

        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }

        private float currentValue;

        public void SetCurrentValue(float amount)
        {
            currentValue = amount;
            OnCurrentValueChange?.Invoke();
        }
        public void SetMaxValue(float amount)
        {
            MaxValue = amount;
        }

        public void Init() 
        {
            CurrentValue = MaxValue;
            OnCurrentValueChange?.Invoke();
        }
        public void IncreaseMaxValue(float amount) => MaxValue += amount;

        public void Increase(float amount) 
        {
            CurrentValue += amount;
            OnCurrentValueChange?.Invoke();
        } 

        public void Decrease(float amount)
        {
            CurrentValue -= amount;
            OnCurrentValueChange?.Invoke();
            OnCurrentValueDecrease?.Invoke();
        }
       
    }
}

