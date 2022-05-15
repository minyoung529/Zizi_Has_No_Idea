using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyAway : SettingDirection
{
    private float maxHeight = 2f;
    private float distance = 3f;

    private const int count = 100;
    private const float delay = 1.6f;
    private const float delayPerCount = delay / count;

    private WaitForSeconds explosionDelay = new WaitForSeconds(delayPerCount);

    public override void OnCollisionTarget()
    {
        isStopMovement = true;
        StartCoroutine(BehaviourCoroutine());
    }

    private IEnumerator BehaviourCoroutine()
    {
        Debug.Log("들어오긴 했따");
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position - transform.forward * distance;

        for (int i = 0; i < count; i++)
        {
            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
            position.y = maxHeight * sin + startPoint.y;

            if (Mathf.Abs(endPoint.x - startPoint.x) > 0.01f)
                position.x = increment * (endPoint.x - startPoint.x) + startPoint.x;

            if (Mathf.Abs(endPoint.z - startPoint.z) > 0.01f)
                position.z = increment * (endPoint.z - startPoint.z) + startPoint.z;

            transform.DOMove(position, delayPerCount);

            yield return explosionDelay;
        }

        isStopMovement = false;
    }
}
