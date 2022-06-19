using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    private ThornObject[] thornObjects;

    private bool toggle = false;

    private void Start()
    {
        thornObjects = FindObjectsOfType<ThornObject>();
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag(Constant.PLATFORM_TAG))
        {
            foreach (ThornObject obj in thornObjects)
            {
                if (toggle)
                    obj.Enable();
                else
                    obj.Disable();
            }

            toggle = !toggle;
        }
    }

    private void ResetObject()
    {
        toggle = false;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}
