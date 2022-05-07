using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class WordImageVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    private Sprite firstSprite;

    [SerializeField] private UnityEvent<VerbType> onPanelSelected;

    static public bool isSelect;

    private void Start()
    {
        firstSprite = image.sprite;
        image.transform.localScale = Vector3.zero;

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && VerbSystemController.CurrentVerb != null)
        {
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(1f, 0.2f);
            image.sprite = VerbSystemController.CurrentVerb.verbSprites;
            isSelect = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            image.transform.DOScale(0f, 0.2f);
            ResetData();
        }

        else if (!Input.GetMouseButton(0) && isSelect && VerbSystemController.CurrentVerb != null)
        {
            onPanelSelected.Invoke(VerbSystemController.CurrentVerb.verbType);
        }
    }

    private void ResetData()
    {
        image.sprite = firstSprite;
        isSelect = false;
    }
}
