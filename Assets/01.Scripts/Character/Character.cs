using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    public bool IsPlayer => isPlayer;

    private bool isInactive = false;
    public bool IsInactive => isInactive;

    public string characterName;

    private void Awake()
    {
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
    }

    private void RegisterCharacter()
    {
        isInactive = false;

        if (isPlayer)
        {
            PlayerInfo playerInfo = GameManager.Instance.PlayerTransform.GetComponent<PlayerInfo>();
            if (playerInfo != null && !playerInfo.isCharacter)
            {
                isInactive = true;
                return;
            }
            //TODO: Item 추가
        }

        GameManager.Instance.CurrentCharacters.Add(this);
        Debug.Log($"{characterName} 등록");
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
    }
}