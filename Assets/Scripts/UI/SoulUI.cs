using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Void.Manager;

namespace Void.UI
{
    public class SoulUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private ScoreManager scoreManager;

        private void Start()
        {
            this.text = GetComponentInChildren<TMP_Text>();
            scoreManager = FindFirstObjectByType<ScoreManager>();
            scoreManager.onValueChange += UpdateSoul;
        }
        private void UpdateSoul()
        {
            if (ScoreManager.Instance.Soul == 0) this.text.text = null;
            else
            this.text.text = ScoreManager.Instance.Soul.ToString();
        }
        private void OnDisable()
        {
            scoreManager.onValueChange -= UpdateSoul;
        }
    }
}
