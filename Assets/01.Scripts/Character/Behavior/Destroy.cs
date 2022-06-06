using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Destroy : SettingDirection
{
    private AudioClip destroyClip;
    private GameObject destroyEffect;

    private const string DESTROY_EFFECT = "DestroyParticle";
    private const string DESTROY_AUDIO = "Destroy";

    private WaitForSeconds delay = new WaitForSeconds(3.5f);

    private void Awake()
    {
        destroyClip = Resources.Load<AudioClip>(DESTROY_AUDIO);
    }

    public override void OnCollisionTarget(Collision collision)
    {
        CharacterRenderer renderer = collision.transform.GetComponent<CharacterRenderer>();
        PutItem putItem = collision.transform.GetComponent<PutItem>();

        if (putItem)
        {
            putItem.PutItemOnCurrentPos();
        }

        if (renderer)
        {
            SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, destroyClip);
            destroyEffect = PoolManager.Pop(DESTROY_EFFECT);
            destroyEffect.transform.position = collision.contacts[0].point;

            Material material = collision.transform.GetComponentInChildren<Renderer>().material;
            destroyEffect.GetComponent<ParticleSystemRenderer>().material = material;

            StartCoroutine(PoolCoroutine());
            renderer.DisabledRenderer();
        }
    }

    private IEnumerator PoolCoroutine()
    {
        yield return delay;
        PoolManager.Push(destroyEffect);
    }
}
