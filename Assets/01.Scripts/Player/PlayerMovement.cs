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

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.GameState == GameState.Play)
        {
            rigid.velocity = transform.forward * speed;
        }

        OnRigidVelocity.Invoke(rigid.velocity);
    }
}
