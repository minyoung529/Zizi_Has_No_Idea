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
    private Vector3 curDirection = Vector3.forward;

    [SerializeField]
    private UnityEvent<float> OnRigidVelocity;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.GameState == GameState.Play)
        {
            rigid.velocity = curDirection * speed;
        }

        OnRigidVelocity.Invoke(rigid.velocity.sqrMagnitude);
    }
}
