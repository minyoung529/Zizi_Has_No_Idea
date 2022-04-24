using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayUIMovement : UIMovement
{
    protected override void Start()
    {
        base.Start();
        EventManager.StartListening(Constant.START_PLAY_EVENT, OnMove);
        EventManager.StartListening(Constant.GET_STAR_EVENT, OnMoveReverse);
    }

    protected override void OnMove()
    {
        rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InBack);
    }

    protected override void OnMoveReverse()
    {
        rectTransform.anchoredPosition = targetPosition;
        rectTransform.DOAnchorPos(originPosition, duration).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, OnMove);
        EventManager.StopListening(Constant.GET_STAR_EVENT, OnMoveReverse);
    }
}
