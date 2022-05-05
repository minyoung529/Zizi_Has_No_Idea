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

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.GameState == GameState.Play)
        {
            if (CurrentDirection.sqrMagnitude < 0.01f)
                CurrentDirection = Vector3.forward;

            rigid.velocity = CurrentDirection * speed;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }
}
