using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Destroy : SettingDirection
{
    private AudioClip destroyClip;

    private void Awake()
    {
        destroyClip = Resources.Load<AudioClip>("Destroy");
    }

    public override void OnCollisionTarget(Collision collision)
    {
        CharacterRenderer renderer = collision.transform.GetComponent<CharacterRenderer>();
        
        if (renderer)
        {
            SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, destroyClip);
            // TODO: 추후에 이펙트 추가!
            renderer.DisabledRenderer();
        }
    }
}
