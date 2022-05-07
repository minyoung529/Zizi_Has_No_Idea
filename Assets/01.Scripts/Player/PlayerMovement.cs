using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private UnityEvent<Vector3> OnRigidVelocity;

    private static Vector3 currentDirection;
    public static Vector3 CurrentDirection
    {
        get
        {
            return currentDirection;
        }
        set
        {
            currentDirection = value;
            currentDirection.Normalize();
        }
    }

    [SerializeField] LayerMask groundLayer;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
            if (CurrentDirection.sqrMagnitude < 0.01f)
                CurrentDirection = Vector3.forward;

            transform.forward = CurrentDirection;

            Vector3 velocity = rigid.velocity;
            velocity.x = CurrentDirection.x * speed;
            velocity.z = CurrentDirection.z * speed;

            rigid.velocity = velocity;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }

    private void CheckDead()
    {
        if(transform.position.y < Constant.DEAD_LINE_Y)
        {
            GameManager.Instance.ResetStage();
            Debug.Log("Fail");
        }
    }
}