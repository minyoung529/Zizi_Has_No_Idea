using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeverObject : MonoBehaviour
{
    private ThornObject[] thornObjects;

    public bool toggle = false;

    [SerializeField] private Transform lever;

    private readonly float offsetZ = -60f;

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
                if (toggle)
                {
                    obj.Enable();
                }
                else
                {
                    obj.Disable();
                    lever.DORotate(Vector3.forward * offsetZ, 1f);
                }
            }

            lever.DOKill();

            if (toggle)
                lever.DORotate(Vector3.forward * offsetZ, 1f);

            else
                lever.DORotate(Vector3.forward * -offsetZ, 1f);

            toggle = !toggle;
        }
    }

    private void ResetObject()
    {
        lever.eulerAngles = Vector3.forward * offsetZ;
        toggle = false;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}