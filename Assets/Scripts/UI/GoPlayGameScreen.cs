using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Void.UI
{
    public class GoPlayGameScreen : MonoBehaviour
    {
        public UnityEvent FinishAnimation;
        public UnityEvent OnDisalbeSittingObj;
        [SerializeField] CanvasGroup CanvasGroup;

        private Image image;
        private Animator animator;
        private void Start()
        {
            this.CanvasGroup = GetComponent<CanvasGroup>();
            this.animator = GetComponent<Animator>();
            this.image = GetComponent<Image>();

            ToNewState();
            //image.color = new Color(1f, 1f, 1f, 0f);
        }
        private void Update()
        {

            // TODO: test
            if (Input.GetKeyDown(KeyCode.T))
            {
                ToNewState();
            }
        }
        public void playGame()
        {
            CanvasGroup.alpha = 1f;
            animator.Play("GoPlay");
        }
        public void ToNewState()
        {
            CanvasGroup.alpha = 0f;
            animator.Play("Empty");
        }
        //private void disableSittingObj()
        //{
        //    OnDisalbeSittingObj?.Invoke();
        //}
        //private void finishAnimation()
        //{
        //    FinishAnimation?.Invoke();
        //}
        //private void disableObj()
        //{
        //    gameObject.SetActive(false);
        //}
        //public void debugbutton()
        //{
        //    Debug.Log("onlclick");
        //}
    }
}


