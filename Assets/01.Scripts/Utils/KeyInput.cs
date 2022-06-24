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
            GameManager.Instance.UIManager.ActiveLobby();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.CurrentItems.Count == 0) return;
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
        if (GameManager.GameState == GameState.Play) return false;

        Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 999f, playerMask) && GameManager.GameState == GameState.Ready)
        {
            Character character = hitInfo.transform.GetComponent<Character>();

            if (character == null) return false;

            if (character.IsPlayer && character.IsInactive)
                return false;
            else if (!character.IsPlayer && GameManager.Instance.CurrentItems.Count == 1)
                return false;
            else
                VerbSystemController.CurrentCharacter = character;

            eventParam.character = VerbSystemController.CurrentCharacter;
            eventParam.verbType = VerbType.None;
            eventParam.boolean = true;

            EventManager<EventParam>.TriggerEvent(Constant.CLICK_PLAYER_EVENT, eventParam);
            return true;
        }

        return false;
    }
}
