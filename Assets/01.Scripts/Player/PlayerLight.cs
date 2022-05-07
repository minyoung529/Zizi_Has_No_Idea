using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerLight : MonoBehaviour
{
    new Light light;
    [SerializeField] private float intensity = 1200;
    [SerializeField] private float duration = 1f;

    private void Start()
    {
        light = GetComponent<Light>();
        EventManager.StartListening(Constant.START_PLAY_EVENT, ActiveLight);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, InactiveLight);
        gameObject.SetActive(false);
    }

    private void ActiveLight()
    {
        light.intensity = 0f;
        gameObject.SetActive(true);
        light.DOIntensity(intensity, 0f);
    }

    private void InactiveLight()
    {
        gameObject.SetActive(false);
    }
}
