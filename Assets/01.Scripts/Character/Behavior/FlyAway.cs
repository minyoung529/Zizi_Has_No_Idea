using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class FlyAway : SettingDirection
{
    private float maxHeight = 3.5f;
    private readonly float[] distances = { 7f, 12f, 17f };

    private const int count = 100;
    private const float delay = 2f;
    private const float delayPerCount = delay / count;

    private WaitForSeconds explosionDelay = new WaitForSeconds(delayPerCount);

    private Rigidbody rigid;

    private bool isCollision;

    private Action onExplosion;
    private AudioClip explosionClip;

    private void Start()
    {
        explosionClip = Resources.Load<AudioClip>("Explosion");
    }

    public override void OnCollisionTarget(Collision collision)
    {
        isStopMovement = true;
        rigid = gameObject.GetComponent<Rigidbody>();
        onExplosion += () => SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, explosionClip);

        StartCoroutine(BehaviourCoroutine());
    }

    private IEnumerator BehaviourCoroutine()
    {
        onExplosion.Invoke();

        Vector3 startPoint = transform.position;
        Vector3 endPoint = target.transform.position - (transform.forward * distances[(int)verb.unitType]);

        for (int i = 0; i < count; i++)
        {
            Debug.Log(rigid.collisionDetectionMode);
            Debug.Log(rigid.detectCollisions);

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

            if (isCollision && i > 30)
            {
                isCollision = false;
                isStopMovement = false;
                yield break;
            }
            else if (i < 0)
            {
                isCollision = false;
            }
        }

        isStopMovement = false;
        isCollision = false;
    }

    protected override void ChildOnCollisionTrigger()
    {
        if (isStopMovement)
        {
            isCollision = true;
        }
    }
}
