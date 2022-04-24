using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    protected RectTransform rectTransform;

    protected enum UIMovementType
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    protected UIMovementType movementType;

    [SerializeField]
    protected float duration;

    protected Vector2 targetPosition;
    protected Vector2 originPosition;

    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originPosition = rectTransform.anchoredPosition;

        switch (movementType)
        {
            case UIMovementType.Up:
                targetPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.rect.height * 0.5f);
                break;

            case UIMovementType.Down:
                targetPosition = new Vector2(rectTransform.anchoredPosition.x, -rectTransform.rect.height * 0.5f);
                break;

            case UIMovementType.Left:
                targetPosition = new Vector2(-rectTransform.rect.width * 0.5f, rectTransform.anchoredPosition.y);
                break;

            case UIMovementType.Right:
                targetPosition = new Vector2(rectTransform.rect.width * 0.5f, rectTransform.anchoredPosition.y);
                break;
        }
    }

    protected virtual void OnMove(){}

    protected virtual void OnMoveReverse(){}
}