using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int idleHash = Animator.StringToHash("Idle");
    private readonly int fallHash = Animator.StringToHash("Fall");

    private Animator animator;

    private LayerMask platformLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        platformLayer = LayerMask.GetMask("Platform");
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        if(Physics.Raycast(ray, 5f, platformLayer))
        {
            // TODO: 애니메이션 멈추는 코드 작성
        }
    }

    public void SetWalkAnimation(Vector3 velocity)
    {
        animator.SetBool(walkHash, velocity.x > 0.1f || velocity.z > 0.1f);
    }

    public void SetFallAnimation(Vector3 velocity)
    {
        animator.SetBool(fallHash, velocity.y < Physics.gravity.y + 1f);
    }
}