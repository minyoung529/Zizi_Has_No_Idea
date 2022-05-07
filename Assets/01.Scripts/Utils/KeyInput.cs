using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyInput : MonoBehaviour
{
    private LayerMask playerMask;
    private EventParam eventParam;

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
                eventParam.character = null;
                eventParam.verbType = VerbType.None;
                eventParam.boolean = false;

                EventManager<EventParam>.TriggerEvent(Constant.CLICK_PLAYER_EVENT, eventParam);
            }
        }
    }

    private bool Raycast()
    {
        Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 999f, playerMask) && GameManager.GameState == GameState.Ready)
        {
            VerbSystemController.CurrentCharacter = hitInfo.transform.GetComponent<Character>();
            eventParam.character = VerbSystemController.CurrentCharacter;
            Debug.Log(eventParam.character.characterName);
            eventParam.verbType = VerbType.None;
            eventParam.boolean = true;

            EventManager<EventParam>.TriggerEvent(Constant.CLICK_PLAYER_EVENT, eventParam);
            return true;
        }

        return false;
    }
}
