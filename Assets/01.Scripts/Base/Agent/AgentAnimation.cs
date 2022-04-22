using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    private Animator animator;
    protected readonly int walkHash = Animator.StringToHash(Constant.WALK_ANIM);
    protected readonly int deathHash = Animator.StringToHash(Constant.DEATH_ANIM);

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimatePlay(Vector2 moveInput)
    {
        SetWalkAnimation(Mathf.Abs(moveInput.x) > 0.01f);
    }

    private void SetWalkAnimation(bool isWalk)
    {
        animator.SetBool(walkHash, isWalk);
    }
}
