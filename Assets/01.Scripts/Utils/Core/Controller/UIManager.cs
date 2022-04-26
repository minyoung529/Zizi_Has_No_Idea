using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform chartImage;

    void Start()
    {
        EventManager<bool>.StartListening(Constant.CLICK_PLAYER_EVENT, ActiveChartImage);
    }

    private void ActiveChartImage(bool isActive)
    {
        float delay = 0.3f;

        if (isActive == chartImage.gameObject.activeSelf) return;

        if (isActive)
        {
            chartImage.transform.DOScale(0f, delay).SetEase(Ease.InOutQuad)
                .OnComplete(() => chartImage.gameObject.SetActive(false));
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            pos.x -= 100f;
            pos.y -= 70f;
            //LATER::FIX

            chartImage.anchoredPosition = pos;

            chartImage.gameObject.SetActive(true);
            chartImage.transform.localScale = Vector3.zero;
            chartImage.transform.DOScale(1f, delay).SetEase(Ease.InOutQuad);
        }
    }

}
