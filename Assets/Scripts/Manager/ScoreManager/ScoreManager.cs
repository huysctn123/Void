using System;
using System.Collections;
using UnityEngine;

namespace Void.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public event Action onValueChange;

        public int Soul;
        public bool canBuy;

        //====Test Soul ====//

        private void Start()
        {
            if (ScoreManager.Instance != null) Debug.LogError("Only ScoreManager allow");
            ScoreManager.Instance = this;
            canBuy = false;
        }
        public void LoadSoulAmount(int value)
        {
            Soul = value;
            onValueChange?.Invoke();
        }
        public void Increase(int amount)
        {
            Soul += amount;
            onValueChange?.Invoke();
        } 

        public void Decrease(int amount)
        {
            Soul -= amount;
            onValueChange?.Invoke();
        } 

        public void HanldeDecrease(int amount)
        {
            if (CheckCanBuy(amount))
            {
                Decrease(amount);
            }
        }
        private bool CheckCanBuy(int amount)
        {
            if(Soul < amount)
            {
                canBuy = false;
            }
            else
            {
                canBuy = true;
            }
            return canBuy;
        }
    }
}