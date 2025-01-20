using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Void.Manager;
using Void.Manager.GameManager;

namespace Void.UI
{
    public class ShowUI : VoidMonoBehaviour
    {
        public event Action OnOpenUI;
        public event Action OnCloseUI;

        [SerializeField] private CanvasGroup canvasGroup;

        public float Duration = 0.4f;

        protected override void Start()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
 
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.canvasGroup = GetComponent<CanvasGroup>();
        }
        public void ShowCanvas()
        {
            StartCoroutine(AnimateTransitionIn());
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            OnOpenUI?.Invoke();
        }
        public void HideCanvas()
        {
            StartCoroutine(AnimateTransitionOut());
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            OnCloseUI?.Invoke();
        }
        private IEnumerator AnimateTransitionIn()
        {
            var tweener = canvasGroup.DOFade(1f, Duration).SetUpdate(true);
            yield return tweener.WaitForCompletion();
        }
        private  IEnumerator AnimateTransitionOut()
        {
            var tweener = canvasGroup.DOFade(0f, Duration).SetUpdate(true);
            yield return tweener.WaitForCompletion();

        }
        public void PauseGame()
        {
            GameManager.Instance.ChangeState(GameManager.GameState.UI);
        }
        public void UnPauseGame()
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);
        }
    }
}