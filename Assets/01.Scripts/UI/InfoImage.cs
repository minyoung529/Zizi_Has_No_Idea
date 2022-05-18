using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text messageText;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        messageText = GetComponentInChildren<Text>();
    }

    public void ActiveMessage(string message)
    {
        gameObject.SetActive(true);
        messageText.text = message;
    }
}
