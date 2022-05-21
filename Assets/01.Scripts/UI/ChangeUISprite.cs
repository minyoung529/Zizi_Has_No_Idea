using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ChangeUISprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite previousSprite;
    private Image image;

    private void Awake()
    {
        previousSprite = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && VerbSystemController.CurrentVerb != null)
        {
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(1f, 0.2f);
            image.sprite = VerbSystemController.CurrentVerb.verbSprites;
            IsSelect = true;
            isSelectThis = true;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
