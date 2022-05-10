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
        get
        {
            return currentDirection.normalized;
        }
        set
        {
            currentDirection = value;
        }
    }

    [SerializeField] LayerMask groundLayer;

    private List<SettingDirection> settingDirections = new List<SettingDirection>();

    private Character character;

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
            settingDirections.FindAll(x => x.IsActive).ForEach(x => x.SetDirection());

            if (currentDirection.sqrMagnitude < 0.01f)
            {
                currentDirection = Vector3.forward;
            }

            transform.forward = Vector3.Lerp(transform.forward, CurrentDirection, Time.deltaTime * rotationSpeed);

            Vector3 velocity = rigid.velocity;
            velocity.x = CurrentDirection.x * speed;
            velocity.z = CurrentDirection.z * speed;

            rigid.velocity = velocity;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }

    private void SetDirection()
    {
        settingDirections.ForEach(x => x.ResetData());

        foreach (ItemObject item in GameManager.Instance.CurrentItems)
        {
            if (item.Item.verbPairs[character] == VerbType.None) return;
            AddSettingDirection(item.Item.verbPairs[character], item);
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
        if (transform.position.y < Constant.DEAD_LINE_Y)
        {
            GameManager.Instance.ResetStage();
        }
    }

    private void ResetData()
    {
        currentDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }
}