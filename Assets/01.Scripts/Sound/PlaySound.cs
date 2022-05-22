using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioType audioType = AudioType.EffectSound;

    public void PlayOneShot()
    {
        SoundManager.Instance.PlayOneShotAudio(audioType, audioClip);
    }
}
