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

    static public bool IsSelect { get; set; }
    private bool isSelectThis = false;

    private void Awake()
    {
        image ??= GetComponent<Image>();
        previousSprite = image.sprite;
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && DraggingUI.currentDraggedUI != null)
        {
            Debug.Log("enter");
            image.transform.localScale = Vector3.zero;
            image.transform.DOScale(1f, 0.2f);
            image.sprite = DraggingUI.currentDraggedUI.CurrentSprite;

            IsSelect = true;
            isSelectThis = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && isSelectThis)
        {
            Debug.Log("exit");
            image.sprite = previousSprite;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        if (sprite == null) return;

        image ??= GetComponent<Image>();
        image.sprite = sprite;
        previousSprite = sprite;
    }

    public bool IsInPointer()
    {
        bool value = isSelectThis;
        ResetData();
        return value;
    }

    private void ResetData()
    {
        isSelectThis = false;
        IsSelect = false;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetData);
    }
}