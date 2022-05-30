using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    public bool IsPlayer => isPlayer;

    private bool isInactive = false;
    public bool IsInactive => isInactive;

    public string characterName;

    [SerializeField] private GameObject pointObject;
    private GameObject point;

    [SerializeField] private UnityEvent onSelected;

    private void Awake()
    {
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, OnSelect);
    }

    private void OnSelect(EventParam param)
    {
        if (param.character == null) return;

        if (characterName == param.character.characterName)
        {
            onSelected.Invoke();
        }
    }

    private void RegisterCharacter()
    {
        isInactive = false;

        if (isPlayer)
        {
            PlayerInfo playerInfo = GameManager.Instance.PlayerTransform.GetComponent<PlayerInfo>();
            if (playerInfo && !playerInfo.isCharacter)
            {
                point?.SetActive(false);
                isInactive = true;
                return;
            }
            else
            {
                if (point == null)
                {
                    InstantiatePoint();
                }
                else
                {
                    point.SetActive(true);
                }
            }
            //TODO: Item 추가
        }
        else
        {
            InstantiatePoint();
        }

        GameManager.Instance.CurrentCharacters.Add(this);
        Debug.Log($"{characterName} 등록");
    }

    private void InstantiatePoint()
    {
        Vector3 pointPosition = transform.position;
        pointPosition.y += 2f;
        point = PoolManager.Pop(pointObject);
        point.transform.SetPositionAndRotation(pointPosition, pointObject.transform.rotation);
        point.transform.SetParent(transform);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, RegisterCharacter);
    }

    public bool isCollision = true;
    private void OnCollisionEnter(Collision collision)
    {
        isCollision = true;
    }
}