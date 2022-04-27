using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WordImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{ 
    [SerializeField] private Image image;
    private Sprite firstSprite;

    private void Start()
    {
        firstSprite = image.sprite;
        image.transform.localScale = Vector3.zero;
    }

    //이 구조는 나중에 고치기
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.transform.localScale.sqrMagnitude > 0.01f || VerbSystemController.CurrentVerb == null) return;

        if (Input.GetMouseButton(0))
        {
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(1f, 0.2f);
            image.sprite = VerbSystemController.CurrentVerb.verbSprites;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            image.sprite = firstSprite;
            image.transform.DOScale(0f, 0.2f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("호출되나 보죠");
    }
}
