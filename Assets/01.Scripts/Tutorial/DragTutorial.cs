using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragTutorial : MonoBehaviour
{
    private Transform targetObject;
    private Vector2 targetPosition = new Vector2(133f, 115.5f);
    private Vector2 originalPosition;

    private void Start()
    {
        targetObject = transform.GetChild(0).Find("DraggedObject");
        targetObject.GetComponent<Image>().enabled = true;
        originalPosition = targetObject.position;
    }

    void Update()
    {
        if (Vector2.Distance(targetObject.localPosition, targetPosition) < 0.1f)
        {
            targetObject.position = originalPosition;
        }

        targetObject.localPosition = Vector2.Lerp(targetObject.localPosition, targetPosition, Time.deltaTime * 3.5f);
    }

    private void OnDestroy()
    {
        targetObject.gameObject.SetActive(false);
    }
}
