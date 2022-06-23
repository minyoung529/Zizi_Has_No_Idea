using DG.Tweening;
using UnityEngine;

public class PlayUIMovement : UIMovement
{
    [SerializeField] Ease moveEase = Ease.InBack;
    [SerializeField] Ease reverseEase = Ease.OutBack;

    protected override void Start()
    {
        base.Start();
        EventManager.StartListening(Constant.START_PLAY_EVENT, OnMove);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, OnMoveReverse);
    }

    protected override void OnMove()
    {
        rectTransform.DOKill();
        rectTransform.DOAnchorPos(targetPosition, duration).SetEase(moveEase);
    }

    protected override void OnMoveReverse()
    {
        rectTransform.DOKill();
        rectTransform.anchoredPosition = targetPosition;
        rectTransform.DOAnchorPos(originPosition, duration).SetEase(reverseEase);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, OnMove);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, OnMoveReverse);
    }
}
