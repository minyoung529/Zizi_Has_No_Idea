using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DraggingUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image dragObject;
    private Image image;
    private bool isSelecting = false;

    public static DraggingUI currentDraggedUI;

    private void Start()
    {
        image = GetComponent<Image>();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        isSelecting = true;
        currentDraggedUI = this;

        dragObject.transform.localScale = Vector3.zero;
        dragObject.gameObject.SetActive(true);
        dragObject.transform.DOScale(1f, 0.3f);
        dragObject.sprite = image.sprite;
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject.gameObject.SetActive(false);
        dragObject.sprite = null;
        currentDraggedUI = null;

        isSelecting = false;
    }
}
