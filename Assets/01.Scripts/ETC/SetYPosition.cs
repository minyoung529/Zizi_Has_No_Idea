using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetYPosition : MonoBehaviour
{
    private LayerMask layer;
    private float yPosition = 0.5f;

    private void Awake()
    {
        layer = LayerMask.GetMask(Constant.PLATFORM_TAG);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, SettingYPosition);
    }

    private void SettingYPosition()
    {
        transform.DOKill();
        Vector3 targetPos = transform.position;
        targetPos.y = yPosition;

        transform.DOLocalMoveY(0.5f, 1.4f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, SettingYPosition);
    }
}
