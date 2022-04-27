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
    private bool isSelecting = false;

    private void Awake()
    {
        image ??= GetComponent<Image>();
    }

    private void Update()
    {
        if (isSelecting)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.localPosition.z;
            dragObject.transform.position = Vector3.Lerp(dragObject.transform.position, mousePos, Time.deltaTime * 10f);
        }
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
        isSelecting = true;

        dragObject.transform.localScale = Vector3.zero;
        dragObject.gameObject.SetActive(true);
        dragObject.transform.DOScale(1f, 0.3f);
        dragObject.sprite = image.sprite;

        VerbSystemController.CurrentVerb = verb;
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject.gameObject.SetActive(false);
        isSelecting = false;
        dragObject.sprite = null;

        VerbSystemController.CurrentVerb = null;
    }

    #endregion
}