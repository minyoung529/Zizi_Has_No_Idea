using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private AudioClip landingSound;

    public void PlayWalkSound()
    {
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, walkSound);
    }

    public void PlaySelectSound()
    {
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, selectSound);
    }

    public void PlayFallSound()
    {
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, fallSound);
    }

    public void PlayLandingSound()
    {
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, landingSound);
    }
}
