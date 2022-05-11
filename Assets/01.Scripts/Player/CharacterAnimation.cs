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

    private Animator animator;

    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, TriggerSelectedAnimation);
    }

    public void SetWalkAnimation(Vector3 velocity)
    {
        animator.SetBool(walkHash, (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.z) > 0.1f) && velocity.y > -0.5f);
        animator.SetBool(fallHash, velocity.y < -1f);
    }

    private void TriggerSelectedAnimation(EventParam param)
    {
        if (character == null || param.character == null) return;

        if (character.characterName == param.character.characterName)
        {
            animator.Play(selectedHash);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLATFORM_TAG))
        {
            Debug.Log("¶¥¿¡ ´êÀ½");
            animator.SetTrigger(landingHash);
        }
    }
}