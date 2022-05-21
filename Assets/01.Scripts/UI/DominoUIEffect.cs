using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DominoUIEffect : MonoBehaviour
{
    [SerializeField] private float dominoDelay = 0.25f;
    [SerializeField] private float changeDelay = 0.3f;
    [SerializeField] private float startValue = 0f;
    [SerializeField] private float endValue = 1f;
    [SerializeField] private Ease dotweenEase = Ease.InOutQuad;
    [SerializeField] private bool onAwake;

    private WaitForSeconds animationDelay;
    private bool isAnimation;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        animationDelay = new WaitForSeconds((transform.GetSiblingIndex()) * dominoDelay);

        if (onAwake)
        {
            StartCoroutine(DominoAnimation());
        }
    }

    private IEnumerator DominoAnimation()
    {
        isAnimation = true;
        transform.localScale = Vector3.one * startValue;
        yield return animationDelay;
        transform.DOScale(endValue, changeDelay).SetEase(dotweenEase);
        isAnimation = false;
    }

    private void OnEnable()
    {
        ResetAnimation();
        StartCoroutine(DominoAnimation());
    }

    private void OnDisable()
    {
        ResetAnimation();
        transform.localScale = Vector3.one * startValue;
    }

    private void ResetAnimation()
    {
        if (isAnimation)
        {
            StopCoroutine("DominoAnimation");
            transform.DOKill();
        }
    }
}
