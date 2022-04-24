using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int idleHash = Animator.StringToHash("Idle");
    private readonly int fallHash = Animator.StringToHash("Fall");

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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