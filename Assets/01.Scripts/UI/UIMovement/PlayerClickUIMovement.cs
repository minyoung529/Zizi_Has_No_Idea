using DG.Tweening;

public class PlayerClickUIMovement : UIMovement
{
    protected override void Start()
    {
        base.Start();
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, OnMove);
        EventManager.StartListening(Constant.START_PLAY_EVENT, OnReverseMove);
    }

    private void OnMove(EventParam param)
    {
        rectTransform.DOKill();

        if (param.boolean)
        {
            gameObject.SetActive(param.boolean);
            rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InOutQuad);
        }
        else
        {
            rectTransform.DOAnchorPos(originPosition, duration).SetEase(Ease.InOutQuad).
                OnComplete(() =>gameObject.SetActive(param.boolean));
        }
    }

    private void OnReverseMove()
    {
        EventParam e = new EventParam();
        e.boolean = false;

        OnMove(e);
    }

    private void OnDestroy()
    {
        EventManager<EventParam>.StopListening(Constant.CLICK_PLAYER_EVENT, OnMove);
    }
}