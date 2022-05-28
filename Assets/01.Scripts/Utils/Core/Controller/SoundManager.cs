using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource effectAudio;

    private Dictionary<AudioType, AudioSource> audioSources = new Dictionary<AudioType, AudioSource>();

    private AudioClip fireWorkClip;

    private WaitForSeconds delay01 = new WaitForSeconds(1f);

    private void Awake()
    {
        audioSources.Add(AudioType.BGM, bgmAudio);
        audioSources.Add(AudioType.EffectSound, effectAudio);

        fireWorkClip = Resources.Load<AudioClip>("Firework");
    }

    public void PlayAudio(AudioType audioType, AudioClip audioClip)
    {
        if(audioSources.ContainsKey(audioType))
        {
            audioSources[audioType].Stop();
            audioSources[audioType].clip = audioClip;
            audioSources[audioType].Play();
        }
    }

    public void PlayOneShotAudio(AudioType audioType, AudioClip audioClip)
    {
        if (audioSources.ContainsKey(audioType))
        {
            audioSources[audioType].PlayOneShot(audioClip);
        }
    }

    public void PlayClearSound()
    {
        StartCoroutine(FireworkCoroutine());
    }

    private IEnumerator FireworkCoroutine()
    {
        PlayOneShotAudio(AudioType.EffectSound, fireWorkClip);
        yield return delay01;
        PlayOneShotAudio(AudioType.EffectSound, fireWorkClip);
        yield return delay01;
        PlayOneShotAudio(AudioType.EffectSound, fireWorkClip);
    }
}