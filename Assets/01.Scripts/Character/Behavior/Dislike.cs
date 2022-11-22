using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dislike : CharacterBehavior
{
    public override void SetDirection()
    {
        if (target != null && GameManager.GameState == GameState.Play)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = 0f;
            Vector3 charPos = transform.position;
            charPos.y = 0f;

            Vector3 direction = (charPos - targetPos).normalized;
            direction.y = 0f;

            currentCharacter.CurrentDirection += direction;
        }
    }

}