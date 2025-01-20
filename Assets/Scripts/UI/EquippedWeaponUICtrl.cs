using System.Collections;
using UnityEngine;
using DG.Tweening;
using Void.Manager;

namespace Void.UI
{
    public class EquippedWeaponUICtrl : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private float Duration;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            LevelManager.Instance.EnterTimeLine += HideCanvas;
            LevelManager.Instance.ExitTimeLine += ShowCanvas;
        }
        public void ShowCanvas()
        {
            StartCoroutine(AnimateTransitionIn());
        }
        public void HideCanvas()
        {
            StartCoroutine(AnimateTransitionOut());
        }
        private IEnumerator AnimateTransitionIn()
        {
            var tweener = canvasGroup.DOFade(1f, Duration).SetUpdate(true);
            yield return tweener.WaitForCompletion();
        }
        private IEnumerator AnimateTransitionOut()
        {
            var tweener = canvasGroup.DOFade(0f, Duration).SetUpdate(true);
            yield return tweener.WaitForCompletion();
        }
        private void OnDestroy()
        {
            LevelManager.Instance.EnterTimeLine -= HideCanvas;
            LevelManager.Instance.ExitTimeLine -= ShowCanvas;
        }
    }
}