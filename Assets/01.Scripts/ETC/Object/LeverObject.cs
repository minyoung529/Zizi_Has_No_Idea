using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeverObject : MonoBehaviour
{
    private ThornObject[] thornObjects;

    [SerializeField] private Transform lever;

    private readonly float offsetZ = -60f;
    private Vector3 curEulerAngles;

    private void Start()
    {
        thornObjects = FindObjectsOfType<ThornObject>();

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constant.PLATFORM_TAG))
        {
            foreach (ThornObject obj in thornObjects)
            {
                obj.InterAct();
            }

            lever.DOKill();

            if (curEulerAngles.z < 0.1f)
                lever.DORotate(Vector3.forward * offsetZ, 1f);

            else
                lever.DORotate(Vector3.forward * -offsetZ, 1f);
        }
    }

    private void ResetObject()
    {
        lever.eulerAngles = Vector3.forward * offsetZ;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}
