using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    private void ResetObject()
    {
        PoolManager.Push(gameObject);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}