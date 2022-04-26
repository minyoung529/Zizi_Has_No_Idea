using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class VerbPanel : PanelBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Verb verb;
    private Image image;
    private WaitForSeconds animationDelay;

    [SerializeField] private Image dragObject;
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
        image.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutQuad);
    }

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        VerbSystemController.CurrentVerb = verb;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragObject.transform.position = Define.MousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        VerbSystemController.CurrentVerb = null;
    }
    #endregion
}