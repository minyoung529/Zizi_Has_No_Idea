using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int idleHash = Animator.StringToHash("Idle");

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalkAnimation(float velocity)
    {
        animator.SetBool(walkHash, velocity > 0.1f);
    }
}
