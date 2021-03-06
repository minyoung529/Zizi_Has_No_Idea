using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

public class VerbPanelEvent : PanelBase, IPointerEnterHandler, IPointerExitHandler
{
    private Verb verb;
    private Image image;

    private void Awake()
    {
        image ??= GetComponent<Image>();
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, CheckActive);
    }

    private void CheckActive()
    {
        bool isActive = GameManager.Instance.CurrentVerbs.Contains(verb.verbType);
        gameObject.SetActive(isActive);
    }

    public void Initialize(Verb verb)
    {
        image ??= GetComponent<Image>();
        image.sprite = verb.verbSprites;
        this.verb = verb;
    }

    #region Drag
    public void OnBeginDrag()
    {
        VerbSystemController.CurrentVerb = verb;
    }

    public void OnEndDrag()
    {
        if (!ChangeUISprite.IsSelect)
        {
            VerbSystemController.CurrentVerb = null;
        }
    }

    public void OnSelected()
    {
        EventManager.TriggerEvent(Constant.SELECT_VERB_WORD_EVENT);
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