using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;
    public float time = 0.5f;

    public override IEnumerator AnimateTransitionIn()
    {
        var tweener = crossFade.DOFade(1f, time).SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }

    public override IEnumerator AnimateTransitionOut()
    {
        var tweener = crossFade.DOFade(0f, time).SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }
}