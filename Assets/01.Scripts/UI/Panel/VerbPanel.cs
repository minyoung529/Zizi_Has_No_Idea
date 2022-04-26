using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VerbPanel : PanelBase
{
    private Verb verb;
    private Image image;
    private WaitForSeconds animationDelay;

    private void Awake()
    {
        image ??= GetComponent<Image>();
    }

    public void Initialize(Verb verb)
    {
        image ??= GetComponent<Image>();
        image.sprite = verb.verbSprites;
        animationDelay = new WaitForSeconds((transform.GetSiblingIndex() - 1) * 0.25f);
        this.verb = verb;
    }

    private IEnumerator StartAnimation()
    {
        image.transform.localScale = Vector3.zero;
        yield return animationDelay;
        image.transform.DOScale(1f, 0.3f);
    }

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());   
    }
}
