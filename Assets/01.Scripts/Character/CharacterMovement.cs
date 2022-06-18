using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 캐릭터의 움직임을 담당하는 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Character))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private UnityEvent<Vector3> OnRigidVelocity;

    private Vector3 currentDirection;
    public Vector3 CurrentDirection
    {
        get
        {
            if (IsReverse)
                return -currentDirection;
            else
                return currentDirection;
        }
        set => currentDirection = value;
    }

    public bool IsReverse { get; set; } = false;

    [SerializeField] LayerMask groundLayer;

    private List<SettingDirection> settingDirections = new List<SettingDirection>();

    // 나중에 이벤트를 구독하는 식으로 바꾸면 좋을 듯하다.
    public Character character { get; set; }

    #region Events
    [SerializeField] private UnityEvent onLanding;
    #endregion

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        character = GetComponent<Character>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, SetDirection);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetData);
    }

    private void FixedUpdate()
    {
        OnPlay();
        CheckDead();
    }

    private void OnPlay()
    {
        if (GameManager.Instance.StarCount == 0) return;

        if (GameManager.GameState == GameState.Play)
        {
            if (character.IsInactive) return;
            List<SettingDirection> directions = settingDirections.FindAll(x => x.IsActive);
            directions.ForEach(x => x.SetDirection());

            if (CurrentDirection.sqrMagnitude < 0.05f)
            {
                if (directions.FindAll(x => x.SimulateType == SimulateType.Move).Count == 0)
                {
                    CurrentDirection = transform.forward;
                }
                else
                {
                    return;
                }
            }

            transform.forward = Vector3.Lerp(transform.forward, CurrentDirection.normalized, Time.deltaTime * rotationSpeed);

            Vector3 velocity = rigid.velocity;
            velocity.x = CurrentDirection.normalized.x * speed;
            velocity.z = CurrentDirection.normalized.z * speed;

            rigid.velocity = velocity;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }

    private void SetDirection()
    {
        if (character.IsInactive) return;

        settingDirections.ForEach(x => x.ResetData());

        foreach (ItemObject item in GameManager.Instance.CurrentItems)
        {
            if (!item.Item.verbPairs.ContainsKey(character)) continue;
            if (item.Item.verbPairs[character].verbType == VerbType.None) continue;
            AddSettingDirection(item.Item.verbPairs[character].verbType, item);
        }
    }

    private void AddSettingDirection(VerbType type, ItemObject item)
    {
        Type scriptType = Type.GetType(type.ToString());

        SettingDirection settingDirection = GetComponent(scriptType) as SettingDirection;

        if (settingDirection == null || settingDirection.IsActive)
        {
            settingDirection = gameObject.AddComponent(scriptType) as SettingDirection;
        }

        settingDirection.Init(item);

        if (!settingDirections.Contains(settingDirection))
        {
            settingDirections.Add(settingDirection);
        }
    }

    private void CheckDead()
    {
        if (transform.position.y < Constant.DEAD_LINE_Y && character.IsPlayer)
        {
            GameManager.Instance.ResetStage();
        }
    }

    private void ResetData()
    {
        currentDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
        settingDirections.ForEach(x => x.ResetData());
        IsReverse = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLATFORM_TAG))
        {
            onLanding.Invoke();
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, SetDirection);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetData);
    }
}