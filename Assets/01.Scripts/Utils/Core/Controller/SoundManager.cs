using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource effectAudio;

    private Dictionary<AudioType, AudioSource> audioSources = new Dictionary<AudioType, AudioSource>();


    private void Awake()
    {
        audioSources.Add(AudioType.BGM, bgmAudio);
        audioSources.Add(AudioType.EffectSound, effectAudio);
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
}