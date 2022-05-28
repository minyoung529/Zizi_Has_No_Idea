using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Character))]
public class CharacterAnimation : MonoBehaviour
{
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int fallHash = Animator.StringToHash("Fall");
    private readonly int landingHash = Animator.StringToHash("Landing");
    private readonly int selectedHash = Animator.StringToHash("Selected");
    private readonly int joyHash = Animator.StringToHash("Joy");

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.StartListening(Constant.GET_STAR_EVENT, PlayJoyAnimation);
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, StopJoyAnimation);
    }

    public void SetWalkAnimation(Vector3 velocity)
    {
        animator.SetBool(walkHash, (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.z) > 0.1f) && velocity.y > -0.5f);
        animator.SetBool(fallHash, velocity.y < -1f);
    }

    public void PlaySelectedAnimation()
    {
        animator.SetTrigger(selectedHash);
    }

    public void PlayLandingAnimation()
    {
        animator.SetTrigger(landingHash);
    }

    private void PlayJoyAnimation()
    {
        if(GameManager.Instance.StarCount == 0)
        {
            animator.SetBool(joyHash, true);
        }
    }

    private void StopJoyAnimation()
    {
        animator.SetBool(joyHash, false);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.GET_STAR_EVENT, PlayJoyAnimation);
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, StopJoyAnimation);
    }
}