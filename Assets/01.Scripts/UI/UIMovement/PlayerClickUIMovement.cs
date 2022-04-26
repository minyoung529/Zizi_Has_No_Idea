using DG.Tweening;

public class PlayerClickUIMovement : UIMovement
{
    protected override void Start()
    {
        base.Start();
        EventManager<bool>.StartListening(Constant.CLICK_PLAYER_EVENT, OnMove);
    }

    private void OnMove(bool isActive)
    {
        if (isActive)
        {
            rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InOutQuad);
        }
        else
        {
            rectTransform.DOAnchorPos(originPosition, duration).SetEase(Ease.InOutQuad);
        }
    }

    private void OnDestroy()
    {
        EventManager<bool>.StopListening(Constant.CLICK_PLAYER_EVENT, OnMove);
    }
}