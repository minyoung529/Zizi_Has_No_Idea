using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class InfoImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text messageText;
    private WaitForSeconds textDelay = new WaitForSeconds(0.07f);

    private void SetUp()
    {
        messageText ??= GetComponentInChildren<Text>();
        messageText.text = "";

        rectTransform ??= GetComponent<RectTransform>();
        transform.DOKill();
    }
    public void ActiveMessage(string message, Vector2 position)
    {
        SetUp();
        gameObject.SetActive(message != "");

        if (message == "") return;

        AdjustSize(message);

        transform.localScale = new Vector3(0f, 1f, 1f);
        transform.DOScaleX(1f, 0.3f);

        transform.position = position;

        StopAllCoroutines();
        StartCoroutine(TextAnimation(message));
    }

    private void AdjustSize(string message)
    {
        var empty = from e in message where e == ' ' select e;

        float fontSize = messageText.fontSize;
        float width = fontSize * (empty.Count() * 0.2f + (message.Length - empty.Count()) * 1.1f);

        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = width;
        rectTransform.sizeDelta = sizeDelta;
    }

    private IEnumerator TextAnimation(string message)
    {
        foreach(char c in message)
        {
            messageText.text += c;
            yield return textDelay;
        }
    }
}
