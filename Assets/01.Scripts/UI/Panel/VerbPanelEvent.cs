using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class VerbPanelEvent : PanelBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Verb verb;
    private Image image;

    private bool isSelecting = false;

    private void Awake()
    {
        image ??= GetComponent<Image>();
    }

    public void Initialize(Verb verb)
    {
        image ??= GetComponent<Image>();
        image.sprite = verb.verbSprites;
        this.verb = verb;
    }

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        VerbSystemController.CurrentVerb = verb;
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!WordImageVisual.IsSelect)
        {
            VerbSystemController.CurrentVerb = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSelecting)
        {
            EventManager.TriggerEvent(Constant.SELECT_VERB_WORD);
            isSelecting = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 position = transform.position;
        position.x += image.rectTransform.rect.width * 0.4f;
        position.y -= image.rectTransform.rect.height * 0.4f;

        GameManager.Instance.UIManager.ActiveInfoImage(verb.verbName, position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.UIManager.ActiveInfoImage("", transform.position);
    }

    #endregion
}