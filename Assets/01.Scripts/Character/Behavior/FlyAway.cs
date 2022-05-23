using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyAway : SettingDirection
{
    private float maxHeight = 2.5f;
    private readonly float[] distances = { 7f, 12f, 17f };

    private const int count = 100;
    private const float delay = 2f;
    private const float delayPerCount = delay / count;

    private WaitForSeconds explosionDelay = new WaitForSeconds(delayPerCount);

    private Rigidbody rigid;

    public override void OnCollisionTarget(Collision collision)
    {
        isStopMovement = true;
        rigid = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(BehaviourCoroutine());
    }

    private IEnumerator BehaviourCoroutine()
    {
        // TODO: ¶¥¿¡ ´êÀ¸¸é ¸ØÃß´Â °Í! 

        Vector3 startPoint = transform.position;
        Vector3 endPoint = target.transform.position - (transform.forward * distances[(int)verb.unitType]);

        for (int i = 0; i < count; i++)
        {
            if (GameManager.GameState != GameState.Play)
            {
                GetComponent<BackPosition>()?.ResetObject();
                yield break;
            }

            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
            position.y = maxHeight * sin + startPoint.y;

            if (Mathf.Abs(endPoint.x - startPoint.x) > 0.01f)
                position.x = increment * (endPoint.x - startPoint.x) + startPoint.x;

            if (Mathf.Abs(endPoint.z - startPoint.z) > 0.01f)
                position.z = increment * (endPoint.z - startPoint.z) + startPoint.z;

            if (rigid)
            {
                rigid.DOMove(position, delayPerCount).SetEase(Ease.Unset);
            }
            else
            {
                transform.DOMove(position, delayPerCount).SetEase(Ease.Unset);
            }

            yield return explosionDelay;
        }

        isStopMovement = false;
    }
}
