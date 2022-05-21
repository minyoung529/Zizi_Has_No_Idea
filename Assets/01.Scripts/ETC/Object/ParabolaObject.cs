using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParabolaObject : MonoBehaviour
{
    private string characterName;
    public string CharacterName => characterName;

    private string itemName;
    public string ItemName => itemName;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;

    private const int count = 40;

    public float height;
    public const float maxHeight = 4f;

    private WaitForSeconds drawDelay = new WaitForSeconds(0.017f);

    public void Init(Character start, ItemObject end, Sprite sprite)
    {
        characterName = start.characterName;
        itemName = end.Item.Name;

        SetLineRenderer();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;

        startPoint = start.transform.position;
        startPoint.y = ParabolaController.offsetY;

        endPoint = end.transform.position;
        endPoint.y = ParabolaController.offsetY;

        lineRenderer.SetPosition(0, startPoint);

        height = maxHeight * (startPoint - endPoint).magnitude * 0.06f;

        StartCoroutine(DrawParabolaObject(() => SetVerbSprite(sprite)));
    }

    private void SetVerbSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.transform.position = lineRenderer.GetPosition(count / 2);

        spriteRenderer.transform.localScale = Vector3.zero;
        spriteRenderer.enabled = true;
        spriteRenderer.transform.DOScale(0.5f, 0.2f);
    }

    private void SetLineRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
    }

    private IEnumerator DrawParabolaObject(Action callback)
    {
        gameObject.SetActive(true);

        for (int i = 1; i < count - 1; i++)
        {
            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
            position.y = height * sin + startPoint.y;

            position.x = increment * (endPoint.x - startPoint.x) + startPoint.x;
            position.z = increment * (endPoint.z - startPoint.z) + startPoint.z;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(i, position);

            yield return drawDelay;
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(count - 1, endPoint);
        callback.Invoke();
    }
}
