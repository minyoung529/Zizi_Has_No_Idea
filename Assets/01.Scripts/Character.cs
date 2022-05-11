using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool isPlayer;

    public string characterName;
    private Vector3 originPosition = Vector3.zero;
    private Quaternion originalRotation = Quaternion.identity;

    private void Awake()
    {
        originPosition = transform.position;
        originPosition.y = Constant.SPAWN_CHARACTER_Y;
        originalRotation = transform.rotation;

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetCharacter);
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
    }

    private void ResetCharacter()
    {
        if (isPlayer)
        {
            transform.SetPositionAndRotation(GameManager.Instance.PlayerTransform.position, originalRotation);
        }
        else
        {
            transform.SetPositionAndRotation(originPosition, originalRotation);
        }
    }

    private void RegisterCharacter()
    {
        if (isPlayer)
        {
            PlayerInfo playerInfo = GameManager.Instance.PlayerTransform.GetComponent<PlayerInfo>();
            if (playerInfo != null && playerInfo.isCharacter) return;
            //TODO: Item 추가        
        }

        GameManager.Instance.CurrentCharacters.Add(this);
        Debug.Log($"{characterName} 등록");
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetCharacter);
    }
}