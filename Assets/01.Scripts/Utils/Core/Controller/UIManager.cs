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

    private void GenerateVerbs()
    {

    }

    private void ActiveChartImage(bool isActive)
    {
        float delay = 0.5f;

        if (chartImage.gameObject.activeSelf == isActive) return;

        if (isActive)
        {
            Vector3 pos = Input.mousePosition;
            pos.x -= 100f;
            pos.y -= 70f;
            //LATER::FIX

            chartImage.anchoredPosition = pos;

            chartImage.gameObject.SetActive(true);
            chartImage.transform.localScale = Vector3.zero;
            chartImage.transform.DOScale(1f, delay);
        }
        else
        {
            chartImage.transform.DOScale(0f, delay).OnComplete(() => chartImage.gameObject.SetActive(false));
        }
    }

}
