using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayUISound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private AudioClip downClip;
    [SerializeField] private AudioClip upClip;
    [SerializeField] private AudioClip enterClip;
    [SerializeField] private AudioClip exitClip;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip beginDragClip;

    [SerializeField] private bool isAutoDown = true;
    [SerializeField] private bool isAutoUp = true;
    [SerializeField] private bool isAutoEnter = true;
    [SerializeField] private bool isAutoExit = true;
    [SerializeField] private bool isAutoSelect = true;
    [SerializeField] private bool isAutoBeginDrag = true;

    public void PlayDownClip()
    {
        if (downClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, downClip);
    }

    public void PlayUpClip()
    {
        if (upClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, upClip);
    }

    public void PlayEnterClip()
    {
        if (enterClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, enterClip);
    }

    public void PlayExitClip()
    {
        if (exitClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, exitClip);
    }

    public void PlaySelectClip()
    {
        if (selectClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, selectClip);
    }

    public void PlayBeginDragClip()
    {
        if (beginDragClip == null || SoundManager.Instance == null) return;
        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, beginDragClip);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isAutoDown)
        {
            PlayDownClip();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isAutoUp)
        {
            PlayUpClip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAutoEnter)
        {
            PlayEnterClip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAutoExit)
        {
            PlayExitClip();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isAutoBeginDrag)
        {
            PlayBeginDragClip();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}