using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class CircleWipe : SceneTransition
{
    public Image circle;
    public float time = 0.5f;

    public override IEnumerator AnimateTransitionIn()
    {
        circle.rectTransform.anchoredPosition = new Vector2(-3000f, 0f);
        var tweener = circle.rectTransform.DOAnchorPosX(0f, time).SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }

    public override IEnumerator AnimateTransitionOut()
    {
        var tweener = circle.rectTransform.DOAnchorPosX(3000f, time).SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }
}