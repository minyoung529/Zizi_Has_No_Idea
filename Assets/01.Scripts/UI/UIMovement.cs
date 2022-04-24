using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    private RectTransform rectTransform;

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
    protected float speed;

    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected virtual void OnMove(){}

    protected virtual void OnMoveReverse(){}


}