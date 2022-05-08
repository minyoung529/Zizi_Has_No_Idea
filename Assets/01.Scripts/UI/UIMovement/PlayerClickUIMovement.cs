using DG.Tweening;

public class PlayerClickUIMovement : UIMovement
{
    protected override void Start()
    {
        base.Start();
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, OnMove);
    }

    private void OnMove(EventParam param)
    {
        gameObject.SetActive(!param.boolean);
        gameObject.SetActive(param.boolean);

        if (param.boolean)
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
        EventManager<EventParam>.StopListening(Constant.CLICK_PLAYER_EVENT, OnMove);
    }
}