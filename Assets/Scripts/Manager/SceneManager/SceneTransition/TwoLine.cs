using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TwoLine : SceneTransition
{
    [SerializeField] private Image upImage;
    [SerializeField] private Image DownImage;

    public float time = 0.5f;

    public override IEnumerator AnimateTransitionIn()
    {
        upImage.rectTransform.anchoredPosition = new Vector2(-460f, 1000f);
        DownImage.rectTransform.anchoredPosition = new Vector2(-460f, -1000f);
        var tweener1 = upImage.rectTransform.DOAnchorPosY(530f, time);
        var tweener2 = DownImage.rectTransform.DOAnchorPosY(-535f, time);
        yield return tweener1.WaitForCompletion();
        yield return tweener2.WaitForCompletion();
    }

    public override IEnumerator AnimateTransitionOut()
    {
        upImage.rectTransform.anchoredPosition = new Vector2(-460f, 530f);
        DownImage.rectTransform.anchoredPosition = new Vector2(-460f, -535f);
        var tweener1 = upImage.rectTransform.DOAnchorPosY(1000f, time);
        var tweener2 = DownImage.rectTransform.DOAnchorPosY(-1000f, time);
        yield return tweener1.WaitForCompletion();
        yield return tweener2.WaitForCompletion();
    }
}
