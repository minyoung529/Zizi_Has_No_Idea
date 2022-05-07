using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Character))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float speed = 1f;

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

    private void Awake()
    {
        EventManager.StartListening(Constant.START_PLAY_EVENT, SetDirection);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, () => rigid.velocity = Vector3.zero);
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
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
            foreach (SettingDirection s in settingDirections)
            {
                s.SetDirection();
            }

            if (currentDirection.sqrMagnitude < 0.01f)
            {
                currentDirection = Vector3.forward;
            }

            transform.forward = CurrentDirection;

            Vector3 velocity = rigid.velocity;
            velocity.x = CurrentDirection.x * speed;
            velocity.z = CurrentDirection.z * speed;

            rigid.velocity = velocity;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }

    private void SetDirection()
    {
        foreach (Item item in GameManager.Instance.CurrentItems)
        {
            if (item.verbPairs[character] == VerbType.None) return;
            AddSettingDirection(item.verbPairs[character], item);
        }
    }

    private void AddSettingDirection(VerbType type, Item item)
    {
        SettingDirection settingDirection = GetComponent(type.ToString()) as SettingDirection;
        settingDirection ??= gameObject.AddComponent(Type.GetType(type.ToString())) as SettingDirection;
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
}