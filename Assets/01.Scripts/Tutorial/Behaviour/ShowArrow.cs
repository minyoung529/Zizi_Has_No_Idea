using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowArrow : MonoBehaviour
{
    private RectTransform targetRect;
    private const string ARROW_LAYER = "ArrowLayer";
    private const string HEART_ARROW = "HeartArrow";
    private readonly Vector2 targetSize = new Vector2(100f, 190f);

    private void Start()
    {
        targetRect = transform.GetChild(0).Find(ARROW_LAYER).GetComponent<RectTransform>();
        targetRect.GetComponent<Image>().enabled = true;
    }

    private void Update()
    {
        targetRect.sizeDelta = Vector2.Lerp(targetRect.sizeDelta, targetSize, Time.deltaTime * 2f);
    }

    private void OnDestroy()
    {
        transform.GetChild(0).Find(ARROW_LAYER).gameObject.SetActive(false);
        transform.GetChild(0).Find(HEART_ARROW).gameObject.SetActive(false);
        targetRect.gameObject.SetActive(false);
    }
}
