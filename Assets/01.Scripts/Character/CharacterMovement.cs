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

    private List<CharacterBehavior> behaviors = new List<CharacterBehavior>();

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
            List<CharacterBehavior> directions = behaviors.FindAll(x => x.IsActive);
            directions.ForEach(x => x.SetDirection());

            if (CurrentDirection.sqrMagnitude < 0.01f)
            {
                if (directions.FindAll(x => x.SimulateType == SimulateType.Move).Count == 0)
                {
                    currentDirection = transform.forward;
                }
                else
                {
                    return;
                }
            }

            // TODO: 오류 고치기...
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

        behaviors.ForEach(x => x.ResetData());

        foreach (ItemObject item in GameManager.Instance.CurrentItems)
        {
            if (!item.Item.verbPairs.ContainsKey(character)) continue;
            if (item.Item.verbPairs[character].verbType == VerbType.None) continue;
            AddSettingDirection(item.Item.verbPairs[character].verbType, item);
        }
    }

    /// <summary>
    /// 행동 타입에 맞는 컴포넌트를 넣어주는 함수
    /// </summary>
    /// <param name="type">행동 타입</param>
    /// <param name="item">대상 (아이템)</param>
    private void AddSettingDirection(VerbType type, ItemObject item)
    {
        Type scriptType = Type.GetType(type.ToString());

        CharacterBehavior behavior = GetComponent(scriptType) as CharacterBehavior;

        // 해당 행동이 없거나 있지만 대상이 다를 때는 새로 넣어준다
        if (behavior == null || behavior.IsActive)
        {
            behavior = gameObject.AddComponent(scriptType) as CharacterBehavior;
            behaviors.Add(behavior);
        }

        behavior.Init(item);
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
        behaviors.ForEach(x => x.ResetData());
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