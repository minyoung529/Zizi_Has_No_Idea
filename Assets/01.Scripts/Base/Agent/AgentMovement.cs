using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private MovementSO movementData;

    protected float currentVelocity = 3f;
    protected Vector2 movementDirection;

    public UnityEvent<float> OnVelocityChange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnVelocityChange.Invoke(currentVelocity);
        rigid.velocity = new Vector3(movementDirection.x, rigid.velocity.y);
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0.01f)
        {
            if (Vector2.Dot(movementInput, movementDirection) < 0f)
            {
                currentVelocity = 0f;
            }
            else
            {
                currentVelocity = movementData.speed;
            }
        }
        else
        {
            currentVelocity = 0f;
        }

        movementDirection = movementInput.normalized * currentVelocity;
    }


    public void Jump()
    {

    }

    //넉백 구현 때 사용 예정
    public void StopImmediatelly()
    {
        currentVelocity = 0f;
        rigid.velocity = Vector2.zero;
    }
}