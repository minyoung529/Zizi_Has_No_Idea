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
            transform.SetPositionAndRotation(GameManager.Instance.PlayerSpawnPosition, originalRotation);
        else
            transform.SetPositionAndRotation(originPosition, originalRotation);
    }

    private void RegisterCharacter()
    {
        GameManager.Instance.CurrentCharacters.Add(this);
        Debug.Log($"{characterName} µî·Ï");
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetCharacter);
    }
}