using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyInput : MonoBehaviour
{
    private LayerMask playerMask;

    private void Start()
    {
        playerMask = LayerMask.GetMask(Constant.PLAYER_TAG);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Raycast()) return;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                EventManager<bool>.TriggerEvent(Constant.CLICK_PLAYER_EVENT, false);
            }
        }
    }

    private bool Raycast()
    {
        Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, 999f, playerMask) && GameManager.GameState == GameState.Ready)
        {
            EventManager<bool>.TriggerEvent(Constant.CLICK_PLAYER_EVENT, true);
            return true;
        }

        return false;
    }
}
