using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Like : SettingDirection
{
    private Rigidbody rigid;

    public override void SetupDirection()
    {
        //currentCharacter.CurrentDirection += (target.transform.position - transform.position).normalized;
        //rigid = target.transform.GetComponentInChildren<Rigidbody>();
    }

    public override void SetDirection()
    {
        if (GameManager.GameState == GameState.Play && target != null)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = 0f;
            Vector3 charPos = transform.position;
            charPos.y = 0f;

            Vector3 direction = (targetPos - charPos).normalized;
            direction.y = 0f;

            currentCharacter.CurrentDirection += direction;
        }
    }
}
