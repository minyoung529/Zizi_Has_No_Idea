using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveArrow : MonoBehaviour
{
    private Transform targetObject;
    private Vector2 targetPosition = new Vector2(-55.5f, 44f);
    private const string HEART_ARROW = "HeartArrow";

    private void Start()
    {
        targetObject = transform.GetChild(0).Find(HEART_ARROW);
        targetObject.GetComponent<Image>().enabled = true;
    }

    private void Update()
    {
        targetObject.localPosition = Vector2.Lerp(targetObject.localPosition, targetPosition, Time.deltaTime * 2f);
    }
}
