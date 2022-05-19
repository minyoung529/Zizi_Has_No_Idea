using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        get => currentDirection;
        set => currentDirection = value;
    }

    [SerializeField] LayerMask groundLayer;

    private List<SettingDirection> settingDirections = new List<SettingDirection>();

    public Character character { get; set; }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        character = GetComponent<Character>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, SetDirection);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetData);
    }

    private void Update()
    {
        OnPlay();
        CheckDead();
    }

    private void OnPlay()
    {
        if (GameManager.GameState == GameState.Play)
        {
            if (character.IsInactive) return;
            settingDirections.FindAll(x => x.IsActive).ForEach(x => x.SetDirection());

            if (settingDirections.FindAll(x => x.SimulateType == SimulateType.Move).Count == 0
                && currentDirection.sqrMagnitude < 0.01f)
            {
                currentDirection = transform.forward;
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
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, SetDirection);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetData);
    }
}