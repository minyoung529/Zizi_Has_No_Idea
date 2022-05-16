using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarObject : MonoBehaviour
{
    private bool isCollision = false;
    private void Awake()
    {
        RegisterStarCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play && !isCollision)
        {
            Debug.Log("Get Star");

            transform.DOScale(0f, 0.4f).SetEase(Ease.InOutQuad);
            GameManager.Instance.StarCount -= 1;
            isCollision = true;

            if (GameManager.Instance.StarCount == 0)
            {
                Debug.Log(GameManager.Instance.StarCount);
                EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
            }
        }
    }

    private void RegisterStarCount()
    {
        GameManager.Instance.StarCount += 1;
        Debug.Log($"Star Count = {GameManager.Instance.StarCount}");
    }
}
