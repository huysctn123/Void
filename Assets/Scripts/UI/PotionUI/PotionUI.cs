using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Void.UI
{
    public class PotionUI: VoidMonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private TMP_Text text;

        [SerializeField] private GetPotionData potionData;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.animator = GetComponentInChildren<Animator>();
            this.text = GetComponentInChildren<TMP_Text>();
            this.potionData = GetComponentInChildren<GetPotionData>();
        }
        private void Update()
        {
            animator.SetInteger("Count", potionData.currentUseTimeLeft);

            if(potionData.currentUseTimeLeft == 0f)
            {
                text.text = null;
                return;
            }
            else
            {
                text.SetText(potionData.currentUseTimeLeft.ToString());
            }
        }
    }
}
