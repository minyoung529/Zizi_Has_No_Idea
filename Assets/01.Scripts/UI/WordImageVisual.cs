using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class WordImageVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    [SerializeField] private Image image;
    private Sprite firstSprite;

    [SerializeField] private UnityEvent onPanelSelected;

    private void Start()
    {
        firstSprite = image.sprite;
        image.transform.localScale = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && VerbSystemController.CurrentVerb != null)
        {
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(1f, 0.2f);
            image.sprite = VerbSystemController.CurrentVerb.verbSprites;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && VerbSystemController.CurrentVerb != null)
        {
            image.sprite = firstSprite;
            image.transform.DOScale(0f, 0.2f);
        }
    }
}